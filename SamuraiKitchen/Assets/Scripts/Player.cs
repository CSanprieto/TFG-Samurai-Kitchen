using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
[SerializeField] private float moveSpeed = 8f;
[SerializeField] private GameInput gameInput;

private bool isWalking;
    // Player variabless
    private Animator animator;
    private Vector3 lastInteractDir;

    void Start() {
        // Get player animator
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {

        PlayerMove();
        PlayerInteractions();

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

        // If we can not move 
        if(!canMove){
            // check if we can move on X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if(canMove){
                // It means we can only move on the X
                moveDir = moveDirX;
            } else{
                // cant move on X, try to move on Z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if(canMove){
                    // we can move only on Z
                    moveDir = moveDirZ;
                }
            }

        }
        
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

        // control animation speed
        float currentSpeed = moveDir.magnitude * moveSpeed;
        animator.SetFloat("MoveSpeed", currentSpeed);
    }

    private void PlayerInteractions(){
        // Get input vector from Game Input
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir != Vector3.zero){
            lastInteractDir = moveDir;
        }

        // Check if we hit something
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance)){
            Debug.Log(raycastHit.transform);
        } else{
            Debug.Log("-");
        }

    }

    
}
