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
        this.filter = this.World.Filter.With<Movement>().Without<Player>().Build();
        moveStash = World.GetStash<Movement>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {

            ref var move = ref moveStash.Get(entity);
            Quaternion targetRotation = Quaternion.LookRotation(move.rb.transform.forward, move.direction);
            Quaternion rotation = Quaternion.RotateTowards(move.rb.transform.rotation, targetRotation, move.rotationSpeed * Time.deltaTime);

            move.rb.SetRotation(rotation);
            var transform = move.rb.transform;
            move.rb.linearVelocity = transform.up * move.currentSpeed;
            move.currentSpeed = 0;        
        }
    }
}