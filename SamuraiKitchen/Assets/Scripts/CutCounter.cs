using UnityEngine;

public class CutCounter : BaseCounter
{

    [SerializeField] private KitcheObjectSO cutKitchenObjectSO;

    public override void Interact(Player player){
                 // Check if there is any object in the counter
        if(!HasKitchenObject()){

            // If player is carriying something
            if(player.HasKitchenObject()){
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            
        } else{
            // There is an object in the counter, we want to give it to the player if he is not carrying something
            if(!player.HasKitchenObject()){
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

        public override void UseItem(Player player){
        // Check if there is any object in the counter
        if(HasKitchenObject()){

            // cut item so first destroy it and instance the new cut object
            GetKitchenObject().DestroySelf();
            Transform kitchenObjetTrasnform = Instantiate(cutKitchenObjectSO.prefab);
            kitchenObjetTrasnform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } 
    }
}
