using UnityEngine;

[CreateAssetMenu(fileName = "Refil Spawner", menuName = "Spawner/Refil Spawner")]
public class RefillSpawner : Spawner
{
    [SerializeField]
    protected int totalEnemies=5;
    
    [Tooltip("The maximum time that the spawner will be respawning the killed enemies")]
    [SerializeField]
    protected float maxRefilTime;

    [SerializeField] 
    protected howToCompleteRefilSpawn howToCompleteSpawn = howToCompleteRefilSpawn.KillingAllEnemies;

    private float _refilTime = 0;
    private bool timeEnded = false;

    public enum howToCompleteRefilSpawn
    {
        KillingAllEnemies,
        TimeOut,
    }

    public override void OnUpdateSpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        _refilTime += Time.deltaTime;
        base.OnUpdateSpawn(spawnerManager,spawnGroup);
    }

    protected override void AfterDelaySpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        if ((!timeEnded) && (totalEnemies > (enemySpawned-killedEnemies)))
        {
            SpawnEnemy(spawnerManager,spawnGroup);
        }
    }

    protected override void SpawnerLogic(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        if (_refilTime > maxRefilTime)
        {
            timeEnded = true;
        }        
        
        if (howToCompleteSpawn == howToCompleteRefilSpawn.KillingAllEnemies)
        {
            if ((killedEnemies >= enemySpawned) && timeEnded)
            {
                SpawnerCompleted(spawnGroup);
            }
        }
        else if (howToCompleteSpawn == howToCompleteRefilSpawn.TimeOut)
        {
            if (timeEnded)
            {
                SpawnerCompleted(spawnGroup);
            }
        }
    }
}