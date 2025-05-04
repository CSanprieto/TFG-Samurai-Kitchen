using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    // Player property
    public static Player Instance { get; private set;}
    

    // Player event to know which counter we have selected
    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    public event EventHandler OnPickedSomething;
    
    // Serialized fields
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    // Player variabless 
    private Animator animator;
    private Vector3 lastInteractDir;

    private BaseCounter selectedCounter;

    private KitchenObject kitchenObject;

    // Awake method
    private void Awake()
    {
        if(Instance != null){
            Debug.Log("There are more than one player instances!");
        }

        Instance = this;
    }

    // Start method
    void Start() {
        // Get player animator
        animator = GetComponent<Animator>(); 
        // Get events
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnUseItemAction += GameInput_OnUseItemAction;
    }

    // Update method
    void Update()
    {

        PlayerMove();
        PlayerInteractions();

    }

    // Handle interact event
    private void GameInput_OnInteractAction(object sender, System.EventArgs e){
        if(selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }

    // Handle use item event
    private void GameInput_OnUseItemAction(object sender, System.EventArgs e){
        if(selectedCounter != null){
            selectedCounter.UseItem(this);
        }
    }

    // Method for control player moving
    void PlayerMove(){

        // avoid player move if game is not playing
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;

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

        // Add ecape to quit the game
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


    // Method to handle interaction
    private void PlayerInteractions(){
        // Get input vector from Game Input
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir != Vector3.zero){
            lastInteractDir = moveDir;
        }

        // Check if we hit something
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)){
            // Check for the object we are facing
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                // Has clear counter
                if(baseCounter != selectedCounter){
                    SetSelectedCounter(baseCounter);

                }
            }
            else{
                SetSelectedCounter(null);
            }
        } else{
           SetSelectedCounter(null);
        } 

    }


    // Method to set the selected counter
    private void SetSelectedCounter( BaseCounter selectedCounter){
        this.selectedCounter = selectedCounter;

                    // Launch selected counter event
                    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs{
                        selectedCounter = selectedCounter
                    });
    }

    public Transform GetKitchenObjectFollowTransform(){
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null){
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
}
