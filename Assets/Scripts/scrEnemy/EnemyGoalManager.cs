using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoalManager : MonoBehaviour
{
    #region Singleton
    public static EnemyGoalManager instance;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       this.GetComponent<Health>().health = 1000;
    }

    #endregion

    public GameObject enemyGoal;

}