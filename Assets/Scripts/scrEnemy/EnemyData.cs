using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Enemy", order = 1)]



public class EnemyData : ScriptableObject // holds data we can change
{
    public float hp;
    public float damage;
    public float enemySpeed;
    public float attackSpeed;
    public float attackRange;
    public float attackHomeRadius;
    public float chasePlayersRadius;

}
