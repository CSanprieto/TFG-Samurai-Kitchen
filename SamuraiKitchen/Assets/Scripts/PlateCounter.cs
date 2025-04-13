using System;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 5f;

    private int platesAmount;
    private int platesAmountMax = 5; 
    [SerializeField] private KitcheObjectSO plateKitchenObjectSO;

    // Create event to spawn more plates on top of counter
    public event EventHandler OnPlateSpawned;

    public event EventHandler OnPlateRemoved;


    // We watn to spawn plates each X seconds
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if(spawnPlateTimer > spawnPlateTimerMax){
            spawnPlateTimer = 0f;

            if(platesAmount < platesAmountMax){
                platesAmount++;

                // Call the plate even
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    // Function to give the plate to the player
    public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            // Player is carrying nothing so quit a plate and give it to player
            if(platesAmount > 0){
                platesAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
