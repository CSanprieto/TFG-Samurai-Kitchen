using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
[SerializeField] private float moveSpeed = 5f;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>(); // Obtener el Animator del personaje
    }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) inputVector.y = +1;
        if (Input.GetKey(KeyCode.S)) inputVector.y = -1;
        if (Input.GetKey(KeyCode.A)) inputVector.x = -1;
        if (Input.GetKey(KeyCode.D)) inputVector.x = +1;

        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        // Mover al personaje
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Si hay movimiento, girar el personaje
        if (moveDir != Vector3.zero) {
            transform.forward = moveDir;
        }

        // Control de animaciones con MoveSpeed
        float currentSpeed = moveDir.magnitude * moveSpeed;
        animator.SetFloat("MoveSpeed", currentSpeed);
    }

    
}
