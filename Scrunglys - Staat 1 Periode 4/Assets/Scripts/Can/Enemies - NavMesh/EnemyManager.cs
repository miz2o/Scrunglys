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

    public int crowdCount;

    public GameObject[] enemies;
    public BasicAI basicAI;

    public void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            basicAI = enemy.GetComponent<BasicAI>();
            basicAI.player = player;
        }
    }
    private void Update()
    {
        foreach(GameObject enemy in enemies)
        {
            basicAI = enemy.GetComponent<BasicAI>();
            basicAI.UpdateAI();
        }
        CheckCrowd();
    }
    public void CheckCrowd()
    {
        if (crowdCount == crowdLimit)
        {
            crowded = true;
        }
        else if (crowdCount < crowdLimit)
        {
            crowded = false; 
        }
    }
    //public void CheckDistance()
    //{
    //    List<GameObject> enemiesToRemove = new List<GameObject>();

    //    foreach (GameObject enemy in enemies)
    //    {
    //        float distance = Vector3.Distance(enemy.transform.position, player.transform.position);

    //        basicAI = enemy.GetComponent<BasicAI>();

    //        if (distance <= basicAI.wanderRange)
    //        {
    //            enemyInWanderRange.Add(enemy);
    //        }
    //        else
    //        {
    //            if (enemyInWanderRange.Contains(enemy))
    //            {
    //                enemiesToRemove.Add(enemy);
    //            }
    //        }

    //        if (distance <= basicAI.attackRange)
    //        {
    //            basicAI.AttackPlayer();
    //        }
    //        else if (crowded && !basicAI)
    //        {
    //            basicAI.WanderAroundPlayer();
    //        }
    //        else if (distance <= basicAI.noticeRange)
    //        {
    //            basicAI.currentState = BasicAI.State.WANDERING;
    //        }
    //    }
    //    foreach(GameObject enemy in enemiesToRemove)
    //    {
    //        enemyInWanderRange.Remove(enemy);
    //    }
    //}
}
