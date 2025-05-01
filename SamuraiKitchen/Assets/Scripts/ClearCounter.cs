using UnityEngine;

public class ClearCounter : BaseCounter
{

    public override void Interact(Player player){
        // Check if there is any object in the counter
        if(!HasKitchenObject()){

            // If player is carriying something
            if(player.HasKitchenObject()){
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            
        } else{
            // There is a kitchen object here
            if(player.HasKitchenObject()){

                // Player is carrying something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    // Player is holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitcheObjectSO())){

                        
                        // Mover el kitchenObject (nigiri, maki, etc.) al plato
                        KitchenObject kitchenObject = GetKitchenObject(); 
                    
                        plateKitchenObject.SetKitchenObject(kitchenObject);
                        
                        // Cambiar el parent visual para que siga el punto del plato
                        kitchenObject.transform.parent = plateKitchenObject.GetKitchenObjectFollowTransform();
                        kitchenObject.transform.localPosition = Vector3.zero; // centrado en el holdPoint del plato
                    
                        // Limpiar el counter
                        ClearKitchenObject();
                    }
                    
                } else{
                    // Player is not carriying a plate but something more
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        // The counter is holding a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitcheObjectSO())){

                        KitchenObject kitchenObject = player.GetKitchenObject();
                        plateKitchenObject.SetKitchenObject(kitchenObject);
                        
                        // Cambiar el parent visual para que siga el punto del plato
                        kitchenObject.transform.parent = plateKitchenObject.GetKitchenObjectFollowTransform();
                        kitchenObject.transform.localPosition = Vector3.zero; // centrado en el holdPoint del plato
                        player.ClearKitchenObject();
                        }
                    }
                }

                
            } else{

                // There is an object in the counter, we want to give it to the player if he is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

}
