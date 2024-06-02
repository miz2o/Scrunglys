using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform player;

    public int crowdLimit;

    public bool crowded = false;

    public int crowdCount;

    public List<GameObject> enemies;

    public CheckCurrentSword currentSword;

    void Start()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (GameObject enemy in enemies)
        {
            BasicAI basicAI = enemy.GetComponent<BasicAI>();
            if (basicAI != null)
            {
                basicAI.player = player;
                basicAI.animator = enemy.GetComponent<Animator>();
            }
        }
    }

    void Update()
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
                BasicAI basicAI = enemy.GetComponent<BasicAI>();
                if (basicAI != null)
                {
                    basicAI.UpdateAI();

                    MeleeEnemy meleeAI = enemy.GetComponent<MeleeEnemy>();
                    if (meleeAI != null)
                    {
                        if (meleeAI.hit && !currentSword.swordScript.slashing)
                        {
                            meleeAI.hit = false;
                        }
                    }
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
        if (crowdCount >= crowdLimit)
        {
            crowded = true;
        }
        else
        {
            crowded = false;
        }
    }
}