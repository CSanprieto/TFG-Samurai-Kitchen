using UnityEngine;
using UnityEngine.UI;


public class ProgressBarUIMakiCounter : MonoBehaviour
{
    [SerializeField] private Image barImage; 
    [SerializeField] private MakiCounter makiCounter;

    private void Start()
    {
        makiCounter.OnProgressChanged += MakiCounter_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    // Event that give us the progress of cut object
    private void MakiCounter_OnProgressChanged(object sender, MakiCounter.OnProgressChangedEventsArgs e){
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
