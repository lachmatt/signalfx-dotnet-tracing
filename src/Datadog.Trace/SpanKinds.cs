// Modified by SignalFx

namespace SignalFx.Tracing
{
    /// <summary>
    /// A set of standard span kinds that can be used by integrations.
    /// Not to be confused with span types.
    /// </summary>
    /// <seealso cref="SpanTypes"/>
    public static class SpanKinds
    {
        /// <summary>
        /// A span generated by the client in a client/server architecture.
        /// </summary>
        /// <seealso cref="Tags.SpanKind"/>
        public const string Client = "client";

        /// <summary>
        /// A span generated by the server in a client/server architecture.
        /// </summary>
        /// <seealso cref="Tags.SpanKind"/>
        public const string Server = "server";

        /// <summary>
        /// A span generated by the producer in a producer/consumer architecture.
        /// </summary>
        /// <seealso cref="Tags.SpanKind"/>
        public const string Producer = "producer";

        /// <summary>
        /// A span generated by the consumer in a producer/consumer architecture.
        /// </summary>
        /// <seealso cref="Tags.SpanKind"/>
        public const string Consumer = "consumer";
    }
}
