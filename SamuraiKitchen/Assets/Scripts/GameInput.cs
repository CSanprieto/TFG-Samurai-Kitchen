using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{

private PlayerInputActions playerInputActions;
    // We control player movement through player input actions where we have created Move with ASWD

    public event EventHandler OnInteractAction;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    // we separate the input from the player
    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }

    // event to handle interaction
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        // It means that there is some suscribe to the event
        OnInteractAction ? .Invoke(this, EventArgs.Empty);
        
        
    }
}

