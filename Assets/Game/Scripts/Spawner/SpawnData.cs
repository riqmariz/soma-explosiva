using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnData", menuName = "Spawn Data/Spawn Data")]
public class SpawnData : ScriptableObject
{
    [Header("Sequenced Spawns")] 
    [SerializeField]
    private float timeToStartFirstSpawn = 3;
    public float TimeToStartFirstSpawn => timeToStartFirstSpawn;
    [SerializeField] 
    private float timeBetweenSequencedSpawns = 5;
    public float TimeBetweenSequencedSpawns => timeBetweenSequencedSpawns;

    [SerializeField]
    private SpawnGroup[] sequencedSpawns;
    public SpawnGroup[] SequencedSpawns => sequencedSpawns;

    [SerializeField]
    private bool spawnBossAfterSequence = false;

    public bool SpawnBossAfterSequence => spawnBossAfterSequence;

    [SerializeField] 
    [ConditionalHide("spawnBossAfterSequence", true)]
    private SpawnGroup boss;

    public SpawnGroup Boss
    {
        get => boss;
        set => boss = value;
    }

    [Header("Parallel Spawns")]
    [SerializeField]
    private SpawnGroup[] parallelSpawns;

    public SpawnGroup[] ParallelSpawns => parallelSpawns;

    [Header("Spawn Settings")]
    [Tooltip("When it's true, sequenced spawns will start after spawn manager gets initialized. When it's false, it will wait some script to enable it")]
    [SerializeField]
    private bool beginSequencedSpawnAtStart = true;

    public bool BeginSequencedSpawnAtStart => beginSequencedSpawnAtStart;
    [SerializeField]
    [Tooltip("When it's true, parallel spawns will start after spawn manager gets initialized. When it's false, it will wait some script to enable it")]
    private bool beginParallelSpawnAtStart = true;

    public bool BeginParallelSpawnAtStart => beginParallelSpawnAtStart;
	
}