using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysMove : IFixedSystem {
    public World World { get; set; }
    private Filter filter;
    private Stash<Movement> moveStash;

    public void Dispose()
    {
    }

    public void OnAwake()
    {
        Debug.Log("Hello world");
        this.filter = this.World.Filter.With<Movement>().Build();
        moveStash = World.GetStash<Movement>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var move = ref moveStash.Get(entity);
            var transform = move.rb.transform;
            var posOffset = new Vector3(move.direction.x, move.direction.y, 0);
            move.rb.MovePosition(transform.position + posOffset * move.speed * deltaTime);
        }
    }
}