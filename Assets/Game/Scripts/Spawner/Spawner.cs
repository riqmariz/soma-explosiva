using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public abstract class Spawner : ScriptableObject
{
    [SerializeField]
    protected float delayBetweenSpawns = 1;

    protected float _countDown = 0;
    protected int enemySpawned = 0;
    protected int killedEnemies = 0;

    private bool _completed = false;
    public bool Completed => _completed;

    public virtual void OnUpdateSpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        _countDown -= Time.deltaTime;
        if (_countDown <= 0)
        {
            _countDown = delayBetweenSpawns;
            
            AfterDelaySpawn(spawnerManager,spawnGroup);
        }
        
        SpawnerLogic(spawnerManager,spawnGroup);
    }

    protected abstract void AfterDelaySpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup);

    protected abstract void SpawnerLogic(SpawnerManager spawnerManager, SpawnGroup spawnGroup);

    protected void SpawnEnemy(SpawnerManager spawnerManager,SpawnGroup spawnGroup)
    {
        var objToSpawn = spawnGroup.GetEnemyFromSpawnGroup();
        var spawnArea = spawnerManager.FindSpawnArea(spawnGroup.GetSpawnAreaPosition());
        var spawnLocation = spawnArea.GetRandomSpawnLocation();
        var spawnParent = spawnGroup.GetParent();

        GameObject enemy = Instantiate(objToSpawn, spawnLocation, objToSpawn.transform.rotation, spawnParent);
        Debug.Log("From SpawnGroup - "+spawnGroup.name+" - "+enemy.name + " has been spawned at: " + Time.time);

        var spawnable = enemy.GetComponent<ISpawnable>();
        if (spawnable != null)
        {
            spawnable.Spawner = this;
        }
        else
        {
            Debug.LogWarning("The Instantiated object does not inherit from ISpawnable, so it wont advise when gets destroyed");
        }

        enemySpawned++;
    }

    public void KillEnemy()
    {
        killedEnemies++;
    }

    protected void SpawnerCompleted(SpawnGroup spawnGroup)
    {
        _completed = true;
        Debug.Log("Spawn Group: "+spawnGroup.name+" has been completed!");
    }
}