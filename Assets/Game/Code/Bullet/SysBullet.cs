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
    private Stash<Damage> damages;
    private Filter filter;
    
    public void Dispose()
    {

    }
    public void OnAwake() {
        collider = World.GetStash<Colliding>();
        damages = World.GetStash<Damage>();
        bullets = World.GetStash<Bullet>().AsDisposable();
        this.filter = this.World.Filter.With<Bullet>().Build();
    }

    public void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter)
        {
            ref var collide = ref collider.Get(entity);
            ref var bullet = ref bullets.Get(entity);
            if (collide.collided)
            {
                damages.Set(collide.entity, new Damage(){
                    damage = bullet.damage,
                });
                World.RemoveEntity(entity);
            }
            if (Time.time - bullet.time > bullet.lifetime)
            {
                World.RemoveEntity(entity);
            }
        }
    }
}