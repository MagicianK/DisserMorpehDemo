using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class Collision : MonoProvider<Colliding> {
    private void OnCollisionEnter2D(Collision2D other) {
        GetData().collided = true;
        if (other.gameObject.TryGetComponent<EntityProvider>(out var provider))
            GetData().entity = provider.Entity;
    }
}

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Colliding : IComponent {
    [HideInInspector] public bool collided;
    [HideInInspector] public Entity entity;

}