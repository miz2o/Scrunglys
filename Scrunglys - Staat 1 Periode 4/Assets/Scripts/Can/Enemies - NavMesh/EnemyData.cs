using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Cooldowns")]
    public float attackTime;
    public float attackWaitTime;
    public float waitAnimation;

    [Header("Stats")]
    public float damage;

    [Header("Ranges")]
    public float noticeRange;
    public float attackRange;
    public float wanderRange;
    public float searchRange;

    [Header("Timers")]
    public float wanderTimerMin;
    public float wanderTimerMax;

    public float searchTimerMin;
    public float searchTimerMax;
}
