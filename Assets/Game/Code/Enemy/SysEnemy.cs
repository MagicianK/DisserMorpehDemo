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
    private Stash<Movement> moveStash;
    private Stash<Scanner> scanners;
    private Stash<Shoot> shoots;

    public void Dispose()
    {
    }

    public void OnAwake()
    {
        this.filter = this.World.Filter.With<Movement>().With<EnemyTag>().Build();
        moveStash = World.GetStash<Movement>();
        scanners = World.GetStash<Scanner>();
        shoots = World.GetStash<Shoot>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var move = ref moveStash.Get(entity);
            ref var scanner = ref scanners.Get(entity);
            var pos = scanner.enemyGo.transform.position;
            move.direction = Vector2.zero;
            if (!scanner.lost)
                move.direction = (new Vector2(pos.x, pos.y) - move.rb.position).normalized;
            shoots.Set(entity);
            move.rb.transform.up = new Vector2(pos.x, pos.y) - move.rb.position;
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

