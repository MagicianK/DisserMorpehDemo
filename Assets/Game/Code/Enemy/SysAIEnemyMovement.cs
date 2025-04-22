using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysAIEnemyMovement : ISystem {
    public World World { get; set; }
    private Filter filter;
    private Stash<Movement> moveStash;
    private Stash<AITargeting> scanners;
    private Stash<Shooter> shooters;
    private Stash<TransformComponent> transforms;
    private Stash<Attack> attacks;

    public void Dispose()
    {
    }

    public void OnAwake()
    {
        this.filter = this.World.Filter.Without<Player>().With<Movement>().With<AITargeting>() .With<Attack>().Build();
        moveStash = World.GetStash<Movement>();
        scanners = World.GetStash<AITargeting>();
        shooters = World.GetStash<Shooter>();
        transforms = World.GetStash<TransformComponent>();
        attacks = World.GetStash<Attack>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var move = ref moveStash.Get(entity);
            ref var scanner = ref scanners.Get(entity);
            var attackEntity = attacks.Get(entity).entity;
            var enemyTransform = transforms.Get(attackEntity).transform;
            ref var shooter = ref shooters.Get(entity);
            shooter.shoot = true;
            move.direction = (new Vector2(enemyTransform.position.x, enemyTransform.position.y) - move.rb.position).normalized;
            move.currentSpeed = move.speed;
        }
    }
}

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct EnemyTag : IComponent
{
}

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Attack : IComponent
{
    public Entity entity;
}