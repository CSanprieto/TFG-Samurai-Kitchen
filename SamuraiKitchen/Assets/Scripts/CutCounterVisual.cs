using System;
using UnityEngine;

public class CutCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    private Animator animator;
    [SerializeField] private CutCounter cutCounter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cutCounter.OnCut += CutCounter_OnCut;
    }

    private void CutCounter_OnCut(object sender, System.EventArgs e){
        animator.SetTrigger(CUT);
    }
}
