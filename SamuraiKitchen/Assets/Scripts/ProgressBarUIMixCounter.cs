using UnityEngine;
using UnityEngine.UI;


public class ProgressBarUIMixCounter : MonoBehaviour
{
    [SerializeField] private Image barImage; 
    [SerializeField] private MixCounter mixCounter;

    private void Start()
    {
        mixCounter.OnProgressChanged += MixCounter_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    // Event that give us the progress of cut object
    private void MixCounter_OnProgressChanged(object sender, MixCounter.OnProgressChangedEventsArgs e){
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
