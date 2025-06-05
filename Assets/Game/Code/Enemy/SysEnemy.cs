using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysEnemy : ISystem {
    public World World { get; set; }
    private Filter filter;
    private Filter player;
    private Stash<Movement> moveStash;
    private Stash<Shoot> shoots;

    public void Dispose()
    {
    }

    public void OnAwake()
    {
        filter = World.Filter.With<Movement>().With<EnemyTag>().Build();
        player = World.Filter.With<Player>().Build();
        moveStash = World.GetStash<Movement>();
        shoots = World.GetStash<Shoot>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            if (player.IsEmpty()) return;
            ref var move = ref moveStash.Get(entity);
            move.direction = Vector2.zero;
            Vector2 playerPos = moveStash.Get(player.First()).rb.position;
            move.direction = (playerPos - move.rb.position).normalized;
            shoots.Set(entity);
            move.rb.transform.up = playerPos - move.rb.position;
        }
    }
}

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct EnemyTag : IComponent, IDisposable
{
    [HideInInspector] public GameObject enemy;
    public void Dispose()
    {
        GameObject.Destroy(enemy);
    }
}

