using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Class to handle delivery
public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance
    {
        get; private set;
    }

    // Delivery events
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    [SerializeField] List<KitcheObjectSO> recipeList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 5f;
    private int recipeMax = 4;

    private int totalRecipesSuccess;

    [SerializeField] List<KitcheObjectSO> waitingRecipeList;

    private void Awake()
    {
        Instance = this;
        waitingRecipeList = new List<KitcheObjectSO>();

    }

    private void Update()
    {
        // We add recipes each 5 seconds
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeList.Count < recipeMax)
            {
                KitcheObjectSO waitingRecipeSO = recipeList[UnityEngine.Random.Range(0, recipeList.Count)];
                waitingRecipeList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    // Method to handle when a recipe is delivered
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeList.Count; i++)
        {
            KitcheObjectSO kitcheObjectSO = waitingRecipeList[i];
            if (plateKitchenObject.GetKitchenObject().GetKitcheObjectSO().name.Equals(kitcheObjectSO.name))
            {
                // Plate item is on the waiting recipes
                waitingRecipeList.RemoveAt(i);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                totalRecipesSuccess++;
                return;
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        Debug.Log("Wrong recipe delivered!");
    }

    // method to get recipes waiting
    public List<KitcheObjectSO> GetwaitingRecipeList()
    {
        return waitingRecipeList;
    }

    // method to get total recipes to show on game over screen
    public int getTotalRecipesSuccess()
    {
        return totalRecipesSuccess;
    }
}
