using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounterVisuals : MonoBehaviour
{
    Animator animator;
    [SerializeField] ContainerCounter containerCounter;

    const string OPEN_CLOSE = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabKitchenObject += UpdatingContainerCounterVisuals;
    }

    void UpdatingContainerCounterVisuals(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }


}
