using System;
using System.Reflection;

namespace SignalFx.Tracing.DuckTyping
{
    /// <summary>
    /// Duck kind
    /// </summary>
    public enum DuckKind
    {
        /// <summary>
        /// Property
        /// </summary>
        Property,

        /// <summary>
        /// Field
        /// </summary>
        Field
    }

    /// <summary>
    /// Duck attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]
    public class DuckAttribute : Attribute
    {
        /// <summary>
        /// Default BindingFlags
        /// </summary>
        public const BindingFlags DefaultFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

        /// <summary>
        /// Gets or sets the underlying type member name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets duck kind
        /// </summary>
        public DuckKind Kind { get; set; } = DuckKind.Property;

        /// <summary>
        /// Gets or sets the binding flags
        /// </summary>
        public BindingFlags BindingFlags { get; set; } = DefaultFlags;

        /// <summary>
        /// Gets or sets the generic parameter type names definition for a generic method call (required when calling generic methods and instance type is non public)
        /// </summary>
        public string[] GenericParameterTypeNames { get; set; }

        /// <summary>
        /// Gets or sets the parameter type names of the target method (optional / used to disambiguation)
        /// </summary>
        public string[] ParameterTypeNames { get; set; }

        /// <summary>
        /// Gets or sets the explicit interface type name
        /// </summary>
        public string ExplicitInterfaceTypeName { get; set; }
    }
}
