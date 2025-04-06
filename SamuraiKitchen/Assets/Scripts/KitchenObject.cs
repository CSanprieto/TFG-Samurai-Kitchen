using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitcheObjectSO kitcheObjectSO;

    public KitcheObjectSO GetKitcheObjectSO(){
        return kitcheObjectSO;
    }


}
