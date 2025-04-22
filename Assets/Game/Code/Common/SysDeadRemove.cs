using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysDeadRemove : ICleanupSystem {
    public World World { get; set; }
    private Filter filter;
    Stash<TransformComponent> transforms;
    public void Dispose()
    {

    }
    public void OnAwake() {
        this.filter = this.World.Filter.With<DeadTag>().With<TransformComponent>().Build();
        transforms = World.GetStash<TransformComponent>();
    }

    public void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter)
        {
            GameObject.Destroy(transforms.Get(entity).transform.gameObject);
            World.RemoveEntity(entity);
        }
    }
}