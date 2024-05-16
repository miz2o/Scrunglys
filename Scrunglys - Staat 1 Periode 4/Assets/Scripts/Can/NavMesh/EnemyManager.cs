using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;

public class EnemyManager : MonoBehaviour
{
    public Transform player;
  
    public bool crowded = false;
    public bool inWanderRange;

    private HashSet<GameObject> enemyInWanderRange = new HashSet<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();

    public BasicAI basicAI;
    private void Update()
    {
        CheckDistance();
    }
    public void CheckDistance()
    {
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, player.transform.position);

            if (distance <= basicAI.wanderRange)
            {
                enemyInWanderRange.Add(enemy);
            }
            else
            {
                if (enemyInWanderRange.Contains(enemy))
                {
                    enemiesToRemove.Add(enemy);
                }
            }
            //inWanderRange =

            //inAttackRange = Physics.CheckSphere(player.position, attackRange, enemy);

            //crowded = Physics.CheckSphere(player.position, distanceFromPlayer, enemy);
            //if (!alreadyListed && inRange)
            //{
            //    enemyCount.enemies.Add(gameObject);
            //    listIndex = enemyCount.enemies.Count;
            //    alreadyListed = true;
            //}
            //else if (!inRange)
            //{
            //    //enemyCount.enemies.Remove(listIndex);
            //}
            //if (!crowded)
            //{

            //}
            //else if (crowded)
            //{
            //    basicAI.WanderAroundPlayer();
            //}
        }
        foreach(GameObject enemy in enemiesToRemove)
        {
            enemyInWanderRange.Remove(enemy);
        }
    }

    public void CrowdedBeh
  
}
