using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Shooter : IComponent {
    public GameObject shooter;
    public GameObject socket;
    public Rigidbody2D bullet;
    [HideInInspector] public float time;
    [HideInInspector] public bool shoot;
    public float reloadCooldown;
}