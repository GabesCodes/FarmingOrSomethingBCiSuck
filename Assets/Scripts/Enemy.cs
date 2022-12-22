using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;

    public Transform enemyGoal;

    private GameObject player;
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

    public float enemyHP;
    private float enemySpeed;
    private float enemyDamage;
    private float attackSpeed;
    private float attackRange;

   
    private void Awake()
    {
    }
    private void Start()
    {
        player = PlayerManager.instance.player;

        enemy = this.transform;

        enemyGoalPosition = EnemyGoalManager.instance.enemyGoal.transform.position;
        enemyHP = enemyData.hp;
        enemySpeed = enemyData.enemySpeed;
        attackSpeed = enemyData.attackSpeed;
        attackRange = enemyData.attackRange;
        enemyDamage = enemyData.damage;

        attackHomeRadius = enemyData.attackHomeRadius;
        chasePlayersRadius = enemyData.chasePlayersRadius;
        
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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
        AttackHome();
        canAttackHome = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttackHome = true;
    }
    private void AttackHome()
    {
        RaycastHit hit;
        if(Physics.Raycast(enemy.position, transform.forward, out hit, 10f))
        {
            if (hit.collider.gameObject.GetComponent<Health>())
            {
                Debug.Log(hit.collider.gameObject);
                Health target = hit.transform.GetComponent<Health>();
                if (target != null) //  only do this if component is found, target is not null (nothing)
                {
                    target.TakeDamage(enemyDamage);
                    Destroy(this.gameObject);
                }
            }
        }
    }


    private void FacePlayerQuick()
    {
        Vector3 direction = (playerPosition - enemy.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 130f);
    }

    private void FacePlayerSmooth()
    {
        Vector3 direction = (playerPosition - enemy.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 50f);
    }

    private void TryChasePlayer() //really gross code bruh
    {
        playerPosition = player.transform.position;
        distanceFromPlayer = Vector3.Distance(enemy.position, playerPosition);

        if (distanceFromPlayer <= chasePlayersRadius && !ignorePlayer)
        {
            FacePlayerSmooth();
            playerInChaseRange = true;
            enemy.position = Vector3.MoveTowards(enemy.position, playerPosition, enemySpeed * Time.deltaTime);  
        }

        if(distanceFromPlayer >= chasePlayersRadius)
        {
            inAttackPlayersRange = false;
            playerInChaseRange = false;
        }

        if (distanceFromPlayer <= attackRange)
        {
            FacePlayerQuick();
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
        Debug.Log("attempting attack");
        if (inAttackPlayersRange && canAttackPlayers)
        {
            StartCoroutine(StartAttackPlayer());
        }
    }
    IEnumerator StartAttackPlayer()
    {
        //yield return new WaitForSeconds(3);
        Debug.Log("trying attack");
        AttackPlayer();
        canAttackPlayers = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttackPlayers = true;
    }
    private void AttackPlayer()
    {
        Debug.Log("finding player");
        RaycastHit hit;
        if (Physics.Raycast(enemy.position, enemy.forward, out hit, attackRange) && hit.collider.CompareTag("Player"))
        {
            Debug.Log("player found");
            Health target = hit.collider.gameObject.GetComponent<Health>();
            if (target != null)
            {
                Debug.Log("player hit");
                target.TakeDamage(enemyDamage);
            }

            if (target = null)
            {
                return;
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackHomeRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chasePlayersRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}



