namespace SignalFx.Tracing.DuckTyping.Tests.Fields.TypeChaining.ProxiesDefinitions
{
    public interface IObscureDuckType
    {
        [Duck(Name = "_publicStaticReadonlySelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject PublicStaticReadonlySelfTypeField { get; }

        [Duck(Name = "_internalStaticReadonlySelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject InternalStaticReadonlySelfTypeField { get; }

        [Duck(Name = "_protectedStaticReadonlySelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject ProtectedStaticReadonlySelfTypeField { get; }

        [Duck(Name = "_privateStaticReadonlySelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject PrivateStaticReadonlySelfTypeField { get; }

        // *

        [Duck(Name = "_publicStaticSelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject PublicStaticSelfTypeField { get; set; }

        [Duck(Name = "_internalStaticSelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject InternalStaticSelfTypeField { get; set; }

        [Duck(Name = "_protectedStaticSelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject ProtectedStaticSelfTypeField { get; set; }

        [Duck(Name = "_privateStaticSelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject PrivateStaticSelfTypeField { get; set; }

        // *

        [Duck(Name = "_publicReadonlySelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject PublicReadonlySelfTypeField { get; }

        [Duck(Name = "_internalReadonlySelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject InternalReadonlySelfTypeField { get; }

        [Duck(Name = "_protectedReadonlySelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject ProtectedReadonlySelfTypeField { get; }

        [Duck(Name = "_privateReadonlySelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject PrivateReadonlySelfTypeField { get; }

        // *

        [Duck(Name = "_publicSelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject PublicSelfTypeField { get; set; }

        [Duck(Name = "_internalSelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject InternalSelfTypeField { get; set; }

        [Duck(Name = "_protectedSelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject ProtectedSelfTypeField { get; set; }

        [Duck(Name = "_privateSelfTypeField", Kind = DuckKind.Field)]
        IDummyFieldObject PrivateSelfTypeField { get; set; }
    }
}
