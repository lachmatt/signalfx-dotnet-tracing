// Modified by SignalFx
using System;
using System.Collections.Generic;
using OpenTracing;
using OpenTracing.Propagation;
using SignalFx.Tracing.Logging;

namespace SignalFx.Tracing.OpenTracing
{
    internal class OpenTracingTracer : ITracer
    {
        private static readonly SignalFx.Tracing.Vendors.Serilog.ILogger Log = SignalFxLogging.For<OpenTracingTracer>();

        private readonly Dictionary<string, ICodec> _codecs;

        public OpenTracingTracer(ISignalFxTracer tracer)
            : this(tracer, new OpenTracingScopeManager(tracer.ScopeManager))
        {
        }

        public OpenTracingTracer(ISignalFxTracer tracer, global::OpenTracing.IScopeManager scopeManager)
        {
            SignalFxTracer = tracer;
            DefaultServiceName = tracer.DefaultServiceName;
            ScopeManager = scopeManager;
            _codecs = new Dictionary<string, ICodec>
            {
                { BuiltinFormats.HttpHeaders.ToString(), new HttpHeadersCodec(tracer.Propagator) },
                { BuiltinFormats.TextMap.ToString(), new HttpHeadersCodec(tracer.Propagator) } // the HttpHeadersCodec can support an unconstrained ITextMap
            };
        }

        public ISignalFxTracer SignalFxTracer { get; }

        public string DefaultServiceName { get; }

        public global::OpenTracing.IScopeManager ScopeManager { get; }

        public OpenTracingSpan ActiveSpan => (OpenTracingSpan)ScopeManager.Active?.Span;

        ISpan ITracer.ActiveSpan => ScopeManager.Active?.Span;

        public ISpanBuilder BuildSpan(string operationName)
        {
            return new OpenTracingSpanBuilder(this, operationName);
        }

        public global::OpenTracing.ISpanContext Extract<TCarrier>(IFormat<TCarrier> format, TCarrier carrier)
        {
            _codecs.TryGetValue(format.ToString(), out ICodec codec);

            if (codec != null)
            {
                return codec.Extract(carrier);
            }

            throw new NotSupportedException($"Tracer.Extract is not implemented for {format} by SignalFx.Tracing");
        }

        public void Inject<TCarrier>(global::OpenTracing.ISpanContext spanContext, IFormat<TCarrier> format, TCarrier carrier)
        {
            _codecs.TryGetValue(format.ToString(), out ICodec codec);

            if (codec != null)
            {
                codec.Inject(spanContext, carrier);
            }
            else
            {
                throw new NotSupportedException($"Tracer.Inject is not implemented for {format} by SignalFx.Tracing");
            }
        }
    }
}
