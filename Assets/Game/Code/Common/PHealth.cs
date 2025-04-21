using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PHealth : MonoProvider<Health> {
    Stash<DamageBuffer> damageBuffers;
    protected override void Initialize()
    {
        damageBuffers = World.Default.GetStash<DamageBuffer>();
        GetData().health = GetData().maxHealth;
        var damageBuffer = new DamageBuffer() { value = new List<Damage>()};
        Entity.SetComponent(damageBuffer);
        Debug.Log($"Count: {damageBuffers.Length}");
    }
}