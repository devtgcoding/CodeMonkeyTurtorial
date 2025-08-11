using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCouterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainnerCounter containnerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containnerCounter.OnPlayerGrabbedObject += ContainerCounter_OnplayerGrabbedObject;
    }

    private void ContainerCounter_OnplayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
