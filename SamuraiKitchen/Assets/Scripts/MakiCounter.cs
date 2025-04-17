using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class MakiCounter : BaseCounter
{
    [SerializeField] private MakiRecipeSO[] makiRecipeSOArray;

    public class OnProgressChangedEventsArgs : EventArgs{
        public float progressNormalized;
    }

    public event EventHandler OnMakiMix;

    private int mixingProgress;

    bool thereAre3ValidIngredients = false;

    public event EventHandler<OnProgressChangedEventsArgs> OnProgressChanged;

    [SerializeField] private GameObject mixingEffectPrefab;
    [SerializeField] private Transform effectSpawnPoint;

    [SerializeField] private Image pompImage; 
    [SerializeField] private float showDuration = 0.05f;


public override void Interact(Player player){
    // Si ya hay 3 ingredientes válidos en el counter, no se puede interactuar
    if (HasThreeValidIngredients()){
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
            // Obtener los ingredientes actuales del counter
            
            KitchenObject[] kitchenObjects = GetComponentsInChildren<KitchenObject>();
            Debug.Log("hay items en el counter : " + kitchenObjects.Length);
            if (kitchenObjects.Length == 1){
                // Hay 1 ingrediente en el counter
                Debug.Log("hay solo un item en el counter : " + kitchenObjects.Length);
                KitcheObjectSO obj1 = kitchenObjects[0].GetKitcheObjectSO();
                KitcheObjectSO obj2 = player.GetKitchenObject().GetKitcheObjectSO();
                Debug.Log("comprobamos si es valido el item para la receta");
                MakiRecipeSO recipe = GetMakiRecipeWithInputs(obj1, obj2, null); // solo dos por ahora

                if (recipe != null){
                    Debug.Log("el item que lleva el jugador es valido lo dejamos en el counter");
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    mixingProgress = 0;
                } else{
                    Debug.Log("el item que lleva el jugador no es valido no dejamos interactuar");
                }

            } else if (kitchenObjects.Length == 2){
                // Hay 2 ingredientes en el counter
                KitcheObjectSO obj1 = kitchenObjects[0].GetKitcheObjectSO();
                KitcheObjectSO obj2 = kitchenObjects[1].GetKitcheObjectSO();
                KitcheObjectSO obj3 = player.GetKitchenObject().GetKitcheObjectSO();

                MakiRecipeSO recipe = GetMakiRecipeWithInputs(obj1, obj2, obj3);

                if (recipe != null){
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    mixingProgress = 0;
                    thereAre3ValidIngredients = true;
                }
            }
            // Si hay más de 2 ya no permitimos añadir (lo controla HasThreeValidIngredients)
        }
    }
}

 
private bool IsIngredientPartOfAnyRecipe(KitcheObjectSO input){
    foreach (MakiRecipeSO recipe in makiRecipeSOArray){
        if (recipe.input1 == input || recipe.input2 == input || recipe.input3 == input){
            return true;
        }
    }
    return false;
}

private MakiRecipeSO GetMakiRecipeWithInputs(KitcheObjectSO first, KitcheObjectSO second, KitcheObjectSO third) {
    // Lista de ingredientes proporcionados (sin null)
    List<KitcheObjectSO> providedIngredients = new List<KitcheObjectSO>();
    if (first != null) providedIngredients.Add(first);
    if (second != null) providedIngredients.Add(second);
    if (third != null) providedIngredients.Add(third);

    // Crear diccionario con el conteo de cada ingrediente proporcionado
    Dictionary<KitcheObjectSO, int> providedCounts = new Dictionary<KitcheObjectSO, int>();
    foreach (var ingredient in providedIngredients) {
        if (providedCounts.ContainsKey(ingredient)) {
            providedCounts[ingredient]++;
        } else {
            providedCounts[ingredient] = 1;
        }
    }

    foreach (MakiRecipeSO recipe in makiRecipeSOArray) {
        List<KitcheObjectSO> recipeIngredients = new List<KitcheObjectSO> {
            recipe.input1, recipe.input2, recipe.input3
        };

        // Contar los ingredientes de la receta también
        Dictionary<KitcheObjectSO, int> recipeCounts = new Dictionary<KitcheObjectSO, int>();
        foreach (var ingredient in recipeIngredients) {
            if (recipeCounts.ContainsKey(ingredient)) {
                recipeCounts[ingredient]++;
            } else {
                recipeCounts[ingredient] = 1;
            }
        }

        // Validar que todos los ingredientes proporcionados estén en la receta con el mismo o menor conteo
        bool matches = true;
        foreach (var pair in providedCounts) {
            if (!recipeCounts.ContainsKey(pair.Key) || recipeCounts[pair.Key] < pair.Value) {
                matches = false;
                break;
            }
        }

        if (matches) {
            return recipe;
        }
    }

    return null;
}

private bool HasThreeValidIngredients(){
    KitchenObject[] kitchenObjects = GetComponentsInChildren<KitchenObject>();
    if (kitchenObjects.Length != 3) return false;

    KitcheObjectSO obj1 = kitchenObjects[0].GetKitcheObjectSO();
    KitcheObjectSO obj2 = kitchenObjects[1].GetKitcheObjectSO();
    KitcheObjectSO obj3 = kitchenObjects[1].GetKitcheObjectSO();

    return GetMakiRecipeWithInputs(obj1, obj2, obj3) != null;
}

// Function to mix the items in the counter
public override void UseItem(Player player){
    if(thereAre3ValidIngredients){
        KitchenObject[] kitchenObjects = GetComponentsInChildren<KitchenObject>();
        KitcheObjectSO obj1 = kitchenObjects[0].GetKitcheObjectSO();
        KitcheObjectSO obj2 = kitchenObjects[1].GetKitcheObjectSO();
        KitcheObjectSO obj3 = kitchenObjects[2].GetKitcheObjectSO();

        mixingProgress++;
        ShowPompaEffect();
        MakiRecipeSO mixRecipeSO = GetMakiRecipeWithInputs(obj1, obj2, obj3);

        // Lanzar evento de progreso
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventsArgs {
            progressNormalized = (float)mixingProgress / mixRecipeSO.mixingProgressMax
        });

        OnMakiMix?.Invoke(this, EventArgs.Empty);
        Instantiate(mixingEffectPrefab, effectSpawnPoint.position, Quaternion.identity);

        if (mixingProgress >= mixRecipeSO.mixingProgressMax){
            

            // Destruir los objetos existentes
            foreach (KitchenObject ko in kitchenObjects){
                ko.DestroySelf();
            }

            // Instanciar el nuevo objeto mezclado
            KitcheObjectSO outputKitchenObjectSO = GetOutPutItemForInputItem(obj1, obj2, obj3);
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            mixingProgress = 0;
            thereAre3ValidIngredients = false;
        }
    }
}



    // Check the received item and return the output item
    private KitcheObjectSO GetOutPutItemForInputItem(KitcheObjectSO inputKitchenObjectSO1, KitcheObjectSO inputKitchenObjectSO2, KitcheObjectSO inputKitchenObjectSO3){
        MakiRecipeSO mixRecipeSO = GetMakiRecipeWithInputs(inputKitchenObjectSO1, inputKitchenObjectSO2, inputKitchenObjectSO3);
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
