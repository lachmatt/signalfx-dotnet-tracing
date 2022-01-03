// <copyright file="IDatadogTracer.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

// Modified by Splunk Inc.

using System;
using Datadog.Trace.Configuration;
using Datadog.Trace.Propagation;
using Datadog.Trace.Sampling;

namespace Datadog.Trace
{
    /// <summary>
    /// Internal interface used for mocking the Tracer in <see cref="TraceContext"/>, its associated tests,
    /// and the AgentWriterTests
    /// </summary>
    internal interface IDatadogTracer
    {
        string DefaultServiceName { get; }

        ISampler Sampler { get; }

        ImmutableTracerSettings Settings { get; }

        void Write(ArraySegment<Span> span);
    }
}
