using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysShoot : ISystem {
    public World World { get; set; }

    private Filter filter;
    private Stash<Shooter> shooters;
    private Stash<Shoot> shooted;
    private Stash<Movement> moves;
    private Stash<Bullet> bullets;

    public void Dispose()
    {

    }
    public void OnAwake() {
        this.filter = World.Filter.With<Shooter>().With<Shoot>().Build();
        this.shooters = World.GetStash<Shooter>();
        this.moves = World.GetStash<Movement>();
        this.shooted = World.GetStash<Shoot>();
        this.bullets = World.GetStash<Bullet>();
    }

    public void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter)
        {
            ref var shooter = ref shooters.Get(entity);
            if (Time.time - shooter.time < shooter.reloadCooldown)
            {
                shooted.Remove(entity);
                continue;
            }
            ref var move = ref moves.Get(entity);
            CreateBullet(shooter);
            shooted.Remove(entity);
            shooter.time = Time.time;
        }
    }
    public void CreateBullet(Shooter shooter)
    {
        Rigidbody2D body = GameObject.Instantiate(shooter.bullet, shooter.socket.transform.position, shooter.shooter.transform.rotation);
        var bulletEntity = body.GetComponent<EntityProvider>();
        ref var bullet = ref bullets.Get(bulletEntity.Entity);
        body.AddForce(body.transform.up * bullet.speed, ForceMode2D.Impulse);
        bullet.time = Time.time;
    }   
}