using UnityEngine;

[CreateAssetMenu(fileName = "Timed Wave Spawner", menuName = "Spawner/Timed Wave Spawner")]
public class TimedWaveSpawner : WaveSpawner
{
    [Tooltip("Time to spawn next wave regardless wave had been killed or not")]
    [SerializeField]
    protected float waveTimer = 30.0f;
    
    protected float _timeTillWave = 0.0f;

    public override void OnUpdateSpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        _timeTillWave += Time.deltaTime;
        base.OnUpdateSpawn(spawnerManager, spawnGroup);
    }

    protected override void WaveCheck()
    {
        if (_timeTillWave >= waveTimer)
        {
            if (_actualWave + 1 <= numberOfWaves)
            {
                NextWave();
            }
        }

        if (killedEnemies >= totalEnemiesPerWave * _actualWave)
        {
            NextWave();
        }

    }

    private void NextWave()
    {
        _actualWave++;
        _timeTillWave = 0;
    }
}