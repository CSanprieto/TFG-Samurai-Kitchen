using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitcheObjectSO kitcheObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitcheObjectSO GetKitcheObjectSO(){
        return kitcheObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent){

        if(this.kitchenObjectParent != null){
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if(kitchenObjectParent.HasKitchenObject()){
            Debug.LogError("IKitchenObjectParent already has a kitchen object!");
        }

        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
        
    }

    public IKitchenObjectParent GetKitchenObjectParent(){
        return kitchenObjectParent;
    }

    public void DestroySelf(){
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    // Function to call and spawn kitchen objects
    public static KitchenObject SpawnKitchenObject(KitcheObjectSO kitcheObjectSO, IKitchenObjectParent kitchenObjectParent){
        Transform kitchenObjetTrasnform = Instantiate(kitcheObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjetTrasnform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

}
