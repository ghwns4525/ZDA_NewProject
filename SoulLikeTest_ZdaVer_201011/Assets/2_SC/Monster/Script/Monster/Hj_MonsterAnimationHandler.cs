using System.Collections;
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
    }

    public void PlayTargetActionAnimation(string actionName)
    {
        animator.CrossFade(actionName, 0.12f);
    }
}
