using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject , IKitchenObjectParent
{   

    // Valid recipes that can go on a plate
    [SerializeField] private List<KitcheObjectSO> validKitchenSOList;
    // List of kitchen objects SO
    private List<KitcheObjectSO> kitcheObjectSOList;

    private KitchenObject kitchenObject;

        [SerializeField] private Transform kitchenObjectHoldPoint;


    private void Awake()
    {
        kitcheObjectSOList = new List<KitcheObjectSO>();
    }


    public bool TryAddIngredient(KitcheObjectSO kitcheObjectSO){
        if(!validKitchenSOList.Contains(kitcheObjectSO)){
            // is not a valid item to add to the plate
            return false;
        }

        if(kitcheObjectSOList.Contains(kitcheObjectSO)){
            // Already contained in the plate
            return false;

        }else{
            // you can add it to the plate 
            kitcheObjectSOList.Add(kitcheObjectSO);
            return true;
        }

    }

    public Transform GetKitchenObjectFollowTransform(){
        return kitchenObjectHoldPoint;
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
