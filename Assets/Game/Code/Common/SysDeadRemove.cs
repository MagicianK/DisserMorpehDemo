using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysDeadRemove : ICleanupSystem {
    public World World { get; set; }
    private Filter filter;
    
    public void Dispose()
    {

    }
    public void OnAwake() {
        this.filter = this.World.Filter.With<DeadTag>().Build();
    }

    public void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter)
        {
            World.RemoveEntity(entity);
        }
    }
}