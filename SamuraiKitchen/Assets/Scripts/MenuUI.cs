using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => {
            // Click function
            SceneManager.LoadScene(1);
        });
        
        exitButton.onClick.AddListener(() => {
            // Click function
            Application.Quit();
        });
    }
 
}
