using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using UnityEngine.InputSystem;
using UnityEngine;
using Disser;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SysInput : ISystem, Controls.IPlayerActions {
    public World World { get; set; }
    private Vector2 move;
    private Filter filter;
    private Stash<Movement> moveStash;
    private Stash<Shooter> shooters;
    private Controls controls;
    private Vector2 look;
    private bool Attack;
    public void Dispose()
    {
        controls.Player.Disable();
    }
    public void OnAwake()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();

        filter = World.Filter.With<Movement>().With<Player>().Build();
        moveStash = World.GetStash<Movement>();
        shooters = World.GetStash<Shooter>();
    }

    public void OnUpdate(float deltaTime)
    {

        foreach (var entity in this.filter)
        {
            ref var shooter = ref shooters.Get(entity);
            shooter.shoot = Attack;
            ref var movement = ref moveStash.Get(entity);
            movement.direction = move;
            movement.currentSpeed = movement.speed * move.magnitude;
            var mousePos = Camera.main.ScreenToWorldPoint(look);
            var transform = movement.rb.transform;
            Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            movement.direction = move * dir;
            Attack = false;
        }
    }
    #region INPUT
    public void OnAttack(InputAction.CallbackContext context)
    {
        Attack = true;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("Dash!");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        Debug.Log("Next");
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        Debug.Log("Previous");
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        Debug.Log("Sprint");
    }
    #endregion
}