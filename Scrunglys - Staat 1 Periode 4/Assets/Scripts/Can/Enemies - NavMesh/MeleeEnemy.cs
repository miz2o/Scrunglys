using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BasicAI
{
    public Collider attackCollider;
    override public void UpdateAI()
    {
        base.UpdateAI();
    }
    override public void AttackPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

        StartCoroutine(AttackPlayer(data.attackTime));
    }
    public IEnumerator AttackPlayer(float attackDuration)
    {
        attacking = true;
        attackCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        attackCollider.enabled = false;
        attacking = false;
    }
    void ResetAttack()
    {
        attacked = false;
        print("Reset attack");
    }
}
