using UnityEngine;
using UnityEngine.AI;

public class EnemyIA1 : MonoBehaviour
{
    [Header("Configuración de Patrulla")]
    public float patrolRadius = 20f;        // Radio máximo de patrulla dentro del NavMesh
    public float patrolWaitTime = 3f;       // Tiempo que espera antes de ir a otro punto

    [Header("Configuración de Detección")]
    public float detectionRadius = 10f;     // Distancia para detectar al jugador
    public float loseRadius = 15f;          // Distancia a la que el enemigo pierde al jugador
    public float fieldOfView = 120f;        // Ángulo de visión del enemigo
    public string playerTag = "Player";     // Tag del jugador

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 patrolDestination;
    private float waitTimer;
    private bool isChasing = false;

    void Start()
    {
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

        // Si ve al jugador, lo persigue
        if (CanSeePlayer(distanceToPlayer))
        {
            isChasing = true;
            agent.SetDestination(player.position);
            RotateTowards(player.position);
        }
        // Si lo estaba persiguiendo pero lo pierde
        else if (isChasing && distanceToPlayer > loseRadius)
        {
            isChasing = false;
            SetRandomDestination();
        }

        // Si no está persiguiendo, sigue patrullando
        if (!isChasing)
            Patrol();
    }

    // ============================================================
    // ====================== MÉTODOS =============================
    // ============================================================

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
                {
                    // Si realmente ve al jugador
                    return true;
                }
            }
        }

        // Si está muy cerca aunque esté fuera del ángulo, también detectarlo
        if (distanceToPlayer < detectionRadius * 0.5f)
            return true;

        return false;
    }

    void Patrol()
    {
        // Si llegó al destino, esperar un poco antes de elegir otro punto
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
    }
}
