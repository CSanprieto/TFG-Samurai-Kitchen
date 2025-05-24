using System;
using UnityEngine;

// Class for counters with the ingredients
public class ContainerCounter : BaseCounter
{

    [SerializeField] private KitcheObjectSO kitcheObjectSO;

    // Make event to know when player grab objects
    public event EventHandler OnPlayerGrabObject;

    // Interact with player to give him the ingredient
    public override void Interact(Player player)
    {
        // If player is not carriying something them give the object
        if (!player.HasKitchenObject())
        {
            // Give item to the player
            KitchenObject.SpawnKitchenObject(kitcheObjectSO, player);
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
