using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;

    public Transform enemyGoal;

    private GameObject player;

    public NavMeshAgent agent;

    public LayerMask obstacles;


    public float chasePlayersRadius; //radius size if player enters we chase
    public float attackPlayersRadius; //radius size to start an attack
    public bool playerInChaseRange; //is the player in chase range
    public bool inAttackPlayersRange; //are they in range for an attack
    public bool canAttackPlayers; //do we have the ability to attack the player
    public bool ignorePlayer; //are we ignoring the player


    public Vector3 enemyGoalPosition; //goal position
    public Vector3 playerCurrentPosition; //current player position

    public float distanceFromGoal; //current distance from goal
    public float distanceFromPlayer; //current distance from player
    public float distanceFromDestination;


    public float attackGoalRadius; //radius for being able to attack goal
    public bool inAttackGoalRadius; //are we in attacking goal distance
    public bool canAttackHome; //do we have the ability to attack
    public bool canSeeGoal; // can we currently see the goal

    public float enemyHP;
    private float enemySpeed;
    private float enemyDamage;
    private float attackSpeed;
    private float attackRange;

   
    private void Start()
    {
        player = FindObjectOfType<PlayerManager>().player;
        agent = this.GetComponent<NavMeshAgent>();
        enemyGoal = FindObjectOfType<EnemyGoalManager>().enemyGoal.transform;
        if(enemyGoal == null)
        {
            return;
        }
        
        enemyHP = enemyData.hp;
        agent.speed  = enemyData.enemySpeed;
        attackSpeed = enemyData.attackSpeed;
        attackRange = enemyData.attackRange;
        enemyDamage = enemyData.damage;

        attackGoalRadius = enemyData.attackHomeRadius;
        chasePlayersRadius = enemyData.chasePlayersRadius;
        
        inAttackGoalRadius = false;
        inAttackPlayersRange = false;

        ignorePlayer = false;

        canAttackPlayers = true;
        canAttackHome = true;
    }

    private void Update()
    {
        distanceFromDestination = agent.remainingDistance;
        playerCurrentPosition = player.transform.position;
        enemyGoalPosition = enemyGoal.transform.position;
        distanceFromGoal = Vector3.Distance(agent.transform.position, enemyGoalPosition);
        distanceFromPlayer = Vector3.Distance(agent.transform.position, playerCurrentPosition);

        CanSeeGoal();

        if (!playerInChaseRange)
        {
            agent.SetDestination(enemyGoalPosition);

            if (!inAttackGoalRadius)
            {
                LookAt(enemyGoal);
            }
        }

        if (distanceFromGoal >= attackGoalRadius)
        {
            inAttackGoalRadius = false;
            ignorePlayer = false;
        }
        else
        {
            inAttackGoalRadius = true;
            ignorePlayer = true;
            AttemptAttackHome();
        }

        TryChasePlayer();     
    }

    private void CanSeeGoal()
    {
        RaycastHit hit;
        if (Physics.Raycast(agent.transform.position, agent.transform.forward, out hit, 1000f, obstacles))
        {
            if (hit.collider.gameObject != enemyGoal.gameObject)
            {
                canSeeGoal = false; 
            }
            else
            {
                canSeeGoal = true;
                Debug.Log(gameObject.name + "can see");
            }
        }
    }

    private void AttemptAttackHome()
    {
        if (inAttackGoalRadius && canAttackHome)
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
        if(Physics.Raycast(agent.transform.position, transform.forward, out hit, 10f))
        {
            if (hit.collider.gameObject.GetComponent<Health>())
            {
                Debug.Log(hit.collider.gameObject);
                Health target = hit.transform.GetComponent<Health>();
                if (target != null) //  only do this if component is found, target is not null (nothing)
                {
                    target.TakeDamage(enemyDamage);
                }
            }
        }
    }

    private void TryChasePlayer() 
    {
        if (distanceFromPlayer <= chasePlayersRadius && !ignorePlayer)
        {
            LookAt(player.transform);
            playerInChaseRange = true;
            agent.SetDestination(playerCurrentPosition);
        }

        if(distanceFromPlayer >= chasePlayersRadius)
        {
            inAttackPlayersRange = false;
            playerInChaseRange = false;
            
        }

        if (distanceFromPlayer <= attackRange)
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
        if (inAttackPlayersRange && canAttackPlayers && !ignorePlayer)
        {
            Debug.Log("attempting attack");
            StartCoroutine(StartAttackPlayer());
        }
    }
    IEnumerator StartAttackPlayer()
    {
        Debug.Log("trying attack");
        AttackPlayer();
        canAttackPlayers = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttackPlayers = true;
    }
    private void AttackPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(agent.transform.position, agent.transform.forward, out hit, attackRange) && hit.collider.CompareTag("Player"))
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
    private void LookAt(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
    }
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackGoalRadius);

        Gizmos.color = Color.yellow;
       // Gizmos.DrawWireSphere(transform.position, chasePlayersRadius);

        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

