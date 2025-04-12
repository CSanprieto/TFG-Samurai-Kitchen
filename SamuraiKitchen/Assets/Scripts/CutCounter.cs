using System;
using UnityEngine;

public class CutCounter : BaseCounter
{

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    public class OnProgressChangedEventsArgs : EventArgs{
        public float progressNormalized;
    }

    public event EventHandler OnCut;

    public event EventHandler<OnProgressChangedEventsArgs> OnProgressChanged;

    public override void Interact(Player player){
                 // Check if there is any object in the counter
        if(!HasKitchenObject()){

            // If player is carriying something
            if(player.HasKitchenObject()){
                // If the item can be cutted then let drop it in counter
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitcheObjectSO())){
                    // Drop item to counter
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    // Launch progress event
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitcheObjectSO());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventsArgs{
                        progressNormalized = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });

                }
                
            }
            
        } else{
            // There is an object in the counter, we want to give it to the player if he is not carrying something
            if(!player.HasKitchenObject()){
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

        public override void UseItem(Player player){
        // Check if there is any object in the counter and only if it can be cut
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitcheObjectSO())){
            // We get the output item first and increase cutting progress
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitcheObjectSO());

                    // Launch progress event
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventsArgs{
                        progressNormalized = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });

                    // Launch cut event
                    OnCut?.Invoke(this, EventArgs.Empty);

            // check if cutting progress is at max
            if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax){
                // cut item so first destroy it and instance the new cut object
                KitcheObjectSO outputKitchenObjectSO = GetOutPutItemForInputItem(GetKitchenObject().GetKitcheObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        } 
    }

    // Check the received item and return the output item
    private KitcheObjectSO GetOutPutItemForInputItem(KitcheObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipeSO != null){
            return cuttingRecipeSO.outputItem;
        }else{
            return null;
        }
    }

    // Method to let put the item on this counter only if you can cut the item (salmon, atun)
    private bool HasRecipeWithInput(KitcheObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
        
    }

    // Method to optain the outpu item for input item
    private CuttingRecipeSO  GetCuttingRecipeSOWithInput(KitcheObjectSO inputKitcheObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray){
            if(cuttingRecipeSO.inputItem == inputKitcheObjectSO){
                return cuttingRecipeSO;
            }
        }
        return null;

    }
}
