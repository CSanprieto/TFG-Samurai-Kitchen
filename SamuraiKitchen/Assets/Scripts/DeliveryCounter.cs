using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // Check what is holding the player
        if(player.HasKitchenObject()){
            
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){

                // Check if plate has valid recipe
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                // Only accept plates
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
