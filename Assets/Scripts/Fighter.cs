﻿using System;
using UnityEngine;

public class Fighter : MonoBehaviour, IAction
{
    [SerializeField] float weaponRange = 2f;

    Transform target;

    private void Update()
    {
        if (target == null) return;

        if (!GetIsInRange())
        {
            GetComponent<Mover>().MoveTo(target.position);
        }
        else
        {
            GetComponent<Mover>().Cancel();
            AttackBehaviour();
        }
    }

    private bool GetIsInRange()
    {
        return Vector3.Distance(transform.position, target.position) < weaponRange;
    }

    public void Attack(CombatTarget combatTarget)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        target = combatTarget.transform;
    }

    public void Cancel()
    {
        target = null;
    }


    private void AttackBehaviour()
    {
        GetComponent<Animator>().SetTrigger("attack");
    }

    // Animation Event
    private void Hit()
    {

    }
}
