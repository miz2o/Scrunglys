using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    public Transform player;
   // public List<GameObject> enemies = new List<GameObject>();   make separate script for counting enemies

    public float noticeRange;
    public float distanceFromPlayer;

    bool tooClose= false;
    bool inRange = false;
    bool crowded = false;
    bool following = false;
    bool attacking = false;

    public LayerMask enemy;
    void Update()
    {
        CheckDistance();
        States();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
    }
    void States()
    {
        if (inRange)
        {
            FollowPlayer();
        }
    }

    void CheckDistance()
    {
        if (!crowded)
        {
            inRange = Physics.CheckSphere(player.position, noticeRange, enemy);

            tooClose = Physics.CheckSphere(player.position, distanceFromPlayer, enemy);
            //enemies.Add(gameObject);
        }
        
    }

    void FollowPlayer()
    {
        following = true;

    }
    void AttackPlayer()
    {
        attacking = true;
    }

}
