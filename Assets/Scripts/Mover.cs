﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mover : MonoBehaviour, IAction
{
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        UpdateAnimator();
    }


    public void StartMoveAction(Vector3 destination)
    {
        //GetComponent<Fighter>().Cancel();
        GetComponent<ActionScheduler>().StartAction(this);
        MoveTo(destination);
    }


    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = false;
    }


    public void Cancel()
    {
        navMeshAgent.isStopped = true;
    }


    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("Blend", speed);
    }
}

