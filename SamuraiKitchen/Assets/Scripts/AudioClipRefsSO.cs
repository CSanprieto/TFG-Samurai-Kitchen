using UnityEngine;

// This class will have all sounds in different arrays
[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{

    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] trash;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
}
