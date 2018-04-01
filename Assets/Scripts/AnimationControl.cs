using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationControl : MonoBehaviour {

    public Transform target;

    Animator animator;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);

        if(!(agent.remainingDistance > agent.stoppingDistance))
        {
            animator.StopPlayback();
        }
	}
}
