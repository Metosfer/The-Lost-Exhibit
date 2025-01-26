using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private PlayerAnimation playerAnimation; // PlayerAnimation scriptine referans

    public NavMeshAgent navMeshAgent;
    public Animator animator; // Animator bile�eni eklendi
    public float startWaitTime = 4f;
    public float timeToRotate = 2f;

    public float speedWalk = 6f;
    public float speedRun = 9f;
    public float patrolSpeed = 3f; // Devriye h�z�

    public float viewRadius = 15f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex = 0;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;

    float attackCooldown = 2f; // Sald�r� cooldown s�resi
    float lastAttackTime;

    public int health = 30; // NPC'nin can�
    private bool isDead = false; // �l�m durumu bayra��
    private bool isWalking = false; // Y�r�me durumu bayra��

    void Start()
    {
        playerAnimation = FindAnyObjectByType<PlayerAnimation>();
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Animator bile�eni al�nd�

        if (waypoints.Length > 0)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = patrolSpeed;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            animator.SetBool("isIdle", true); // Ba�lang��ta idle animasyonu tetikleniyor
        }
        else
        {
            Debug.LogWarning("Waypoints array is empty.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return; // E�er �l� ise di�er animasyonlar� oynatma

        EnvironmentView();
        if (!m_IsPatrol)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);
            animator.SetBool("isRunning", true); // Ko�ma animasyonu tetikleniyor
            animator.SetBool("isIdle", false); // Idle animasyonu durduruluyor
            animator.SetBool("isWalking", false); // Y�r�me animasyonu durduruluyor
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(patrolSpeed);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                if (waypoints.Length > 0)
                {
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }
                animator.SetBool("isRunning", false); // Ko�ma animasyonu durduruluyor
                animator.SetBool("isIdle", true); // Idle animasyonu tetikleniyor
                animator.SetBool("isWalking", false); // Y�r�me animasyonu durduruluyor
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < navMeshAgent.stoppingDistance)
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;

                    if (Time.time >= lastAttackTime + attackCooldown)
                    {
                        animator.SetTrigger("attack"); // Sald�r� animasyonu tetikleniyor
                        lastAttackTime = Time.time;
                    }
                }
            }
        }
    }

    void Patrolling()
    {
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(patrolSpeed);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            if (waypoints.Length > 0)
            {
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (m_WaitTime <= 0)
                    {
                        NextPoint();
                        Move(patrolSpeed);
                        m_WaitTime = startWaitTime;
                    }
                    else
                    {
                        Stop();
                        m_WaitTime -= Time.deltaTime;
                        animator.SetBool("isIdle", true); // Idle animasyonu tetikleniyor
                        animator.SetBool("isWalking", false); // Y�r�me animasyonu durduruluyor
                    }
                }
                else
                {
                    Move(patrolSpeed); // Devriye gezerken isWalking parametresini �al��t�r
                }
            }
        }
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
        animator.SetBool("isRunning", speed == speedRun); // Ko�ma animasyonu durumu g�ncelleniyor
        animator.SetBool("isIdle", false); // Idle animasyonu durumu g�ncelleniyor
        animator.SetBool("isWalking", speed == patrolSpeed); // Y�r�me animasyonu durumu g�ncelleniyor
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
        animator.SetBool("isRunning", false); // Ko�ma animasyonu durduruluyor
        animator.SetBool("isIdle", true); // Idle animasyonu tetikleniyor
        animator.SetBool("isWalking", false); // Y�r�me animasyonu durduruluyor
    }

    public void NextPoint()
    {
        if (waypoints.Length > 0)
        {
            m_CurrentWaypointIndex = Random.Range(0, waypoints.Length); // Rastgele bir hedef se�
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }

    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3f)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(patrolSpeed);
                if (waypoints.Length > 0)
                {
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }
        }

        if (m_PlayerInRange && playerInRange.Length > 0)
        {
            m_PlayerPosition = playerInRange[0].transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFinger") && playerAnimation.playerAttacking)
        {
            TakeDamage(10); // Her temas etti�inde 10 can azalt
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true; // �l�m durumu bayra��n� ayarla
        StartCoroutine(WaitForAnimationAndDestroy());
        animator.SetTrigger("isDead"); // �l�m animasyonu tetikleniyor
        navMeshAgent.isStopped = true; // NavMeshAgent'� durdur
    }

    private IEnumerator WaitForAnimationAndDestroy()
    {
        yield return new WaitForSeconds(0.5f);

        // NPC'yi yok et
        Destroy(gameObject, 10f);
    }
}
