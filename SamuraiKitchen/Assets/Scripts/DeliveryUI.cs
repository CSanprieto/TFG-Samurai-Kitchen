using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Class to handle succes or error message on recipes delivered
public class DeliveryUI : MonoBehaviour
{

    [SerializeField] private Image BackgroundImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color succesColor;
    [SerializeField] private Color failedColor;

    private Coroutine hideCoroutine;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        gameObject.SetActive(false);
    }

 private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        ShowMessage("SUCCESS!", succesColor);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        ShowMessage("FAILED!", failedColor);
    }

    private void ShowMessage(string message, Color backgroundColor)
    {
        // Show deliver message
        gameObject.SetActive(true);
        BackgroundImage.color = backgroundColor;
        messageText.text = message;

        // Stop coroutine
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        // Init coroutine
        hideCoroutine = StartCoroutine(HideMessageAfterDelay(2f));
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
