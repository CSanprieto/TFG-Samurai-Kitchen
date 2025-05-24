using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;

    // Set the animation on awake
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Lisen to the event for give player item
    private void Start()
    {
        containerCounter.OnPlayerGrabObject += ContainerCounter_OnPlayerGrabObject;

    }

    // Launch counter animation when player get the intem from the counter
    private void ContainerCounter_OnPlayerGrabObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
