﻿using System;
using UnityEngine;

public class Fighter : MonoBehaviour, IAction
{
    [SerializeField] float weaponRange = 2f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] float weaponDamage = 5f;


    Health target;
    float timeSinceLastAttack = 0;


    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (target == null) return;
        if (target.IsDead) return;

        if (!GetIsInRange())
        {
            GetComponent<Mover>().MoveTo(target.transform.position);
        }
        else
        {
            GetComponent<Mover>().Cancel();
            AttackBehaviour();
        }
    }

    private bool GetIsInRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
    }


    private void AttackBehaviour()
    {
        transform.LookAt(target.transform);
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            // This will trigger the Hit() event.
            TriggerAttack();
            timeSinceLastAttack = 0;
        }
    }

    private void TriggerAttack()
    {
        GetComponent<Animator>().ResetTrigger("stopAttack");
        GetComponent<Animator>().SetTrigger("attack");
    }

    public bool CanAttack(CombatTarget combatTarget)
    {
        if (combatTarget == null) { return false; }
        Health targetToTest = combatTarget.GetComponent<Health>();
        return targetToTest != null && !targetToTest.IsDead;
    }


    public void Attack(CombatTarget combatTarget)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        target = combatTarget.GetComponent<Health>();
    }

    public void Cancel()
    {
        StopAttack();
        target = null;
    }


    private void StopAttack()
    {
        GetComponent<Animator>().ResetTrigger("attack");
        GetComponent<Animator>().SetTrigger("stopAttack");
    }


    // Animation Event
    private void Hit()
    {
        if (target == null) return;
        target.TakeDamage(weaponDamage);
    }
}
