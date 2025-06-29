using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Class to handle single recipes UI on screen
public class DeliveryManagerSingleUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetKitchenObjectSO(KitcheObjectSO kitcheObjectSO)
    {
        recipeNameText.text = kitcheObjectSO.objectName;

        // Clean template
        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate)
            {
                continue;

            }
            else
            {
                Destroy(child.gameObject);
            }
        }

        Transform iconTransform = Instantiate(iconTemplate, iconContainer);
        iconTransform.gameObject.SetActive(true);
        iconTransform.GetComponent<Image>().sprite = kitcheObjectSO.sprite;
    }

}
