using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AnimationControl : MonoBehaviour {

    public Transform target;
    public bool attack = false;
    public AudioSource GameOver;

    Animator animator;
    NavMeshAgent agent;
    bool called = false;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);

        if(agent.remainingDistance > agent.stoppingDistance + 1f)
        {
            animator.SetBool("Crawl", true);
        }
        else if(agent.remainingDistance != 0f && agent.remainingDistance <= agent.stoppingDistance + 1f && !animator.GetBool("attack"))
        {
            animator.SetBool("Crawl", false);
            animator.SetBool("attack", true);
            Debug.Log("else!!!!");
            attack = true;
        }

        if (attack && !called)
        {
            StartCoroutine(Over());
            called = true;
        }
	}

    IEnumerator Over()
    {
        GameOver.Play();
        yield return new WaitForSeconds(7f);
        SceneManager.LoadSceneAsync("menu");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            animator.SetBool("attack", true);
            animator.SetBool("attack", false);
            attack = true;
        }
    }
}
