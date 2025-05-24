using System;
using UnityEngine;

// Class for cut counter visual
public class CutCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    private Animator animator;
    [SerializeField] private CutCounter cutCounter;

    // Set the cut animation on awake
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cutCounter.OnCut += CutCounter_OnCut;
    }

    // Method to launch the animation
    private void CutCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
