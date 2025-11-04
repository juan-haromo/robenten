using UnityEngine;

public class Bala : MonoBehaviour
{
    [Header("Configuración de la bala")]
    public float lifeTime = 5f;      // Tiempo máximo de vida antes de destruirse
    public float damage = 10f;       // Daño que puede causar (opcional)
    public string playerTag = "Player"; // Tag del jugador

    void Start()
    {
        // Destruir la bala automáticamente tras cierto tiempo
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si colisiona con el jugador
        if (collision.collider.CompareTag(playerTag))
        {
            Debug.Log("💥 Bala impactó al jugador");

            Destroy(gameObject); // Destruir la bala
        }
        else
        {
            // Destruir la bala si golpea cualquier otra cosa (paredes, suelo, etc.)
            Destroy(gameObject);
        }
    }
}
