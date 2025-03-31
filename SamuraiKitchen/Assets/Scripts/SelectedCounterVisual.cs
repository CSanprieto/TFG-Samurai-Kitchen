using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObjectBox;
        [SerializeField] private GameObject visualGameObjectTape;


    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedArgs e) {
        // If we are the same counter, we will show the visual
        if(e.selectedCounter == clearCounter){
            Show();
        } else{
            Hide();
        }
    }

    private void Show(){
        visualGameObjectBox.SetActive(true);
        visualGameObjectTape.SetActive(true);
    }

    private void Hide(){
        visualGameObjectBox.SetActive(false);
        visualGameObjectTape.SetActive(false);

    }
}
