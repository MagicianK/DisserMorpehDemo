using Scellecs.Morpeh;
using UnityEngine;

public class Starter : MonoBehaviour
{
    World world;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        world = World.Default;

        var systemsGroup = world.CreateSystemsGroup();
        systemsGroup.AddSystem(new SysInput());
        systemsGroup.AddSystem(new SysMove());
        systemsGroup.AddSystem(new SysBullet());
        systemsGroup.AddSystem( new SysShoot());
        systemsGroup.AddSystem(new SysEnemy());
        systemsGroup.AddSystem( new SysDamage());
        systemsGroup.AddSystem(new SysDead());
        systemsGroup.AddSystem(new SysDamageInvincibility());
        systemsGroup.AddSystem(new SysInvincibility());
        systemsGroup.AddSystem(new SysHealthCleaner());
        systemsGroup.AddSystem(new SysDead());
        world.AddSystemsGroup(order: 0, systemsGroup);
    }
}
