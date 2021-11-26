using UnityEngine;

[CreateAssetMenu(fileName = "Once Spawner", menuName = "Spawner/Once Spawner")]
public class OnceSpawner : Spawner
{
    [SerializeField] 
    protected int totalEnemies = 5;

    [SerializeField] protected howToCompleteOnceSpawner howToComplete = howToCompleteOnceSpawner.AfterAllSpawned;

    public enum howToCompleteOnceSpawner
    {
        AfterAllSpawned,
        AfterKillingAllEnemies,
    }

    protected override void AfterDelaySpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        if (totalEnemies > enemySpawned)
        {
            SpawnEnemy(spawnerManager,spawnGroup);
        }
    }

    protected override void SpawnerLogic(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        if (howToComplete == howToCompleteOnceSpawner.AfterAllSpawned)
        {
            if (totalEnemies >= enemySpawned)
            {
                SpawnerCompleted(spawnGroup);
            }
        }else if (howToComplete == howToCompleteOnceSpawner.AfterKillingAllEnemies)
        {
            if (killedEnemies >= totalEnemies)
            {
                SpawnerCompleted(spawnGroup);
            }
        }
    }
}