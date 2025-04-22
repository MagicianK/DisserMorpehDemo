using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System;
using System.Collections.Generic;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct ObstacleAvoidance : IComponent {
    [NonSerialized] public float obstacleAvoidanceCooldown;
    [NonSerialized] public Vector2 obstacleAvoidanceTargetDirection;
    public LayerMask obstacleLayerMask;
    public float obstacleCheckCircleRadius;
    public float obstacleCheckDistance;
    public float rotationSpeed;
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysObstacleAvoidance : IFixedSystem {
    public World World { get; set; }
    private Filter filter;
    private Stash<Movement> moveStash;
    private Stash<ObstacleAvoidance> avoidances;
    private Stash<TransformComponent> transforms;
    private List<RaycastHit2D> _obstacleCollisions = new List<RaycastHit2D>();
    public void Dispose()
    {
    }

    public void OnAwake()
    {
        this.filter = this.World.Filter.With<TransformComponent>().With<ObstacleAvoidance>().Build();
        moveStash = World.GetStash<Movement>();
        avoidances = World.GetStash<ObstacleAvoidance>();
        transforms = World.GetStash<TransformComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var avoidance = ref avoidances.Get(entity);
            ref var move = ref moveStash.Get(entity);
            avoidance.obstacleAvoidanceCooldown -= Time.deltaTime;

            var contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(avoidance.obstacleLayerMask);
            var transform = transforms.Get(entity).transform;
            int numberOfCollisions = Physics2D.CircleCast(
                transform.position,
                avoidance.obstacleCheckCircleRadius,
                transform.up,
                contactFilter,
                _obstacleCollisions,
                avoidance.obstacleCheckDistance);

            for (int index = 0; index < numberOfCollisions; index++)
            {
                var obstacleCollision = _obstacleCollisions[index];

                if (obstacleCollision.collider.gameObject == transform.gameObject)
                {
                    continue;
                }

                if (avoidance.obstacleAvoidanceCooldown <= 0)
                {
                    avoidance.obstacleAvoidanceTargetDirection = obstacleCollision.normal;
                    avoidance.obstacleAvoidanceCooldown = 0.5f;
                }

                var targetRotation = Quaternion.LookRotation(transform.forward, avoidance.obstacleAvoidanceTargetDirection);
                var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, avoidance.rotationSpeed * Time.deltaTime);

                move.direction = rotation * Vector2.up;
                move.currentSpeed = move.speed;
                break;
            }
        }
        _obstacleCollisions.Clear();
    }

}