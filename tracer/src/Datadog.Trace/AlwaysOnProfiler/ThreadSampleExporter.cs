// Modified by Splunk Inc.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Datadog.Trace.Configuration;
using Datadog.Tracer.OpenTelemetry.Proto.Common.V1;
using Datadog.Tracer.OpenTelemetry.Proto.Logs.V1;
using Datadog.Tracer.OpenTelemetry.Proto.Resource.V1;

namespace Datadog.Trace.AlwaysOnProfiler
{
    internal abstract class ThreadSampleExporter
    {
        private readonly ILogSender _logSender;

        private readonly LogsData _logsData;

        protected ThreadSampleExporter(ImmutableTracerSettings tracerSettings, ILogSender logSender, string format)
        {
            FixedLogRecordAttributes = new ReadOnlyCollection<KeyValue>(new List<KeyValue>
            {
                GdiProfilingConventions.LogRecord.Attributes.Source,
                GdiProfilingConventions.LogRecord.Attributes.Type,
                GdiProfilingConventions.LogRecord.Attributes.Format(format)
            });
            _logsData = GdiProfilingConventions.CreateLogsData(tracerSettings.GlobalTags);
            _logSender = logSender ?? throw new ArgumentNullException(nameof(logSender));
        }

        private ReadOnlyCollection<KeyValue> FixedLogRecordAttributes { get; }

        public void ExportThreadSamples(List<ThreadSample> threadSamples)
        {
            if (threadSamples == null || threadSamples.Count < 1)
            {
                return;
            }

            // The same _logsData instance is used on all export messages. With the exception of the list of
            // LogRecords, the Logs property, all other fields are prepopulated.
            try
            {
                // Populate the list of LogRecords
                ProcessThreadSamples(threadSamples);

                _logSender.Send(_logsData);
            }
            finally
            {
                // The exporter reuses the _logsData object, but the actual log records are not
                // needed after serialization, release the log records so they can be garbage collected.
                _logsData.ResourceLogs[0].InstrumentationLibraryLogs[0].Logs.Clear();
            }
        }

        protected abstract void ProcessThreadSamples(List<ThreadSample> samples);

        protected LogRecord AddLogRecord(string body)
        {
            // The stack follows the experimental GDI conventions described at
            // https://github.com/signalfx/gdi-specification/blob/29cbcbc969531d50ccfd0b6a4198bb8a89cedebb/specification/semantic_conventions.md#logrecord-message-fields

            var logRecord = new LogRecord
            {
                Attributes =
                {
                    FixedLogRecordAttributes[0],
                    FixedLogRecordAttributes[1],
                    FixedLogRecordAttributes[2]
                },
                Body = new AnyValue { StringValue = body }
            };

            _logsData.ResourceLogs[0].InstrumentationLibraryLogs[0].Logs.Add(logRecord);
            return logRecord;
        }

        /// <summary>
        /// Holds the GDI profiling semantic conventions.
        /// <see href="https://github.com/signalfx/gdi-specification/blob/b09e176ca3771c3ef19fc9d23e8722fc77a3b6e9/specification/semantic_conventions.md#profiling-resourcelogs-message"/>
        /// </summary>
        internal static class GdiProfilingConventions
        {
            private const string OpenTelemetryProfiling = "otel.profiling";
            private const string Version = "0.1.0";

            public static LogsData CreateLogsData(IEnumerable<KeyValuePair<string, string>> additionalResources)
            {
                var resource = OpenTelemetry.Resource;
                foreach (var kvp in additionalResources)
                {
                    resource.Attributes.Add(new KeyValue { Key = kvp.Key, Value = new AnyValue { StringValue = kvp.Value } });
                }

                return new LogsData
                {
                    ResourceLogs =
                    {
                        new ResourceLogs
                        {
                            InstrumentationLibraryLogs =
                            {
                                new InstrumentationLibraryLogs
                                {
                                    InstrumentationLibrary = OpenTelemetry.InstrumentationLibrary,
                                },
                            },
                            Resource = OpenTelemetry.Resource
                        }
                    }
                };
            }

            private static class OpenTelemetry
            {
                public static readonly InstrumentationLibrary InstrumentationLibrary = new()
                {
                    Name = OpenTelemetryProfiling,
                    Version = Version
                };

                public static readonly Resource Resource = new()
                {
                    Attributes =
                    {
                        new KeyValue { Key = CorrelationIdentifier.EnvKey, Value = new AnyValue { StringValue = CorrelationIdentifier.Env } },
                        new KeyValue { Key = CorrelationIdentifier.ServiceKey, Value = new AnyValue { StringValue = CorrelationIdentifier.Service } },
                        new KeyValue { Key = "telemetry.sdk.name", Value = new AnyValue { StringValue = "signalfx-" + TracerConstants.Library } },
                        new KeyValue { Key = "telemetry.sdk.language", Value = new AnyValue { StringValue = TracerConstants.Language } },
                        new KeyValue { Key = "telemetry.sdk.version", Value = new AnyValue { StringValue = TracerConstants.AssemblyVersion } },
                        new KeyValue { Key = "splunk.distro.version", Value = new AnyValue { StringValue = TracerConstants.AssemblyVersion } }
                    }
                };
            }

            public static class LogRecord
            {
                public static class Attributes
                {
                    public static readonly KeyValue Source = new()
                    {
                        Key = "com.splunk.sourcetype",
                        Value = new AnyValue { StringValue = OpenTelemetryProfiling }
                    };

                    public static readonly KeyValue Type = new()
                    {
                        Key = "profiling.data.type",
                        Value = new AnyValue { StringValue = "cpu" }
                    };

                    public static KeyValue Period(long periodMilliseconds)
                    {
                        return new KeyValue
                        {
                            Key = "source.event.period",
                            Value = new AnyValue { IntValue = periodMilliseconds }
                        };
                    }

                    public static KeyValue Format(string format)
                    {
                        return new KeyValue
                        {
                            Key = "profiling.data.format",
                            Value = new AnyValue
                            {
                                StringValue = format
                            }
                        };
                    }
                }
            }
        }
    }
}
