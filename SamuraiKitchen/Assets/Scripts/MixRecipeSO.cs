using UnityEngine;

[CreateAssetMenu(fileName = "MixRecipeSO", menuName = "Scriptable Objects/MixRecipeSO")]
public class MixRecipeSO : ScriptableObject
{
    public KitcheObjectSO input1;
    public KitcheObjectSO input2;
    public KitcheObjectSO outputItem;

    public int mixingProgressMax;
}
