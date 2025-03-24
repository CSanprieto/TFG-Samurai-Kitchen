using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
[SerializeField] private float moveSpeed = 8f;
[SerializeField] private GameInput gameInput;

private bool isWalking;

    private Animator animator;

    void Start() {
        // Get player animator
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {

        PlayerMove();

    }

    // Method for control player moving
    void PlayerMove(){
        // Get input vector from Game Input
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Raycast to know what we hit something
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        
        if(canMove){
            // Move player if we can move
            transform.position += moveDir * moveDistance;
        }


        // if there are any move, then rotate player
        if (moveDir != Vector3.zero) {
            transform.forward = moveDir;
        }

        // Set isWalking value
        isWalking = moveDir != Vector3.zero;
        Debug.Log("Iswalking? = " + isWalking);

        // control animation speed
        float currentSpeed = moveDir.magnitude * moveSpeed;
        animator.SetFloat("MoveSpeed", currentSpeed);
    }

    
}
