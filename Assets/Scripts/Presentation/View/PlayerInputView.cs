using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

/// <summary>
/// PlayerInputView is responsible for handling player input in the game.
/// Abstracts the input logic from the game objects and uses a receiver to process the input.
/// Used to capture player actions such as movement, jumping, and attacks.
/// Uses Unity's new Input System for input handling.
/// </summary>

[RequireComponent(
    typeof(Rigidbody2D)
    )]
public class PlayerInputView : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    private IInputReceiver _inputReciver;

    [Inject]
    public void Construct(IInputReceiver inputReciver)
    {
        _inputReciver = inputReciver;
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Jump.started += OnJump;
        _inputActions.Player.Enable();
    }

    private void Update()
    {
        float x = _inputActions.Player.Move.ReadValue<Vector2>().x;
        bool running = _inputActions.Player.Run.IsPressed();

        _inputReciver.OnMoveInput(x);
        _inputReciver.OnRunInput(running);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        _inputReciver?.OnJumpInput();
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _inputReciver.OnMeleeAttackInput();
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _inputReciver.OnRangedAttackInput();
        }
    }
}
