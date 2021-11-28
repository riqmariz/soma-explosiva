using UnityEngine;

[CreateAssetMenu(fileName = "Time Order Spawner", menuName = "Spawner/Time Order Spawner")]
public class OrderTimeSpawner : Spawner
{
    [SerializeField]
    protected int totalEnemies;
    [Tooltip("Array with the delay between the enemies, cell by cell, put -1 the totalEnemies size")]
    [SerializeField]
    protected float[] enemyDelay;
    [SerializeField] protected OnceSpawner.howToCompleteOnceSpawner howToComplete = OnceSpawner.howToCompleteOnceSpawner.AfterKillingAllEnemies;

    private int indexDelay = 0;
    private bool onEnter = true;
    public override void OnUpdateSpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        if (onEnter)
        {
            onEnter = false;
            if (totalEnemies-1 != (enemyDelay.Length))
            {
                Debug.LogWarning("The size of the enemyDelay and total Enemies are not compatible");
            }
        }
        
        _countDown -= Time.deltaTime;
        if (_countDown <= 0 && enemySpawned<totalEnemies)
        {
            _countDown = enemyDelay[indexDelay];
            if (indexDelay < enemyDelay.Length-1)
            {
                indexDelay++;
            }
            
            AfterDelaySpawn(spawnerManager,spawnGroup);
        }
        
        SpawnerLogic(spawnerManager,spawnGroup);
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
        if (killedEnemies >= enemySpawned)
        {
            _countDown = 0;
        }
        
        if (howToComplete == OnceSpawner.howToCompleteOnceSpawner.AfterAllSpawned)
        {
            if (totalEnemies >= enemySpawned)
            {
                SpawnerCompleted(spawnGroup);
            }
        }else if (howToComplete == OnceSpawner.howToCompleteOnceSpawner.AfterKillingAllEnemies)
        {
            if (killedEnemies >= totalEnemies)
            {
                SpawnerCompleted(spawnGroup);
            }
        }
    }
}