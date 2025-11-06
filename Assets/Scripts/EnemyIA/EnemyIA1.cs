using UnityEngine;
using UnityEngine.AI;

public class EnemyIA1 : MonoBehaviour, IDamageable
{
    [Header("Configuración de Patrulla")]
    public float patrolRadius = 20f;
    public float patrolWaitTime = 3f;

    [Header("Configuración de Detección")]
    public float detectionRadius = 10f;
    public float loseRadius = 15f;
    public float fieldOfView = 120f;
    public string playerTag = "Player";

    [Header("Distancia mínima con el jugador")]
    public float stopDistance = 2f; // Si está más cerca que esto, se detiene

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 patrolDestination;
    private float waitTimer;
    private bool isChasing = false;

    [SerializeField] private float currentHealth, maxHealth;

    void Start()
    {
        currentHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();

        // Buscar jugador por tag
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("⚠️ No se encontró ningún objeto con el tag '" + playerTag + "' en la escena.");

        // Elegir el primer destino aleatorio
        SetRandomDestination();
    }

    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si ve al jugador
        if (CanSeePlayer(distanceToPlayer))
        {
            isChasing = true;

            // Si está muy cerca, detenerse
            if (distanceToPlayer <= stopDistance)
            {
                agent.isStopped = true;
                RotateTowards(player.position); // Solo mirar al jugador
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position); // Perseguir
                RotateTowards(player.position);
            }
        }
        // Si lo estaba persiguiendo pero lo pierde de vista
        else if (isChasing && distanceToPlayer > loseRadius)
        {
            isChasing = false;
            agent.isStopped = false;
            SetRandomDestination();
        }

        // Si no está persiguiendo, sigue patrullando
        if (!isChasing)
            Patrol();
    }

    bool CanSeePlayer(float distanceToPlayer)
    {
        // Primero, comprobar si el jugador está dentro del radio general
        if (distanceToPlayer > detectionRadius)
            return false;

        // Luego comprobar si el jugador está dentro del ángulo de visión
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (angle < fieldOfView / 2f)
        {
            // Comprobar si hay línea de visión
            if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, out RaycastHit hit, detectionRadius))
            {
                if (hit.transform.CompareTag(playerTag))
                    return true;
            }
        }

        // Si está muy cerca aunque esté fuera del ángulo, también detectarlo
        if (distanceToPlayer < detectionRadius * 0.5f)
            return true;

        return false;
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= patrolWaitTime)
            {
                SetRandomDestination();
                waitTimer = 0f;
            }
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolDestination = hit.position;
            agent.SetDestination(patrolDestination);
        }
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // Evitar rotación vertical

        if (direction.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, loseRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) 
        {
            gameObject.SetActive(false);
        }
    }
}
