using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using UnityEngine;
using System.Collections.Generic;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class Trigger : MonoProvider<Scanner> {

    protected override void Initialize()
    {
        GetData().lost = true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<EntityProvider>() == null) return;
        GetData().enemy = other.GetComponent<EntityProvider>().Entity;
        GetData().enemyGo = other.gameObject;
        GetData().lost = false;
        Debug.Log($"{other.name}");
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<EntityProvider>() == null) return;
        if(GetData().enemy == other.GetComponent<EntityProvider>().Entity)
        {
            GetData().lost = true;
        }

    }
}

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Scanner : IComponent {
    [HideInInspector] public Entity enemy;
    [HideInInspector] public GameObject enemyGo;
    [HideInInspector] public bool lost;
}