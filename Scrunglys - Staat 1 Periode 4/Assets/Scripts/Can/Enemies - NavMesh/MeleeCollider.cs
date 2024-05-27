using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    private MeleeEnemy parentMeleeEnemy;

    private void Start()
    {
        parentMeleeEnemy = GetComponentInParent<MeleeEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            parentMeleeEnemy.OnAttackColliderTrigger(other);
        }
    }
}
