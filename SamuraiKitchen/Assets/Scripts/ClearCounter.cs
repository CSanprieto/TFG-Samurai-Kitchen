using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private KitcheObjectSO kitcheObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact(Player player){
        
        // Add the kitchen object to the counter
        if(kitchenObject == null){
            Transform kitchenObjetTrasnform = Instantiate(kitcheObjectSO.prefab, counterTopPoint);
            kitchenObjetTrasnform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } else{
            // Give the object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }


    
    }

    public Transform GetKitchenObjectFollowTransform(){
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
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
