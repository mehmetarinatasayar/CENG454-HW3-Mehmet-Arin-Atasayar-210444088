using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnRadius = 10f;

    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        // Çekirdeğin etrafında rastgele bir çember üzerinde düşmanı doğur
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, randomCircle.y, 0);

        GameObject newEnemyObj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyComponent = newEnemyObj.GetComponent<Enemy>();

        if (enemyComponent != null)
        {
            // %50 ihtimalle düz koşan, %50 ihtimalle zikzak çizen strateji ata! (Strategy Pattern Kullanımı)
            if (Random.value > 0.5f)
            {
                enemyComponent.SetMovementStrategy(new MoveToCoreStrategy());
                newEnemyObj.name = "Enemy_Hücumcu";
            }
            else
            {
                enemyComponent.SetMovementStrategy(new ZigZagStrategy());
                newEnemyObj.name = "Enemy_Zikzakçı";
            }
        }
    }
}