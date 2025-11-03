using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Objeto a invocar")]
    public GameObject enemyPrefab; 

    [Header("Configuración del Spawn")]
    public float spawnInterval = 3f; 
    public Transform spawnPoint;  
    public bool isSpawning = true;  

    private float timer = 0f;        

    void Start()
    {
       
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }
    }

    void Update()
    {
    
        if (isSpawning)
        {
            timer += Time.deltaTime;

   
            if (timer >= spawnInterval)
            {
                SpawnEnemy(); 
                timer = 0f;    
            }
        }
    }

    void SpawnEnemy()
    {
      
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void ToggleSpawner(bool active)
    {
        isSpawning = active;
    }
}
