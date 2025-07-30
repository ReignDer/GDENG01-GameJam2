using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static InputSystem_Actions;


public interface IInputReader
{
    Vector2 Direction { get; }
    void EnablePlayerInput();
}

[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions, IInputReader
{
    public UnityAction<Vector2> Move = delegate { };
    public UnityAction<Vector2, bool> Look = delegate { };
    public UnityAction<bool> Jump = delegate { };
    public UnityAction<bool> Attack = delegate { };
    public UnityAction<bool> Interact = delegate { };
    public UnityAction<bool> Mirror = delegate { };
    public UnityAction<bool> Escape = delegate { };
    public UnityAction<bool> Crouch = delegate { };
    public UnityAction<bool> Previous = delegate { };
    public UnityAction<bool> Next = delegate { };
    public UnityAction<bool> Sprint = delegate { };
    
    
    public InputSystem_Actions inputActions;
    public Vector2 Direction => inputActions.Player.Move.ReadValue<Vector2>();
    public bool IsJumpKeyPressed() => inputActions.Player.Jump.IsPressed();

    public void EnablePlayerInput()
    {
        if (inputActions == null)
        {
            inputActions = new InputSystem_Actions();
            inputActions.Player.SetCallbacks(this);
        }
        inputActions.Enable();
    }

    public void DisablePlayerInput()
    {
        inputActions.Disable();
    }
    
     public void OnLook(InputAction.CallbackContext context)
     {
         Look.Invoke(context.ReadValue<Vector2>(), isDeviceMouse(context));
     }

     bool isDeviceMouse(InputAction.CallbackContext context)
     {
         return context.control.device.name == "Mouse";
     }

     public void OnAttack(InputAction.CallbackContext context)
     {
         switch (context.phase)
         {
             case InputActionPhase.Started:
                 Attack.Invoke(true);
                 break;
             case InputActionPhase.Canceled:
                 Attack.Invoke(false);
                 break;
             
         }
     }

     public void OnInteract(InputAction.CallbackContext context)
     {
         switch (context.phase)
         {
             case InputActionPhase.Started:
                 Interact.Invoke(true);
                 break;
             case InputActionPhase.Canceled:
                 Interact.Invoke(false);
                 break;
             
         }
     }

     public void OnCrouch(InputAction.CallbackContext context)
     {
         switch (context.phase)
         {
             case InputActionPhase.Started:
                 Crouch.Invoke(true);
                 break;
             case InputActionPhase.Canceled:
                 Crouch.Invoke(false);
                 break;
             
         }
     }

     public void OnJump(InputAction.CallbackContext context)
     {
         switch (context.phase)
         {
             case InputActionPhase.Started:
                 Jump.Invoke(true);
                 break;
             case InputActionPhase.Canceled:
                 Jump.Invoke(false);
                 break;
             
         }
     }

     public void OnPrevious(InputAction.CallbackContext context)
     {
         switch (context.phase)
         {
             case InputActionPhase.Started:
                 Previous.Invoke(true);
                 break;
             case InputActionPhase.Canceled:
                 Previous.Invoke(false);
                 break;
             
         }
     }

     public void OnNext(InputAction.CallbackContext context)
     {
         switch (context.phase)
         {
             case InputActionPhase.Started:
                 Next.Invoke(true);
                 break;
             case InputActionPhase.Canceled:
                 Next.Invoke(false);
                 break;
             
         }
     }

     public void OnSprint(InputAction.CallbackContext context)
     {
         switch (context.phase)
         {
             case InputActionPhase.Started:
                 Sprint.Invoke(true);
                 break;
             case InputActionPhase.Canceled:
                 Sprint.Invoke(false);
                 break;
             
         }
     }

     public void OnMirror(InputAction.CallbackContext context)
     {
         switch (context.phase)
         {
             case InputActionPhase.Started:
                 Mirror.Invoke(true);
                 break;
             case InputActionPhase.Canceled:
                 Mirror.Invoke(false);
                 break;
             
         }
     }

     public void OnPause(InputAction.CallbackContext context)
     {
         switch (context.phase)
         {
             case InputActionPhase.Started:
                 Escape.Invoke(true);
                 break;
             case InputActionPhase.Canceled:
                 Escape.Invoke(false);
                 break;
             
         }
     }

     public void OnMove(InputAction.CallbackContext context)
     {
         Move.Invoke(context.ReadValue<Vector2>());
     }


}
