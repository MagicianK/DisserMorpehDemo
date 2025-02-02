using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PMove : MonoProvider<Movement> {
    protected override void Initialize()
    {
        GetData().rb = GetComponent<Rigidbody2D>();
    }
}