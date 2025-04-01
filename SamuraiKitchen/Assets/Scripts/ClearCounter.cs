using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private Transform ricePrefab;
    [SerializeField] private Transform counterTopPoint;

    public void Interact(){
        Debug.Log("hey interacting with clear counter");
        Transform ricePrefabTransform = Instantiate(ricePrefab, counterTopPoint);
        ricePrefabTransform.localPosition = Vector3.zero;
    }
}
