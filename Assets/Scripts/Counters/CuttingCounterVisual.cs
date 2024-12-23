using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    enum ProgressBarFace
    {
        TowardCamera,
        Inverted
    }

    [SerializeField] ProgressBarFace progressBarFacing;

    static string CUT = "Cut";

    [SerializeField] CuttinggCounter cuttinggCounter;

    [SerializeField] GameObject progressBarUI;

    Animator animator;

    float multiplier;
    




    private void Start()
    {
        animator = GetComponent<Animator>();
        cuttinggCounter.OnCutVisuals += CuttinggCounter_OnCutVisuals;


        
    }

    private void CuttinggCounter_OnCutVisuals(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }


    // for UI Progress Bar to look straight toward camera 
    private void LateUpdate()
    {
        switch (progressBarFacing)
        {
            case ProgressBarFace.Inverted:
                multiplier = -1f;
                break;
            case ProgressBarFace.TowardCamera:
                multiplier = 1f;
                break;
        }

        progressBarUI.transform.forward = Camera.main.transform.forward * multiplier;
    }

    
}
