using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System.Linq;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysDamage : ILateSystem {
    public World World { get; set; }
    private Stash<DamageBuffer> damages;
    private Stash<Health> healths;
    private Filter filter;
    
    public void Dispose()
    {

    }
    public void OnAwake() {
        damages = World.GetStash<DamageBuffer>();
        healths = World.GetStash<Health>();
        this.filter = this.World.Filter.With<Health>().With<DamageBuffer>().Build();
    }

    public void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter)
        {
            var damageBuffer = damages.Get(entity).value;
            ref var health = ref healths.Get(entity);
            var damage = damageBuffer?.Sum(x => x.Value) ?? 0;
            health.health -= damage;
            if (health.health <= 0)
                entity.SetComponent(new DeadTag());
        }
    }
}

public struct Damage {
    public float Value;
}