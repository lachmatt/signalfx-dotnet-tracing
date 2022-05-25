﻿// <auto-generated/>
#nullable enable

using Datadog.Trace.Processors;
using Datadog.Trace.Tagging;

namespace Datadog.Trace.ClrProfiler.AutoInstrumentation.Redis
{
    partial class RedisTags
    {
        // SpanKindBytes = System.Text.Encoding.UTF8.GetBytes("span.kind");
        private static readonly byte[] SpanKindBytes = new byte[] { 115, 112, 97, 110, 46, 107, 105, 110, 100 };
        // InstrumentationNameBytes = System.Text.Encoding.UTF8.GetBytes("component");
        private static readonly byte[] InstrumentationNameBytes = new byte[] { 99, 111, 109, 112, 111, 110, 101, 110, 116 };
        // DbTypeBytes = System.Text.Encoding.UTF8.GetBytes("db.system");
        private static readonly byte[] DbTypeBytes = new byte[] { 100, 98, 46, 115, 121, 115, 116, 101, 109 };
        // RawCommandBytes = System.Text.Encoding.UTF8.GetBytes("db.statement");
        private static readonly byte[] RawCommandBytes = new byte[] { 100, 98, 46, 115, 116, 97, 116, 101, 109, 101, 110, 116 };
        // HostBytes = System.Text.Encoding.UTF8.GetBytes("net.peer.name");
        private static readonly byte[] HostBytes = new byte[] { 110, 101, 116, 46, 112, 101, 101, 114, 46, 110, 97, 109, 101 };
        // PortBytes = System.Text.Encoding.UTF8.GetBytes("net.peer.port");
        private static readonly byte[] PortBytes = new byte[] { 110, 101, 116, 46, 112, 101, 101, 114, 46, 112, 111, 114, 116 };

        public override string? GetTag(string key)
        {
            return key switch
            {
                "span.kind" => SpanKind,
                "component" => InstrumentationName,
                "db.system" => DbType,
                "db.statement" => RawCommand,
                "net.peer.name" => Host,
                "net.peer.port" => Port,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "db.statement": 
                    RawCommand = value;
                    break;
                case "net.peer.name": 
                    Host = value;
                    break;
                case "net.peer.port": 
                    Port = value;
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        protected static Datadog.Trace.Tagging.IProperty<string?>[] RedisTagsProperties => 
             Datadog.Trace.ExtensionMethods.ArrayExtensions.Concat(InstrumentationTagsProperties,
                new Datadog.Trace.Tagging.Property<RedisTags, string?>("span.kind", t => t.SpanKind),
                new Datadog.Trace.Tagging.Property<RedisTags, string?>("component", t => t.InstrumentationName),
                new Datadog.Trace.Tagging.Property<RedisTags, string?>("db.system", t => t.DbType),
                new Datadog.Trace.Tagging.Property<RedisTags, string?>("db.statement", t => t.RawCommand),
                new Datadog.Trace.Tagging.Property<RedisTags, string?>("net.peer.name", t => t.Host),
                new Datadog.Trace.Tagging.Property<RedisTags, string?>("net.peer.port", t => t.Port)
        );

        public override void EnumerateTags<TProcessor>(ref TProcessor processor)
        {
            if (SpanKind is not null)
            {
                processor.Process(new TagItem<string>("span.kind", SpanKind, SpanKindBytes));
            }

            if (InstrumentationName is not null)
            {
                processor.Process(new TagItem<string>("component", InstrumentationName, InstrumentationNameBytes));
            }

            if (DbType is not null)
            {
                processor.Process(new TagItem<string>("db.system", DbType, DbTypeBytes));
            }

            if (RawCommand is not null)
            {
                processor.Process(new TagItem<string>("db.statement", RawCommand, RawCommandBytes));
            }

            if (Host is not null)
            {
                processor.Process(new TagItem<string>("net.peer.name", Host, HostBytes));
            }

            if (Port is not null)
            {
                processor.Process(new TagItem<string>("net.peer.port", Port, PortBytes));
            }

            base.EnumerateTags(ref processor);
        }

        protected override Datadog.Trace.Tagging.IProperty<string?>[] GetAdditionalTags()
        {
             return RedisTagsProperties;
        }

        protected override int WriteAdditionalTags(ref byte[] bytes, ref int offset, ITagProcessor[] tagProcessors)
        {
            var count = 0;
            if (SpanKind is not null)
            {
                count++;
                WriteTag(ref bytes, ref offset, SpanKindBytes, SpanKind, tagProcessors);
            }

            if (InstrumentationName is not null)
            {
                count++;
                WriteTag(ref bytes, ref offset, InstrumentationNameBytes, InstrumentationName, tagProcessors);
            }

            if (DbType is not null)
            {
                count++;
                WriteTag(ref bytes, ref offset, DbTypeBytes, DbType, tagProcessors);
            }

            if (RawCommand is not null)
            {
                count++;
                WriteTag(ref bytes, ref offset, RawCommandBytes, RawCommand, tagProcessors);
            }

            if (Host is not null)
            {
                count++;
                WriteTag(ref bytes, ref offset, HostBytes, Host, tagProcessors);
            }

            if (Port is not null)
            {
                count++;
                WriteTag(ref bytes, ref offset, PortBytes, Port, tagProcessors);
            }

            return count + base.WriteAdditionalTags(ref bytes, ref offset, tagProcessors);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (SpanKind is not null)
            {
                sb.Append("span.kind (tag):")
                  .Append(SpanKind)
                  .Append(',');
            }

            if (InstrumentationName is not null)
            {
                sb.Append("component (tag):")
                  .Append(InstrumentationName)
                  .Append(',');
            }

            if (DbType is not null)
            {
                sb.Append("db.system (tag):")
                  .Append(DbType)
                  .Append(',');
            }

            if (RawCommand is not null)
            {
                sb.Append("db.statement (tag):")
                  .Append(RawCommand)
                  .Append(',');
            }

            if (Host is not null)
            {
                sb.Append("net.peer.name (tag):")
                  .Append(Host)
                  .Append(',');
            }

            if (Port is not null)
            {
                sb.Append("net.peer.port (tag):")
                  .Append(Port)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}
