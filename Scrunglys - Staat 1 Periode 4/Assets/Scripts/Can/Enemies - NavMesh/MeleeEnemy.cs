using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : BasicAI
{
    public Collider attackCollider;

    private new void Awake()
    {
        base.Awake();
    }
    private new void Start()
    {
        base.Start();
    }

    override public void UpdateAI()
    {
        base.UpdateAI();
    }
    override public void AttackPlayer()
    {
        animator.SetTrigger("Attack");

        StartCoroutine(AttackPlayer(data.attackTime));
    }
    public IEnumerator AttackPlayer(float attackDuration)
    {

        attacking = true;

        yield return new WaitForSeconds(data.waitAnimation);

        attackCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        attackCollider.enabled = false;

        yield return new WaitForSeconds(data.attackWaitTime);

        attacking = false;
    }

    public void OnAttackColliderTrigger(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().Health(data.damage);
        }
    }
}
