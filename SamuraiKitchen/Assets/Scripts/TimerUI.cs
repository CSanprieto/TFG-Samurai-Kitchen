using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{

    [SerializeField] private Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = KitchenGameManager.Instance.GetGameTimerNormalized();
    }
}
