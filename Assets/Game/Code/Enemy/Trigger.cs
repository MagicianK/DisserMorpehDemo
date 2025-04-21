using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class Trigger : MonoProvider<AITargeting> {
    
    void OnDrawGizmos()
    {
        Handles.CircleHandleCap(-1, transform.position, Quaternion.LookRotation(Vector2.up), GetData().Radius, EventType.Repaint);
    }
}

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct AITargeting : IComponent {
    [HideInInspector] public Entity enemy;
    public float Radius;
    public LayerMask SenseLayers;
    public float targetCheckDistance;

}