using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System.Linq;


[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysHealthCleaner : ICleanupSystem {
    public World World { get; set; }
    private Stash<Health> healths;
    private Stash<DeadTag> deads;
    private Filter filter;
    
    public void Dispose()
    {

    }
    public void OnAwake() {
        healths = World.GetStash<Health>();
        deads = World.GetStash<DeadTag>();
        this.filter = this.World.Filter.With<Health>().Build();
    }

    public void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter)
        {
            ref var health = ref healths.Get(entity);
            health.damages.Clear();
        }
    }
}