// <copyright file="ITags.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

// Modified by Splunk Inc.

using System.Collections.Generic;

namespace Datadog.Trace.Tagging
{
    internal interface ITags
    {
        List<KeyValuePair<string, double>> Metrics { get; }

        List<KeyValuePair<string, string>> GetAllTags();

        string GetTag(string key);

        void SetTag(string key, string value);

        void EnumerateTags<TProcessor>(ref TProcessor processor)
            where TProcessor : struct, IItemProcessor<string>;

        double? GetMetric(string key);

        void SetMetric(string key, double? value);

        void EnumerateMetrics<TProcessor>(ref TProcessor processor)
            where TProcessor : struct, IItemProcessor<double>;
    }
}
