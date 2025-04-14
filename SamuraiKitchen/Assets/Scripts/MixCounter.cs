using UnityEngine;
using System;

public class MixCounter : BaseCounter
{
    [SerializeField] private MixRecipeSO[] mixRecipeSOArray;

    public class OnProgressChangedEventsArgs : EventArgs{
        public float progressNormalized;
    }


public override void Interact(Player player){
    // Si ya hay dos ingredientes válidos en el counter, no se puede interactuar
    if (HasTwoValidIngredients()){
        return;
    }
    if (!HasKitchenObject()){
        // El counter está vacío
        if (player.HasKitchenObject()){
            KitcheObjectSO playerObjectSO = player.GetKitchenObject().GetKitcheObjectSO();

            // Si el ingrediente del jugador es parte de alguna receta, puede dejarlo
            if (IsIngredientPartOfAnyRecipe(playerObjectSO)){
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
    } else {
        // El counter tiene un objeto
        if (!player.HasKitchenObject()){
            // El jugador está vacío: darle el objeto del counter
            GetKitchenObject().SetKitchenObjectParent(player);
        } else {
            // Ambos tienen objetos, verificar si combinan en una receta
            KitcheObjectSO counterObjectSO = GetKitchenObject().GetKitcheObjectSO();
            KitcheObjectSO playerObjectSO = player.GetKitchenObject().GetKitcheObjectSO();

            MixRecipeSO recipe = GetMixRecipeWithInputs(counterObjectSO, playerObjectSO);
            if (recipe != null){
                // Combinación válida, dejar el ingrediente del jugador en el counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            // Si no forman receta, no hacer nada
        }
    }
}

 
private bool IsIngredientPartOfAnyRecipe(KitcheObjectSO input){
    foreach (MixRecipeSO recipe in mixRecipeSOArray){
        if (recipe.input1 == input || recipe.input2 == input){
            return true;
        }
    }
    return false;
}

private MixRecipeSO GetMixRecipeWithInputs(KitcheObjectSO first, KitcheObjectSO second){
    foreach(MixRecipeSO recipe in mixRecipeSOArray){
        if((recipe.input1 == first && recipe.input2 == second) ||
           (recipe.input1 == second && recipe.input2 == first)){
            return recipe;
        }
    }
    return null;
}

private bool HasTwoValidIngredients(){
    KitchenObject[] kitchenObjects = GetComponentsInChildren<KitchenObject>();
    if (kitchenObjects.Length != 2) return false;

    KitcheObjectSO obj1 = kitchenObjects[0].GetKitcheObjectSO();
    KitcheObjectSO obj2 = kitchenObjects[1].GetKitcheObjectSO();

    return GetMixRecipeWithInputs(obj1, obj2) != null;
}
}
