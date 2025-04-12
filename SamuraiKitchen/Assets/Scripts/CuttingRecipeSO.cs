using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitcheObjectSO inputItem;
    public KitcheObjectSO outputItem;

    public int cuttingProgressMax;

}
