// Modified by SignalFx
using SignalFx.Tracing;
using Xunit;

namespace Datadog.Trace.Tests
{
    public class CorrelationIdentifierTests
    {
        [Fact]
        public void Ids_MatchWithActiveSpan_ZeroWithoutActiveSpan()
        {
            var parentScope = Tracer.Instance.StartActive("parent");
            var parentSpan = parentScope.Span;

            var childScope = Tracer.Instance.StartActive("child");
            var childSpan = childScope.Span;

            Assert.Equal(childSpan.SpanId, CorrelationIdentifier.SpanId);
            Assert.Equal(childSpan.TraceId, CorrelationIdentifier.TraceId);
            childScope.Close();

            Assert.Equal<ulong>(parentSpan.SpanId, CorrelationIdentifier.SpanId);
            Assert.Equal(parentSpan.TraceId, CorrelationIdentifier.TraceId);
            parentScope.Close();

            Assert.Equal<ulong>(0, CorrelationIdentifier.SpanId);
            Assert.Equal(TraceId.Zero, CorrelationIdentifier.TraceId);
        }
    }
}
