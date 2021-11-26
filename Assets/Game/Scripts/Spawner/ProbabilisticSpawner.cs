using UnityEngine;

[CreateAssetMenu(fileName = "Probabilistic Spawner", menuName = "Spawner/Probabilistic Spawner")]
public class ProbabilisticSpawner : Spawner
{
    [SerializeField]
    [Tooltip("Is the chance to spawn after every lotteryDelay")]
    [Range(0f,100f)]
    protected float chanceToSpawn = 5;

    [SerializeField]
    protected float timeToStopSpawner;

    protected bool timeEnded = false;
    protected float time = 0;

    public override void OnUpdateSpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        time += Time.deltaTime;
        base.OnUpdateSpawn(spawnerManager, spawnGroup);
    }

    protected override void AfterDelaySpawn(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        var random = Random.Range(0f, 100f);

        if (chanceToSpawn >= random && !timeEnded)
        {
            SpawnEnemy(spawnerManager,spawnGroup);
        }
    }

    protected override void SpawnerLogic(SpawnerManager spawnerManager, SpawnGroup spawnGroup)
    {
        if (time >= timeToStopSpawner)
        {
            timeEnded = true;
        }

        if (timeEnded)
        {
            SpawnerCompleted(spawnGroup);
        }
    }
}