using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitcheObjectSO kitcheObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitcheObjectSO GetKitcheObjectSO(){
        return kitcheObjectSO;
    }

public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
    if (this.kitchenObjectParent != null) {
        this.kitchenObjectParent.ClearKitchenObject();
    }

    this.kitchenObjectParent = kitchenObjectParent;

    if (kitchenObjectParent.HasKitchenObject()) {
        Debug.LogWarning("IKitchenObjectParent already has a kitchen object!");
    }

    kitchenObjectParent.SetKitchenObject(this);
    transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();

    // Ahora controlamos si es un Plate
    if (this is PlateKitchenObject) {
        // The plate appear a bit down to avoid clipping with other objects
        transform.localPosition = new Vector3(0f, -0.1f, 0f); 
    } else {
        transform.localPosition = Vector3.zero;
    }
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

    // function
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject){
        if(this is PlateKitchenObject){
            // This kitchen object is a plate
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }else{
            // Not a plate
            plateKitchenObject = null;
            return false;
        }
    }
}
