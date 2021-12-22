using UnityEngine;


[CreateAssetMenu(fileName = "Wave Spawner", menuName = "Spawner/Wave Spawner")]
public class WaveSpawner : Spawner
{
    [SerializeField]
    protected int numberOfWaves=1;
    [SerializeField]
    protected int totalEnemiesPerWave=3;
    
    protected int _actualWave = 1;
    
    protected override void AfterDelaySpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        if (enemySpawned < totalEnemiesPerWave * _actualWave)
        {
            _countDown = delayBetweenSpawns;
            
            SpawnEnemy(spawnerManager,spawnGroup);
        }
    }

    protected override void SpawnerLogic(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
       WaveCheck();
       
       if (_actualWave > numberOfWaves)
       {
           SpawnerCompleted(spawnGroup);
       }
    }

    protected virtual void WaveCheck()
    {
        if (killedEnemies >= totalEnemiesPerWave * _actualWave) //Check if enemies had been killed
        {
            _actualWave++;
        }
    }
}