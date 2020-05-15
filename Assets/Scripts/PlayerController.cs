using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (InteractWithCombat())
            return;
        if (InteractWithMovement())
            return;
    }

    private bool InteractWithCombat()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

        foreach (RaycastHit hit in hits)
        {
            CombatTarget target = hit.transform.GetComponent<CombatTarget>();
            if (target == null) continue;

            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Fighter>().Attack(target);
            }
            return true;
        }
        return false;
    }


    private bool InteractWithMovement()
    {
        Ray ray = GetMouseRay();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            if (Input.GetMouseButton(0))
            {
                GetComponent<Mover>().StartMoveAction(hit.point);
            }

            Debug.DrawRay(ray.origin, ray.direction * 100);
            return true;
        }
        return false;
    }


    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
