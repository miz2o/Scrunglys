using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform player;
    public GameObject[] enemyInrange;
    public bool crowded = false;
    public bool inWanderRange;

    public BasicAI basicAI;
    private void Update()
    {
        CheckDistance();
    }
    public void CheckDistance()
    {
        if (!crowded)
        {
            Vector3 distance = Vector3.Distance(enemy[i].transform.position, player.transform.position);
            inWanderRange = 

            inAttackRange = Physics.CheckSphere(player.position, attackRange, enemy);

            crowded = Physics.CheckSphere(player.position, distanceFromPlayer, enemy);
            if (!alreadyListed && inRange)
            {
                enemyCount.enemies.Add(gameObject);
                listIndex = enemyCount.enemies.Count;
                alreadyListed = true;
            }
            else if (!inRange)
            {
                //enemyCount.enemies.Remove(listIndex);
            }


        }
        else
        {
            WanderAroundPlayer();
        }
    }
}
