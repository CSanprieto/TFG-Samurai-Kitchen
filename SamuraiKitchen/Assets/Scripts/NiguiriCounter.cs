using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class NiguiriCounter : BaseCounter
{
    [SerializeField] private NiguiriRecipeSO[] mixRecipeSOArray;

    public class OnProgressChangedEventsArgs : EventArgs{
        public float progressNormalized;
    }

    public event EventHandler OnMix;

    private int mixingProgress;

    bool thereAre2ValidIngredients = false;

    public event EventHandler<OnProgressChangedEventsArgs> OnProgressChanged;

    [SerializeField] private GameObject mixingEffectPrefab;
    [SerializeField] private Transform effectSpawnPoint;

    [SerializeField] private Image pompImage; 
    [SerializeField] private float showDuration = 0.05f;


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

            NiguiriRecipeSO recipe = GetMixRecipeWithInputs(counterObjectSO, playerObjectSO);
            if (recipe != null){
                // Combinación válida, dejar el ingrediente del jugador en el counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
                mixingProgress = 0;
                thereAre2ValidIngredients = true;
                
            }
            // Si no forman receta, no hacer nada
        }
    }
}

 
private bool IsIngredientPartOfAnyRecipe(KitcheObjectSO input){
    foreach (NiguiriRecipeSO recipe in mixRecipeSOArray){
        if (recipe.input1 == input || recipe.input2 == input){
            return true;
        }
    }
    return false;
}

private NiguiriRecipeSO GetMixRecipeWithInputs(KitcheObjectSO first, KitcheObjectSO second){
    foreach(NiguiriRecipeSO recipe in mixRecipeSOArray){
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

// Function to mix the items in the counter
public override void UseItem(Player player){
    if(thereAre2ValidIngredients){
        KitchenObject[] kitchenObjects = GetComponentsInChildren<KitchenObject>();
        KitcheObjectSO obj1 = kitchenObjects[0].GetKitcheObjectSO();
        KitcheObjectSO obj2 = kitchenObjects[1].GetKitcheObjectSO();

        mixingProgress++;
        ShowPompaEffect();
        NiguiriRecipeSO mixRecipeSO = GetMixRecipeWithInputs(obj1, obj2);

        // Lanzar evento de progreso
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventsArgs {
            progressNormalized = (float)mixingProgress / mixRecipeSO.mixingProgressMax
        });

        OnMix?.Invoke(this, EventArgs.Empty);
        Instantiate(mixingEffectPrefab, effectSpawnPoint.position, Quaternion.identity);

        if (mixingProgress >= mixRecipeSO.mixingProgressMax){
            

            // Destruir los objetos existentes
            foreach (KitchenObject ko in kitchenObjects){
                ko.DestroySelf();
            }

            // Instanciar el nuevo objeto mezclado
            KitcheObjectSO outputKitchenObjectSO = GetOutPutItemForInputItem(obj1, obj2);
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            mixingProgress = 0;
            thereAre2ValidIngredients = false;
        }
    }
}



    // Check the received item and return the output item
    private KitcheObjectSO GetOutPutItemForInputItem(KitcheObjectSO inputKitchenObjectSO1, KitcheObjectSO inputKitchenObjectSO2){
        NiguiriRecipeSO mixRecipeSO = GetMixRecipeWithInputs(inputKitchenObjectSO1, inputKitchenObjectSO2);
        if(mixRecipeSO != null){
            Debug.Log("Recipe valid for given ingredientes returning output item!");
            return mixRecipeSO.outputItem;
        }else{
            Debug.Log("Cant take valid output for given ingredients");
            return null;
        }
    }

// Show pomp effect
public void ShowPompaEffect() {
    StartCoroutine(PompaRoutine());
}

private IEnumerator PompaRoutine() {
    // Posicionar la pompa en el punto deseado
    pompImage.gameObject.SetActive(true);
    RectTransform pompaTransform = pompImage.rectTransform;
    pompaTransform.position = effectSpawnPoint.position;

    

    pompaTransform.localScale = Vector3.zero;

    float time = 0f;
    while (time < showDuration) {
        time += Time.deltaTime;
        float progress = time / showDuration;

        float scale = Mathf.SmoothStep(0f, 1f, progress);
        pompaTransform.localScale = new Vector3(scale, scale, scale);

        yield return null;
    }

    yield return new WaitForSeconds(0.05f); // menos espera
    pompImage.gameObject.SetActive(false);
}
}


