using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollideBoss : MonoBehaviour
{
    private BossScript parentMeleeEnemy;

    private void Start()
    {
        parentMeleeEnemy = GetComponentInParent<BossScript>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            parentMeleeEnemy.OnAttackColliderTrigger(other);
        }
    }
}
