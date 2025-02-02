using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using System;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Bullet : IComponent, IDisposable {
    [HideInInspector] public GameObject value;
    [HideInInspector] public Rigidbody2D rb;
    public float speed;
    public float lifetime;
    public float time;
    public float damage;
    public void Dispose()
    {
        Debug.Log("Bullet disposed");
        GameObject.Destroy(value);
    }
}


[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Damage : IComponent {
    public float damage;
}


[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Health : IComponent {
    [HideInInspector] public float health;
    public float maxHealth;
}