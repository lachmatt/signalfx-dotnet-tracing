//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
namespace Datadog.Trace.Vendors.ProtoBuf
{
    /// <summary>
    /// Represents the ability to deserialize values from an input of type <typeparamref name="TInput"/>
    /// </summary>
    internal interface IProtoInput<TInput>
    {
        /// <summary>
        /// Deserialize a value from the input
        /// </summary>
        T Deserialize<T>(TInput source, T value = default, object userState = null);
    }
}
