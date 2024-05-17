using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;

public class EnemyManager : MonoBehaviour
{
    public Transform player;

    public int crowdLimit;
  
    public bool crowded = false;
    public bool inWanderRange;

    private HashSet<GameObject> enemyInWanderRange = new HashSet<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();

    public BasicAI basicAI;
    private void Update()
    {
        CheckDistance();
        CheckCrowd();
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

            basicAI = enemy.GetComponent<BasicAI>();
            if (distance <= basicAI.attackRange)
            {
                basicAI.inAttackRange = true;
            }
            else if (crowded)
            {
                basicAI.WanderAroundPlayer();
            }
            else if (distance <= basicAI.noticeRange)
            {
                basicAI.inRange = true;
            }
        }
        foreach(GameObject enemy in enemiesToRemove)
        {
            enemyInWanderRange.Remove(enemy);
        }
    }

    public void CheckCrowd()
    {
        if(enemyInWanderRange.Count >= crowdLimit)
        {
            crowded = true;
        }
        else
        {
            crowded = false;
        }
    }
}
