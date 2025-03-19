using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
[SerializeField] private float moveSpeed = 8f;
[SerializeField] private GameInput gameInput;
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
        
        // Move player
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // if there are any move, then rotate player
        if (moveDir != Vector3.zero) {
            transform.forward = moveDir;
        }

        // control animation speed
        float currentSpeed = moveDir.magnitude * moveSpeed;
        animator.SetFloat("MoveSpeed", currentSpeed);
    }

    
}
