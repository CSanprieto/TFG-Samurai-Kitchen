using UnityEngine;

public class GameInput : MonoBehaviour
{

private PlayerInputActions playerInputActions;
    // We control player movement through player input actions where we have created Move with ASWD
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    // we separate the input from the player
    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
