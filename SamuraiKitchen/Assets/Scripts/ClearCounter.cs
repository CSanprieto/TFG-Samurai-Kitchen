using UnityEngine;

// Class for empty counters, player can put here kitchen objects
public class ClearCounter : BaseCounter
{
    
    // Method to handle interact
    public override void Interact(Player player)
    {
        // Check if there is any object in the counter
        if (!HasKitchenObject())
        {
            // If player is carriying something
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

        }
        else
        {
            // There is a kitchen object in the counter
            if (player.HasKitchenObject()){
                // Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitcheObjectSO()))
                    {
                        // If item is valid to put in a plate
                        KitchenObject kitchenObject = GetKitchenObject();

                        plateKitchenObject.SetKitchenObject(kitchenObject);

                        // Change parent to met the plate point
                        kitchenObject.transform.parent = plateKitchenObject.GetKitchenObjectFollowTransform();
                        kitchenObject.transform.localPosition = Vector3.zero;

                        // Clean counter
                        ClearKitchenObject();
                    }

                }
                else
                {
                    // Player is not carriying a plate but something more
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // The counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitcheObjectSO()))
                        {

                            KitchenObject kitchenObject = player.GetKitchenObject();
                            plateKitchenObject.SetKitchenObject(kitchenObject);

                            // Change parent to met the plate point
                            kitchenObject.transform.parent = plateKitchenObject.GetKitchenObjectFollowTransform();
                            kitchenObject.transform.localPosition = Vector3.zero;
                            player.ClearKitchenObject();
                        }
                    }
                }


            }
            else
            {
                // There is an object in the counter, we want to give it to the player if he is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }
}
