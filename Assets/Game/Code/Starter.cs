using Scellecs.Morpeh;
using UnityEngine;

public class Starter : MonoBehaviour
{
    World world;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        world = World.Default;
    
        var obstacleAvoidance = new SysObstacleAvoidance();
        var deadRemove = new SysDeadRemove();
        var move = new SysMove();
        var input = new SysInput();
        var bullet = new SysBullet();
        var shoot = new SysShoot();
        var playerSenseAI = new SysPlayerSenseAI();
        var aiMovement = new SysAIEnemyMovement();
        var damage = new SysDamage();
        var damageClear = new SysDamageClear();

        var systemsGroup = world.CreateSystemsGroup();
        systemsGroup.AddSystem(input);
        systemsGroup.AddSystem(playerSenseAI);
        systemsGroup.AddSystem(aiMovement);
        systemsGroup.AddSystem(obstacleAvoidance);
        systemsGroup.AddSystem(bullet);
        systemsGroup.AddSystem(shoot);
        systemsGroup.AddSystem(damage);
        systemsGroup.AddSystem(move);
        systemsGroup.AddSystem(deadRemove);
        systemsGroup.AddSystem(damageClear);

        world.AddSystemsGroup(order: 0, systemsGroup);
    }
}
