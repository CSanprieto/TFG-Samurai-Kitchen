using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CutCounter.OnCutSound += Counter_OnMixSound;
        MakiCounter.OnMakiMixSound += Counter_OnMixSound;
        NiguiriCounter.OnMixSound += Counter_OnMixSound;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += Counter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrash += TrashCounter_OnAnyObjectTrash;
        PlateKitchenObject.OnAnyItemAdded += PlateKitchenObject_OnAnyItemAdded;
    }


    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f){
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipList, Vector3 position, float volume = 1f){
        PlaySound(audioClipList[Random.Range(0, audioClipList.Length)], position, volume);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.deliverySuccess, Camera.main.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.deliveryFail, Camera.main.transform.position);
    }

    private void Counter_OnMixSound(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.chop, Camera.main.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.objectPickup, Camera.main.transform.position);
    }
    private void Counter_OnAnyObjectPlacedHere(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.objectDrop, Camera.main.transform.position);
    }
    private void TrashCounter_OnAnyObjectTrash(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.trash, Camera.main.transform.position);
    }

    private void PlateKitchenObject_OnAnyItemAdded(object sender, System.EventArgs e){
        PlaySound(audioClipRefsSO.objectDrop, Camera.main.transform.position);
    }
}
