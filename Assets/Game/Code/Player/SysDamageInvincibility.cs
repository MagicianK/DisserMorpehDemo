using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using TriInspector;

public struct Invincible : IComponent
{
    [ReadOnly] public float timer;
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysDamageInvincibility : ISystem {
    public World World { get; set; }
    private Stash<Health> healths;
    private Stash<Invincible> invincibles;
    private Stash<DeadTag> deads;
    private Stash<Movement> movements;
    private Filter filter;
    
    public void Dispose()
    {

    }
    public void OnAwake() {
        healths = World.GetStash<Health>();
        invincibles = World.GetStash<Invincible>();
        deads = World.GetStash<DeadTag>();
        movements = World.GetStash<Movement>();
        this.filter = this.World.Filter.With<Health>().With<Player>().Without<Invincible>().Build();
    }

    public void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter)
        {
            ref var health = ref healths.Get(entity);
            if (health.damages.Count > 0 )
            {
                invincibles.Add(entity);
                movements.Get(entity).rb.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}