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

    public List<GameObject> enemies;

    public MeleeEnemy meleeAI;
    public CheckCurrentSword currentSword;

    public void Start()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach(GameObject enemy in enemies)
        {
            meleeAI = enemy.GetComponent<MeleeEnemy>();
            meleeAI.player = player;
            meleeAI.animator = enemy.GetComponent<Animator>();
        }
    }
    private void Update()
    {
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                enemiesToRemove.Add(enemy);
            }
            else
            {
                meleeAI = enemy.GetComponent<MeleeEnemy>();
                meleeAI.UpdateAI();

                if (meleeAI.hit && !currentSword.swordScript.slashing)
                {
                    meleeAI.hit = false;
                }
            }
        }

        foreach (GameObject enemyToRemove in enemiesToRemove)
        {
            enemies.Remove(enemyToRemove);
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
}
