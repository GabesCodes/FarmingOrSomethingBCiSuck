using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;

    public Transform enemyGoal;

    public GameObject player;
    private Transform enemy;

    private Vector3 enemyGoalPosition;
    public Vector3 playerPosition;

    public float distanceFromPlayer;
    public float distanceFromHome;

    public float attackHomeRadius;
    public float chasePlayersRadius;

    public float attackPlayersRadius;

    public bool inAttackPlayersRange;
    public bool playerInChaseRange;
    public bool canAttackPlayers;
    public bool ignorePlayer;

    public bool inAttackHomeRange;
    public bool canAttackHome;

    private float enemySpeed;
    private float enemyDamage;
    private float attackSpeed;

    public float turnSmoothing = 5f;
    

    private void Start()
    {
        enemy = this.transform;
        player = player.gameObject;
        enemyGoalPosition = enemyGoal.position;

        enemySpeed = enemyData.enemySpeed;
        attackSpeed = enemyData.attackSpeed;
        enemyDamage = enemyData.damage;

        attackHomeRadius = enemyData.attackHomeRadius;
        chasePlayersRadius = enemyData.attackPlayerRadius;
        
        inAttackHomeRange = false;
        inAttackPlayersRange = false;

        ignorePlayer = false;

        canAttackPlayers = true;
        canAttackHome = true;
    }

    private void Update()
    {
        if (!playerInChaseRange)
        {
            GoToHome();
        }
        TryChasePlayer();     
    } 
    private void FaceHome()
    {
        Vector3 direction = (enemyGoalPosition - enemy.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSmoothing);
    }
    private void GoToHome()
    {
        FaceHome();
        distanceFromHome = Vector3.Distance(enemy.position, enemyGoalPosition);

        if (distanceFromHome >= attackHomeRadius)
        {
            inAttackHomeRange = false;
            ignorePlayer = false;
            MoveTowardsHome();
        }
        else
        {
            inAttackHomeRange = true;
            ignorePlayer = true;
            AttemptAttackHome();
        }
    }
    private void MoveTowardsHome()
    {
        distanceFromHome = Vector3.Distance(enemy.position, enemyGoalPosition);
        transform.position = Vector3.MoveTowards(enemy.position, enemyGoalPosition, enemySpeed * Time.deltaTime);
    }
    private void AttemptAttackHome()
    {
        if (inAttackHomeRange && canAttackHome)
        {
            StartCoroutine(StartAttackHome());
        }
    }
    IEnumerator StartAttackHome()
    {
        AttackHome(enemyDamage);
        canAttackHome = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttackHome = true;
    }
    private void AttackHome(float damage)
    {
            Debug.Log("test hit home");
    }


    private void FacePlayer()
    {
        Vector3 direction = (playerPosition - enemy.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSmoothing);
    }
    private void TryChasePlayer() //really gross code bruh
    {
        playerPosition = player.gameObject.transform.position;
        distanceFromPlayer = Vector3.Distance(enemy.position, playerPosition);

        if (distanceFromPlayer <= chasePlayersRadius && !ignorePlayer)
        {
            FacePlayer();
            playerInChaseRange = true;
            transform.position = Vector3.MoveTowards(enemy.position, playerPosition, enemySpeed * Time.deltaTime);  
        }

        if(distanceFromPlayer >= chasePlayersRadius)
        {
            inAttackPlayersRange = false;
            playerInChaseRange = false;
        }

        if (distanceFromPlayer <= attackHomeRadius)
        {
            inAttackPlayersRange = true;
            AttemptAttackPlayer();
        }
        else
        {
            inAttackPlayersRange = false;
        }
    }
    private void AttemptAttackPlayer()
    {
        if (inAttackPlayersRange && canAttackPlayers)
        {
            StartCoroutine(StartAttackPlayer());
        }
    }
    IEnumerator StartAttackPlayer()
    {
        AttackPlayer(enemyDamage);
        canAttackPlayers = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttackPlayers = true;
    }
    private void AttackPlayer(float damage)
    {
        Debug.Log("test hit player");
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackHomeRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chasePlayersRadius);
    }
}






