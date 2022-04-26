﻿// <auto-generated/>
#nullable enable

using Datadog.Trace.Processors;

namespace Datadog.Trace.Tagging
{
    partial class AerospikeTags
    {
        private static readonly byte[] SpanKindBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("span.kind");
        private static readonly byte[] InstrumentationNameBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("component");
        private static readonly byte[] DbTypeBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("db.system");
        private static readonly byte[] KeyBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("db.aerospike.key");
        private static readonly byte[] NamespaceBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("db.aerospike.namespace");
        private static readonly byte[] SetNameBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("db.aerospike.setname");
        private static readonly byte[] UserKeyBytes = Datadog.Trace.Vendors.MessagePack.StringEncoding.UTF8.GetBytes("db.aerospike.userkey");

        public override string? GetTag(string key)
        {
            return key switch
            {
                "span.kind" => SpanKind,
                "component" => InstrumentationName,
                "db.system" => DbType,
                "db.aerospike.key" => Key,
                "db.aerospike.namespace" => Namespace,
                "db.aerospike.setname" => SetName,
                "db.aerospike.userkey" => UserKey,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "db.aerospike.key": 
                    Key = value;
                    break;
                case "db.aerospike.namespace": 
                    Namespace = value;
                    break;
                case "db.aerospike.setname": 
                    SetName = value;
                    break;
                case "db.aerospike.userkey": 
                    UserKey = value;
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        protected static Datadog.Trace.Tagging.IProperty<string?>[] AerospikeTagsProperties => 
             Datadog.Trace.ExtensionMethods.ArrayExtensions.Concat(InstrumentationTagsProperties,
                new Datadog.Trace.Tagging.Property<AerospikeTags, string?>("span.kind", t => t.SpanKind),
                new Datadog.Trace.Tagging.Property<AerospikeTags, string?>("component", t => t.InstrumentationName),
                new Datadog.Trace.Tagging.Property<AerospikeTags, string?>("db.system", t => t.DbType),
                new Datadog.Trace.Tagging.Property<AerospikeTags, string?>("db.aerospike.key", t => t.Key),
                new Datadog.Trace.Tagging.Property<AerospikeTags, string?>("db.aerospike.namespace", t => t.Namespace),
                new Datadog.Trace.Tagging.Property<AerospikeTags, string?>("db.aerospike.setname", t => t.SetName),
                new Datadog.Trace.Tagging.Property<AerospikeTags, string?>("db.aerospike.userkey", t => t.UserKey)
);

        protected override Datadog.Trace.Tagging.IProperty<string?>[] GetAdditionalTags()
        {
             return AerospikeTagsProperties;
        }

        protected override int WriteAdditionalTags(ref byte[] bytes, ref int offset, ITagProcessor[] tagProcessors)
        {
            var count = 0;
            if (SpanKind != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, SpanKindBytes, SpanKind, tagProcessors);
            }

            if (InstrumentationName != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, InstrumentationNameBytes, InstrumentationName, tagProcessors);
            }

            if (DbType != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, DbTypeBytes, DbType, tagProcessors);
            }

            if (Key != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, KeyBytes, Key, tagProcessors);
            }

            if (Namespace != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, NamespaceBytes, Namespace, tagProcessors);
            }

            if (SetName != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, SetNameBytes, SetName, tagProcessors);
            }

            if (UserKey != null)
            {
                count++;
                WriteTag(ref bytes, ref offset, UserKeyBytes, UserKey, tagProcessors);
            }

            return count + base.WriteAdditionalTags(ref bytes, ref offset, tagProcessors);
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

            if (DbType != null)
            {
                sb.Append("db.system (tag):")
                  .Append(DbType)
                  .Append(',');
            }

            if (Key != null)
            {
                sb.Append("db.aerospike.key (tag):")
                  .Append(Key)
                  .Append(',');
            }

            if (Namespace != null)
            {
                sb.Append("db.aerospike.namespace (tag):")
                  .Append(Namespace)
                  .Append(',');
            }

            if (SetName != null)
            {
                sb.Append("db.aerospike.setname (tag):")
                  .Append(SetName)
                  .Append(',');
            }

            if (UserKey != null)
            {
                sb.Append("db.aerospike.userkey (tag):")
                  .Append(UserKey)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}
