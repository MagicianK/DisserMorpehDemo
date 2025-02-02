using Scellecs.Morpeh;
using UnityEngine;

public class Starter : MonoBehaviour
{
    World world;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        world = World.Default;

        var move = new SysMove();
        var input = new SysInput();
        var bullet = new SysBullet();
        var shoot = new SysShoot();
        var enemy = new SysEnemy();
        var damage = new SysDamage();

        var systemsGroup = world.CreateSystemsGroup();
        systemsGroup.AddSystem(input);
        systemsGroup.AddSystem(move);
        systemsGroup.AddSystem(bullet);
        systemsGroup.AddSystem(shoot);
        systemsGroup.AddSystem(enemy);
        systemsGroup.AddSystem(damage);

        world.AddSystemsGroup(order: 0, systemsGroup);
    }
}
