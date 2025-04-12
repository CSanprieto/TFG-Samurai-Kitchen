using UnityEngine;
using UnityEngine.UI;


public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Image barImage; 
    [SerializeField] private CutCounter cutCounter;

    private void Start()
    {
        cutCounter.OnProgressChanged += CutCounter_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    // Event that give us the progress of cut object
    private void CutCounter_OnProgressChanged(object sender, CutCounter.OnProgressChangedEventsArgs e){
        barImage.fillAmount = e.progressNormalized;

        if(e.progressNormalized == 0f || e.progressNormalized == 1f){
            Hide();
        }else{
            Show();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

}
