using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysBullet : ISystem {
    public World World { get; set; }
    private Stash<Colliding> collider;
    private Stash<Bullet> bullets;
    private Stash<DamageBuffer> damages;
    private Filter filter;
    
    public void Dispose()
    {

    }
    public void OnAwake() {
        collider = World.GetStash<Colliding>();
        damages = World.GetStash<DamageBuffer>();
        bullets = World.GetStash<Bullet>().AsDisposable();
        this.filter = this.World.Filter.With<Bullet>().Build();
    }

    public void OnUpdate(float deltaTime) {
        foreach (var bulletEntity in this.filter)
        {
            ref var collide = ref collider.Get(bulletEntity);
            ref var bullet = ref bullets.Get(bulletEntity);
            if (collide.collided)
            {
                if (!World.IsDisposed(collide.entity))
                {
                    var damageBuffer = damages.Get(collide.entity).value;
                    damageBuffer.Add(new Damage() { Value = bullet.damage});
                }
                World.RemoveEntity(bulletEntity);
            }
            if (Time.time - bullet.time > bullet.lifetime)
            {
                World.RemoveEntity(bulletEntity);
            }
        }
    }
}