using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public static event EventHandler OnAnyObjectTrash;
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject()){
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrash?.Invoke(this, EventArgs.Empty);
        }
    }
}
