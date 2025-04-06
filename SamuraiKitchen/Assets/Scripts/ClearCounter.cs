using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitcheObjectSO kitcheObjectSO;
    [SerializeField] private Transform counterTopPoint;

    public void Interact(){
        Debug.Log("hey interacting with clear counter");
        Transform kitchenObjetTrasnform = Instantiate(kitcheObjectSO.prefab, counterTopPoint);
        kitchenObjetTrasnform.localPosition = Vector3.zero;
    }
}
