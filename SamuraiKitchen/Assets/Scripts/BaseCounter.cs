using System;
using UnityEngine;

// Class for basic counter that has common methods and fields, shared by all counters
public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    // Event launched to know if any kitchen object is placed here
    public static event EventHandler OnAnyObjectPlacedHere;

    public virtual void Interact(Player player)
    {
        // Each counter will interact
    }

    public virtual void UseItem(Player player)
    {
        // Each counter will use item
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            // If kitchen object is placed here, launch event
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
