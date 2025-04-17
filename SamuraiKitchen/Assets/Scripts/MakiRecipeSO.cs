using UnityEngine;

[CreateAssetMenu(fileName = "MakiRecipeSO", menuName = "Scriptable Objects/MakiRecipeSO")]
public class MakiRecipeSO : ScriptableObject
{
    public KitcheObjectSO input1;
    public KitcheObjectSO input2;
    public KitcheObjectSO input3;

    public KitcheObjectSO outputItem;

    public int mixingProgressMax;
}
