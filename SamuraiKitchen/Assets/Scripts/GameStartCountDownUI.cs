using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e){
        if(KitchenGameManager.Instance.IsGameInCountDownToStart()){
            Show();
        }else{
            Hide();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false); 
    }
}
