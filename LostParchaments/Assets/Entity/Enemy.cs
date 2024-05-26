using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    private NavMeshAgent navAgent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;
    public int damage;
    //public Animator animator;
    public Animation Animation;
    //public ParticleSystem hitEffect;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;
    private bool takeDamage;

    public override void Awake()
    {
        base.Awake();
        //animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        Animation = GetComponent<Animation>();
    }

    protected override void OnDie()
    {
        DataHolder.Instance.AddToData_Enemy(Type);
        QuestManager.UpdateQuestProgress?.Invoke(QuestType.KILL_MOB);
        Destroy(gameObject);
    }

    private void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        else if (!playerInSightRange && takeDamage)
        {
            ChasePlayer();
        }
    }
    
    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            navAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //animator.SetFloat("Velocity", 0.2f);
        Animation.Play("skeleton-skeleton|run");
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        Debug.DrawRay(walkPoint, -transform.up * 20, Color.blue);
        if (Physics.Raycast(walkPoint, -transform.up,300f, groundLayer))
        {
            walkPointSet = true;
        }
    }

   private void ChasePlayer()
{
    navAgent.SetDestination(player.position);
    //animator.SetFloat("Velocity", 0.2f);
    Animation.Play("skeleton-skeleton|run");
    navAgent.isStopped = false; // Add this line
}


  private void AttackPlayer()
{
    navAgent.SetDestination(transform.position);
    Animation.Play("skeleton-skeleton|idle");

    if (!alreadyAttacked)
    {
        Vector3 targetPostition = new Vector3( player.position.x, transform.position.y, player.position.z ) ;
        transform.LookAt(targetPostition);
        alreadyAttacked = true;
        //animator.SetFloat("Velocity", 0);
        //animator.SetTrigger("Attack");
        Animation.Play("skeleton-skeleton|attack");
        Invoke(nameof(ResetAttack), timeBetweenAttacks);

        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.black);
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                IDamageable stats = hit.transform.GetComponent<IDamageable>();
                if (stats != null)
                {
                    stats.OnHit(damage);
                }
            }
            /*
                YOU CAN USE THIS TO GET THE PLAYER HUD AND CALL THE TAKE DAMAGE FUNCTION

            PlayerHUD playerHUD = hit.transform.GetComponent<PlayerHUD>();
            if (playerHUD != null)
            {
               playerHUD.takeDamage(damage);
            }
             */
        }
    }
}

    private void ResetAttack()
    {
        alreadyAttacked = false;
        Animation.Play("skeleton-skeleton|idle");
    }

    private IEnumerator DestroyEnemyCoroutine()
    {
        //animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

