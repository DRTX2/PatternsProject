using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

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
        _inputActions.Player.Enable();
    }

    private void Update()
    {
        float x = _inputActions.Player.Move.ReadValue<Vector2>().x;
        bool running = _inputActions.Player.Run.IsPressed();

        _inputReciver.OnMoveInput(x);
        _inputReciver.OnRunInput(running);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            float direction = context.ReadValue<Vector2>().x;
            _inputReciver.OnMoveInput(direction);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _inputReciver.OnJumpInput();
        }
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
