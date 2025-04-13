using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    [SerializeField] private PlateCounter platesCounter;

    private List<GameObject> plateAmountList;

    private void Awake()
    {
        plateAmountList = new List<GameObject>();
    }

    private void Start()
    {
        // Listen to events
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;

    }

    // When the event is launch we spawn a plate up to another
    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e){
       Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

       float plateDistance = 0.1f;
       plateVisualTransform.localPosition = new Vector3(0, plateDistance * plateAmountList.Count, 0);
       plateAmountList.Add(plateVisualTransform.gameObject);
    }

    // When we give a plate to the player we want to remove it
    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e){
        GameObject plateGameObject = plateAmountList[plateAmountList.Count - 1];
        plateAmountList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

}
