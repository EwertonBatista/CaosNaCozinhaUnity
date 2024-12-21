using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;
    private PlayerInputActions playerInputActions;

    public enum Binding
    {
        Move_up, Move_down, Move_left, Move_right, Interact, InteractAlternate, Pause, Gamepad_Interact, Gamepad_InteractAlternate, Gamepad_Pause
    }

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();

        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_up: return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_down: return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_left: return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_right: return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact: return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate: return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause: return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            case Binding.Gamepad_Interact: return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_InteractAlternate: return playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Binding.Gamepad_Pause: return playerInputActions.Player.Pause.bindings[1].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_up: inputAction = playerInputActions.Player.Move; bindingIndex = 1; break;
            case Binding.Move_down: inputAction = playerInputActions.Player.Move; bindingIndex = 2; break;
            case Binding.Move_left: inputAction = playerInputActions.Player.Move; bindingIndex = 3; break;
            case Binding.Move_right: inputAction = playerInputActions.Player.Move; bindingIndex = 4; break;
            case Binding.Interact: inputAction = playerInputActions.Player.Interact; bindingIndex = 0; break;
            case Binding.InteractAlternate: inputAction = playerInputActions.Player.InteractAlternate; bindingIndex = 0; break;
            case Binding.Pause: inputAction = playerInputActions.Player.Pause; bindingIndex = 0; break;
            case Binding.Gamepad_Interact: inputAction = playerInputActions.Player.Interact; bindingIndex = 1; break;
            case Binding.Gamepad_InteractAlternate: inputAction = playerInputActions.Player.InteractAlternate; bindingIndex = 1; break;
            case Binding.Gamepad_Pause: inputAction = playerInputActions.Player.Pause; bindingIndex = 1; break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
        .OnComplete(callback => { 
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        })
        .Start();
    }
}
