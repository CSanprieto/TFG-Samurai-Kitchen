using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI totalRecipes;

    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        
        mainMenuButton.onClick.AddListener(() => {
            // Click function to load main menu
            SceneManager.LoadScene(0);
        });
    }


    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e){
        if(KitchenGameManager.Instance.IsGameOver()){
            Show();
            totalRecipes.text = DeliveryManager.Instance.getTotalRecipesSuccess().ToString();
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
