﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hj_MonsterAnimationHandler : MonoBehaviour
{

    [SerializeField]
    public Animator animator;
    [SerializeField]
    Rigidbody rigidbody;

    public bool isInteracting;
    [SerializeField] List<Sc_ActionData> ActtionDataList;

    public readonly int hash_isInteracting = Animator.StringToHash("isInteracting");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isInteracting = animator.GetBool(hash_isInteracting);
        if(isInteracting)
        {
            animator.SetFloat("PatrolY", 0);
            Debug.Log("패트롤 0");
        }
    }

    public void PlayTargetActionAnimation(string actionName, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(actionName, 0.12f);
    }
}
