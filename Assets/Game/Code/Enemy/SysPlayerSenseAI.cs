using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System;
using Scellecs.Morpeh.Providers;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysPlayerSenseAI : ISystem {
    public World World { get; set; }
    private Filter filter;
    private Stash<AITargeting> scanners;
    private Stash<TransformComponent> transforms;
    RaycastHit2D[] hits = new RaycastHit2D[10];
    public void Dispose()
    {
    }

    public void OnAwake()
    {
        this.filter = this.World.Filter.With<AITargeting>().Without<Attack>().Build();
        scanners = World.GetStash<AITargeting>();
        transforms = World.GetStash<TransformComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            var scanner = scanners.Get(entity);
            var transform = transforms.Get(entity).transform;
            var contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(scanner.SenseLayers);
            int numberOfCollisions = Physics2D.CircleCast(
                transform.position,
                scanner.Radius,
                transform.up,
                contactFilter,
                hits,
                scanner.targetCheckDistance);
            for (int i = 0; i < numberOfCollisions; i++)
            {
                if (hits[i].collider.gameObject.TryGetComponent(out EntityProvider provider))
                {
                    if (provider.Entity.Has<Player>())
                    {
                        entity.SetComponent(new Attack(){
                            entity = provider.Entity
                        });
                        break;
                    }
                }
            }
        }
    }
}


