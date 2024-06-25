using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Cooldowns")]
    public float attackTime, meleeAttackTime, rangedAttackTime;
    public float attackWaitTime;
    public float waitAnimation;
    public float burstInterval;

    [Header("Stats")]
    public float damage;
    public float projectileDamage;
    public int bulletCap;

    [Header("Ranges")]
    public float noticeRange;
    public float attackRange;

    public float meleeAttackRange, rangedAttackRange;
    public float wanderRange;
    public float searchRange;

    [Header("Timers")]
    public float wanderTimerMin;
    public float wanderTimerMax;

    public float searchTimerMin;
    public float searchTimerMax;

    public float attackTimerMin;
    public float attackTimerMax;
    
}
