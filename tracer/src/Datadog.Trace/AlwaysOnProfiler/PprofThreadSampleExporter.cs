// Modified by Splunk Inc.

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Datadog.Trace.Configuration;
using Datadog.Trace.Vendors.ProtoBuf;
using Datadog.Tracer.Pprof.Proto.Profile;

namespace Datadog.Trace.AlwaysOnProfiler
{
    internal class PprofThreadSampleExporter : ThreadSampleExporter
    {
        private readonly TimeSpan _threadSamplingPeriod;

        public PprofThreadSampleExporter(ImmutableTracerSettings tracerSettings, ILogSender logSender)
            : base(tracerSettings, logSender, "pprof-gzip-base64")
        {
            _threadSamplingPeriod = tracerSettings.ThreadSamplingPeriod;
        }

        protected override void ProcessThreadSamples(List<ThreadSample> samples)
        {
            var cpuProfile = BuildCpuProfile(samples);
            AddLogRecord(cpuProfile, ProfilingDataTypeCpu);
        }

        protected override void ProcessAllocationSamples(List<AllocationSample> allocationSamples)
        {
            var allocationProfile = BuildAllocationProfile(allocationSamples);
            AddLogRecord(allocationProfile, ProfilingDataTypeAllocation);
        }

        private static string Serialize(Profile profile)
        {
            using var memoryStream = new MemoryStream();
            using (var compressionStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                Serializer.Serialize(compressionStream, profile);
                compressionStream.Flush();
            }

            var byteArray = memoryStream.ToArray();
            return Convert.ToBase64String(byteArray);
        }

        private static SampleBuilder CreateSampleBuilder(Pprof pprof, ThreadSample threadSample)
        {
            var sampleBuilder = new SampleBuilder();

            pprof.AddLabel(sampleBuilder, "source.event.time", threadSample.Timestamp.Milliseconds);

            if (threadSample.SpanId != 0 || threadSample.TraceIdHigh != 0 || threadSample.TraceIdLow != 0)
            {
                pprof.AddLabel(sampleBuilder, "span_id", threadSample.SpanId.ToString("x16"));
                pprof.AddLabel(sampleBuilder, "trace_id", TraceIdHelper.ToString(threadSample.TraceIdHigh, threadSample.TraceIdLow));
            }

            foreach (var methodName in threadSample.Frames)
            {
                sampleBuilder.AddLocationId(pprof.GetLocationId(methodName));
            }

            pprof.AddLabel(sampleBuilder, "thread.id", threadSample.ManagedId);
            pprof.AddLabel(sampleBuilder, "thread.name", threadSample.ThreadName);
            return sampleBuilder;
        }

        private static string BuildAllocationProfile(List<AllocationSample> allocationSamples)
        {
            var pprof = new Pprof();
            foreach (var allocationSample in allocationSamples)
            {
                var sampleBuilder = CreateSampleBuilder(pprof, allocationSample.ThreadSample);

                // TODO Splunk: export typename
                sampleBuilder.AddValue(allocationSample.AllocationSizeBytes);
                pprof.Profile.Samples.Add(sampleBuilder.Build());
            }

            return Serialize(pprof.Profile);
        }

        private string BuildCpuProfile(List<ThreadSample> threadSamples)
        {
            var pprof = new Pprof();
            foreach (var threadSample in threadSamples)
            {
                var sampleBuilder = CreateSampleBuilder(pprof, threadSample);

                pprof.AddLabel(sampleBuilder, "source.event.period", (long)_threadSamplingPeriod.TotalMilliseconds);
                pprof.Profile.Samples.Add(sampleBuilder.Build());
            }

            return Serialize(pprof.Profile);
        }
    }
}
