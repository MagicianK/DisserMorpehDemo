using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysDamage : ISystem {
    public World World { get; set; }
    private Stash<Damage> damages;
    private Stash<Health> healths;
    private Filter filter;
    
    public void Dispose()
    {

    }
    public void OnAwake() {
        damages = World.GetStash<Damage>();
        healths = World.GetStash<Health>();
        this.filter = this.World.Filter.With<Health>().With<Damage>().Build();
    }

    public void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter)
        {
            ref var damage = ref damages.Get(entity);
            ref var health = ref healths.Get(entity);
            health.health -= damage.damage;
            damages.Remove(entity);
            if (health.health <= 0)
                World.RemoveEntity(entity);
        }
    }
}