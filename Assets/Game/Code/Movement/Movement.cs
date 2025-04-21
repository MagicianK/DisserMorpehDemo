using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using TriInspector;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Movement : IComponent {
    [HideInInspector] public Vector2 direction;
    public float speed;
    [ReadOnly] public float currentSpeed;
    public float rotationSpeed;
    [HideInInspector] public Rigidbody2D rb;
}


[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct TransformComponent : IComponent {
    [HideInInspector] public Transform transform;
}
