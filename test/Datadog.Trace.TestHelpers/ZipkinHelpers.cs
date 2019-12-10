// Modified by SignalFx
using System;
using System.Collections.Generic;
using System.Linq;
using Datadog.Trace;
using Datadog.Trace.ExtensionMethods;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Datadog.Trace.TestHelpers
{
    /// <summary>
    /// This class provides helpers to read Span and ServiceInfo data
    /// from serialized Zipkin representation.  Modeled from MsgPackHelpers.
    /// </summary>
    public static class ZipkinHelpers
    {
        public static ulong TraceId(this JToken obj)
        {
            return Convert.ToUInt64(obj.FirstDictionary()["traceId"].ToString(), 16);
        }

        public static ulong SpanId(this JToken obj)
        {
            return Convert.ToUInt64(obj.FirstDictionary()["id"].ToString(), 16);
        }

        public static ulong ParentId(this JToken obj)
        {
            return Convert.ToUInt64(obj.FirstDictionary()["parentId"].ToString(), 16);
        }

        public static string OperationName(this JToken obj)
        {
            return obj.FirstDictionary()["name"].ToString();
        }

        public static string ResourceName(this JToken obj)
        {
            string value;
            return Tags(obj).TryGetValue(Trace.Tags.ResourceName, out value) ? value : OperationName(obj);
        }

        public static string ServiceName(this JToken obj)
        {
            var localEndpoint = obj.FirstDictionary()["localEndpoint"];
            return localEndpoint["serviceName"].ToString();
        }

        public static long StartTime(this JToken obj)
        {
            return (long)obj.FirstDictionary()["timestamp"];
        }

        public static long Duration(this JToken obj)
        {
            return (long)obj.FirstDictionary()["duration"];
        }

        public static string Type(this JToken obj)
        {
            string value;
            return Tags(obj).TryGetValue(Trace.Tags.SpanType, out value) ? value : null;
        }

        public static bool Error(this JToken obj)
        {
            string value;
            return Tags(obj).TryGetValue("error", out value) ? value == "true" : false;
        }

        public static Dictionary<string, string> Tags(this JToken obj)
        {
            return obj.FirstDictionary()["tags"].ToObject<Dictionary<string, string>>();
        }

        public static void AssertSpanEqual(Span expected, JToken actual)
        {
            Assert.Equal(expected.Context.TraceId, actual.TraceId());
            Assert.Equal(expected.Context.SpanId, actual.SpanId());
            if (expected.Context.ParentId.HasValue)
            {
                Assert.Equal(expected.Context.ParentId, actual.ParentId());
            }

            Assert.Equal(expected.OperationName, actual.OperationName());
            Assert.Equal(expected.ResourceName, actual.ResourceName());
            Assert.Equal(Tracer.Instance.DefaultServiceName, actual.ServiceName());
            Assert.Equal(expected.Type, actual.Type());
            Assert.Equal(expected.StartTime.ToUnixTimeMicroseconds(), actual.StartTime());
            Assert.Equal(expected.Duration.ToMicroseconds(), actual.Duration());
            if (expected.Error)
            {
                Assert.True(actual.Error());
            }

            if (expected.Tags != null)
            {
                Assert.Equal(expected.Tags, actual.Tags());
            }
        }

        private static JToken FirstDictionary(this JToken obj)
        {
            if (obj is JArray)
            {
                return obj.ToList().First().FirstDictionary();
            }

            return obj;
        }
    }
}
