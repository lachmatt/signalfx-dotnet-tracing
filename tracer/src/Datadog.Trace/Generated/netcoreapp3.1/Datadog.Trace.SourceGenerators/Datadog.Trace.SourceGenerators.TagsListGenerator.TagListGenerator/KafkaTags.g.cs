﻿// <auto-generated/>
#nullable enable

namespace Datadog.Trace.Tagging
{
    partial class KafkaTags
    {
        private static readonly byte[] MessageQueueTimeMsBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("message.queue_time_ms");
        private static readonly byte[] SpanKindBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("span.kind");
        private static readonly byte[] InstrumentationNameBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("component");
        private static readonly byte[] PartitionBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("messaging.kafka.partition");
        private static readonly byte[] OffsetBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("messaging.kafka.offset");
        private static readonly byte[] TombstoneBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("messaging.kafka.tombstone");

        public override string? GetTag(string key)
        {
            return key switch
            {
                "span.kind" => SpanKind,
                "component" => InstrumentationName,
                "messaging.kafka.partition" => Partition,
                "messaging.kafka.offset" => Offset,
                "messaging.kafka.tombstone" => Tombstone,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "messaging.kafka.partition": 
                    Partition = value;
                    break;
                case "messaging.kafka.offset": 
                    Offset = value;
                    break;
                case "messaging.kafka.tombstone": 
                    Tombstone = value;
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        protected static Datadog.Trace.Tagging.IProperty<string?>[] KafkaTagsProperties => 
             Datadog.Trace.ExtensionMethods.ArrayExtensions.Concat(MessagingTagsProperties,
                new Datadog.Trace.Tagging.Property<KafkaTags, string?>("span.kind", t => t.SpanKind),
                new Datadog.Trace.Tagging.Property<KafkaTags, string?>("component", t => t.InstrumentationName),
                new Datadog.Trace.Tagging.Property<KafkaTags, string?>("messaging.kafka.partition", t => t.Partition),
                new Datadog.Trace.Tagging.Property<KafkaTags, string?>("messaging.kafka.offset", t => t.Offset),
                new Datadog.Trace.Tagging.Property<KafkaTags, string?>("messaging.kafka.tombstone", t => t.Tombstone)
);

        protected override Datadog.Trace.Tagging.IProperty<string?>[] GetAdditionalTags()
        {
             return KafkaTagsProperties;
        }

        protected override int WriteAdditionalTags(ref byte[] bytes, ref int offset)
        {
            var count = 0;
            if (SpanKind != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, SpanKindBytes, SpanKind);
            }

            if (InstrumentationName != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, InstrumentationNameBytes, InstrumentationName);
            }

            if (Partition != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, PartitionBytes, Partition);
            }

            if (Offset != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, OffsetBytes, Offset);
            }

            if (Tombstone != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, TombstoneBytes, Tombstone);
            }

            return count + base.WriteAdditionalTags(ref bytes, ref offset);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (SpanKind != null)
            {
                sb.Append("span.kind (tag):")
                  .Append(SpanKind)
                  .Append(',');
            }

            if (InstrumentationName != null)
            {
                sb.Append("component (tag):")
                  .Append(InstrumentationName)
                  .Append(',');
            }

            if (Partition != null)
            {
                sb.Append("messaging.kafka.partition (tag):")
                  .Append(Partition)
                  .Append(',');
            }

            if (Offset != null)
            {
                sb.Append("messaging.kafka.offset (tag):")
                  .Append(Offset)
                  .Append(',');
            }

            if (Tombstone != null)
            {
                sb.Append("messaging.kafka.tombstone (tag):")
                  .Append(Tombstone)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
        public override double? GetMetric(string key)
        {
            return key switch
            {
                "message.queue_time_ms" => MessageQueueTimeMs,
                _ => base.GetMetric(key),
            };
        }

        public override void SetMetric(string key, double? value)
        {
            switch(key)
            {
                case "message.queue_time_ms": 
                    MessageQueueTimeMs = value;
                    break;
                default: 
                    base.SetMetric(key, value);
                    break;
            }
        }

        protected override int WriteAdditionalMetrics(ref byte[] bytes, ref int offset)
        {
            var count = 0;
            if (MessageQueueTimeMs != null)
            {
                count++;
                WriteMetric(ref bytes, ref offset, MessageQueueTimeMsBytes, MessageQueueTimeMs.Value);
            }

            return count + base.WriteAdditionalMetrics(ref bytes, ref offset);
        }

        protected override void WriteAdditionalMetrics(System.Text.StringBuilder sb)
        {
            if (MessageQueueTimeMs != null)
            {
                sb.Append("message.queue_time_ms (metric):")
                  .Append(MessageQueueTimeMs.Value)
                  .Append(',');
            }

            base.WriteAdditionalMetrics(sb);
        }
    }
}
