using Unity.VisualScripting;
using UnityEngine;

// Class to handle recipes UI on screen
public class DeliveryManagerUI : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // Clean template
        foreach (Transform child in container)
        {
            if (child == recipeTemplate)
            {
                continue;

            }
            else
            {
                Destroy(child.gameObject);
            }
        }


        foreach (KitcheObjectSO kitcheObjectSO in DeliveryManager.Instance.GetwaitingRecipeList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetKitchenObjectSO(kitcheObjectSO);
        }
    }
}
