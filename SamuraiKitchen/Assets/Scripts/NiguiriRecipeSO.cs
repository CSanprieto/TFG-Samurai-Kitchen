using UnityEngine;

[CreateAssetMenu(fileName = "NiguiriRecipeSO", menuName = "Scriptable Objects/NiguiriRecipeSO")]
public class NiguiriRecipeSO : ScriptableObject
{
    public KitcheObjectSO input1;
    public KitcheObjectSO input2;
    public KitcheObjectSO outputItem;

    public int mixingProgressMax;
}
