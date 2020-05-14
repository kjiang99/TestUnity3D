using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mover : MonoBehaviour
{
    //[SerializeField] Transform target;

    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        //this.GetComponent<NavMeshAgent>().destination = target.position;

        UpdateAnimator();
    }


    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.destination = destination;
        //navMeshAgent.isStopped = false;
    }


    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("Blend", speed);
    }
}

