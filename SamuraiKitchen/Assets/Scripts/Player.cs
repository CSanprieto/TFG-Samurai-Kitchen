using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    void Update(){

        Vector2 inputVector = new Vector2(0,0);

        if(Input.GetKey(KeyCode.W)){
            inputVector.y = +1;
        }
        if(Input.GetKey(KeyCode.S)){
            inputVector.y = -1;
        }
        if(Input.GetKey(KeyCode.A)){
            inputVector.x = -1;
        }
        if(Input.GetKey(KeyCode.D)){
            inputVector.x = +1;
        }
        
        // normalized
        inputVector = inputVector.normalized;
        // move direction
        Vector3 moveDir =  new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;


    }

    
}
