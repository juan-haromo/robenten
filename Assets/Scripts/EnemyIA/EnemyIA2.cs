using UnityEngine;
using UnityEngine.AI;

public class EnemyIA2 : MonoBehaviour
{
    [Header("Configuración de Patrulla")]
    public float patrolRadius = 20f;
    public float patrolWaitTime = 3f;

    [Header("Configuración de Detección")]
    public float detectionRadius = 15f;
    public float loseRadius = 20f;
    public float fieldOfView = 120f;
    public string playerTag = "Player";

    [Header("Ataque a distancia")]
    public float shootDistance = 8f;          // Distancia desde la que dispara
    public float fireRate = 1f;               // Disparos por segundo
    public GameObject projectilePrefab;       // Prefab del proyectil
    public Transform shootPoint;              // Punto desde donde se dispara

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 patrolDestination;
    private float waitTimer;
    private bool isChasing = false;
    private float shootTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Buscar al jugador por tag
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning(" No se encontró ningún objeto con el tag '" + playerTag + "' en la escena.");

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

            // Si está fuera del rango de disparo → acercarse
            if (distanceToPlayer > shootDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else
            {
                // Mantener distancia y disparar
                agent.isStopped = true;
                RotateTowards(player.position);
                Shoot();
            }
        }
        else if (isChasing && distanceToPlayer > loseRadius)
        {
            // Si lo pierde de vista, vuelve a patrullar
            isChasing = false;
            agent.isStopped = false;
            SetRandomDestination();
        }

        // Si no persigue, patrulla
        if (!isChasing)
            Patrol();
    }

    bool CanSeePlayer(float distanceToPlayer)
    {
        // Verificar distancia
        if (distanceToPlayer > detectionRadius)
            return false;

        // Verificar ángulo de visión
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (angle < fieldOfView / 2f)
        {
            if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, out RaycastHit hit, detectionRadius))
            {
                if (hit.transform.CompareTag(playerTag))
                    return true;
            }
        }

        // Si está muy cerca, detectarlo igual
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
        direction.y = 0;

        if (direction.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void Shoot()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= 1f / fireRate)
        {
            shootTimer = 0f;

            if (projectilePrefab != null && shootPoint != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Disparo hacia el jugador
                    Vector3 dir = (player.position - shootPoint.position).normalized;
                    rb.linearVelocity = dir * 15f; // Velocidad del proyectil
                }
            }
            else
            {
                Debug.LogWarning("⚠️ Falta asignar el prefab del proyectil o el punto de disparo.");
            }
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
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }
}
