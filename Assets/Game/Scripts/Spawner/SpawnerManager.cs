
using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class SpawnerManager : MonoBehaviour
{
	[Header("Spawn Areas")]
	[SerializeField]
	private bool useSpawnAreaParent = true;
	[SerializeField] 
	[ConditionalHide("useSpawnAreaParent",true, false)]
	private GameObject spawnAreaParent;
	[Tooltip(
		"All the areas that the spawn can be placed, but be " +
		"aware to set properly the spawn area positions enum on each individual spawn area")]
	[SerializeField]
	[ConditionalHide("useSpawnAreaParent",true, true)]
	private List<SpawnArea> spawnAreas = new List<SpawnArea>();

	[Header("Spawn Info")] 
	[SerializeField]
	[Tooltip("When set to true, the spawn manager will wait the spawn data be setted for another script, when it's false, you should choose a spawn data on the inspector")]
	private bool waitSpawnDataToBeSetted = false;

	public bool WaitSpawnDataToBeSetted => waitSpawnDataToBeSetted;
	
	[SerializeField]
	[Tooltip("Contains the Spawn data that the spawn manager will use to spawn")]
	[ConditionalHide("waitSpawnDataToBeSetted",false, true)]
	private SpawnData spawnData;

	private bool canSpawn = false;
	
	private float _sequencedCountDown;
	private int _sequencedSpawnIndex = 0;

	private bool SpawnSequenced = false;
	private bool SpawnParallel = false;

	private bool[] sequencedDebugger;
	private bool[] parallelDebugger;
	private bool bossDebugger;

	private bool startParallelSpawn = false;

	public GameObject SequencedParent => _sequencedParent;

	public GameObject ParallelParent => _parallelParent;

	public event Action OnSpawnerCompleted = delegate {  };

	private bool completed = false;
	
	private GameObject _sequencedParent;
	private GameObject _parallelParent;

	public bool Completed => completed;

	//public event Action OnCompleteSpawn = delegate { };
	void Awake()
	{
		if (useSpawnAreaParent)
		{
			this.spawnAreas.Clear();
			var spawnAreas = spawnAreaParent.GetComponentsInChildren<SpawnArea>();
			foreach (var sa in spawnAreas)
			{
				this.spawnAreas.Add(sa);
			}
		}
		
		if (!waitSpawnDataToBeSetted)
		{
			if (spawnData != null)
			{
				OnSpawnDataSet();
			}
			else
			{
				Debug.LogError("Cannot initialize because spawn data is null");
			}
		}
		
	}

	public void SetNewSpawnData(SpawnData spawnData)
	{
		this.spawnData = spawnData;
		
		OnSpawnDataSet();
	}

	private void OnSpawnDataSet()
	{
		spawnData = Instantiate(spawnData);

		_sequencedCountDown = spawnData.TimeToStartFirstSpawn;

		sequencedDebugger = new bool[spawnData.SequencedSpawns.Length];
		parallelDebugger = new bool[spawnData.ParallelSpawns.Length];

		if (spawnData.BeginSequencedSpawnAtStart)
		{
			SpawnSequenced = true;
		}

		if (spawnData.BeginParallelSpawnAtStart)
		{
			SpawnParallel = true;
		}

		InitialSettingOn(spawnAreas);

		InitialSettingOn(spawnData.SequencedSpawns, "SequencedSpawn", out _sequencedParent);
		InitialSettingOn(spawnData.ParallelSpawns, "ParallelSpawn", out _parallelParent);

		InitialSettingOnBoss();
		
		canSpawn = true;
	}

	private void InitialSettingOnBoss()
	{
		if (spawnData.SpawnBossAfterSequence)
		{
			if (spawnData.Boss == null)
			{
				Debug.LogError("Dont have a boss group");
			}
			spawnData.Boss = SpawnGroupCheck(spawnData.Boss, _sequencedParent);
		}
	}

	private void InitialSettingOn(SpawnGroup[] spawnGroups,string parentName, out GameObject parentReference)
	{
		var spawnGroupParent = new GameObject(parentName);
		spawnGroupParent.transform.parent = transform;
		parentReference = spawnGroupParent;

		for(int i=0;i<spawnGroups.Length;i++)
		{
			var sg = spawnGroups[i];
			sg = SpawnGroupCheck(sg, parentReference);
			spawnGroups[i] = sg;
		}
	}

	private SpawnGroup SpawnGroupCheck(SpawnGroup sg, GameObject parentReference)
	{
		sg = Instantiate(sg);
		sg.SetParent(parentReference);
		if (sg.spawner == null)
		{
			Debug.LogError("spawner critical error: " + sg.name + " doesnt have a spawn type");
		}

		if ((sg.enemySpawnGroup == null) || (sg.enemySpawnGroup.Length == 0))
		{
			Debug.LogError("critical error: " + sg.name + " doesnt have a enemy group");
		}

		if ((sg.spawnPositions == null) || (sg.spawnPositions.Length == 0))
		{
			Debug.LogError("critical error: " + sg.name + " doesnt have a spawn position");
		}

		return sg;
	}

	private void InitialSettingOn(List<SpawnArea> spawnAreaList)
	{
		List<SpawnAreaPosition> tempList = new List<SpawnAreaPosition>();
		
		if (spawnAreaList.Count <= 0)
		{
			Debug.LogError("Critical Error: Spawn areas have the same position enum");
			return;
		}
		
		foreach (var sa in spawnAreaList)
		{
			if (!tempList.Contains(sa.SpawnPosition))
			{
				tempList.Add(sa.SpawnPosition);
			}
			else
			{
				Debug.LogError("Critical Error: Spawn areas have the same position enum");
			}
		}
	}
	
	void Update ()
	{
		if (canSpawn)
		{
			if (SpawnSequenced)
			{
				OnUpdateSequenceSpawn();
			}

			if (SpawnParallel)
			{
				OnUpdateParallelSpawn();
			}

		}
		
	}
	
	private void OnUpdateSequenceSpawn()
	{
		_sequencedCountDown -= Time.deltaTime;

		if (_sequencedCountDown <= 0)
		{
			startParallelSpawn = true;
			if (_sequencedSpawnIndex < spawnData.SequencedSpawns.Length)
			{
				var ss = spawnData.SequencedSpawns[_sequencedSpawnIndex];
				if (!ss.spawner.Completed)
				{
					if (!sequencedDebugger[_sequencedSpawnIndex])
					{
						sequencedDebugger[_sequencedSpawnIndex] = true;
						ss.spawner = Instantiate(ss.spawner);
						Debug.Log("Starting sequenced spawn: " + ss.name + ".");
					}

					ss.spawner.OnUpdateSpawn(this, ss);
				}
				else
				{
					UpdateSequencedSpawnIndex();
				}
			}
			else
			{
				if (spawnData.SpawnBossAfterSequence)
				{
					OnUpdateBossAfterSequence();
				}
				else
				{
					SpawnManagerSequenceCompleted();
				}
			}
		}
	}

	private void OnUpdateBossAfterSequence()
	{
		if (!spawnData.Boss.spawner.Completed)
		{
			if (!bossDebugger)
			{
				bossDebugger = true;
				spawnData.Boss.spawner = Instantiate(spawnData.Boss.spawner);
				Debug.Log("Starting boss spawn: " + spawnData.Boss.name + ".");
			}

			spawnData.Boss.spawner.OnUpdateSpawn(this, spawnData.Boss);
		}
		else
		{
			SpawnManagerSequenceCompleted();
		}
	}

	private void OnUpdateParallelSpawn()
	{
		if (startParallelSpawn && !completed)
		{
			var debugIndex = 0;
			foreach (var ps in spawnData.ParallelSpawns)
			{
				if (!ps.spawner.Completed)
				{
					if (!parallelDebugger[debugIndex])
					{
						parallelDebugger[debugIndex] = true;
						ps.spawner = Instantiate(ps.spawner);
						Debug.Log("Starting parallel spawn: " + ps.name + ".");
					}
					ps.spawner.OnUpdateSpawn(this, ps);
				}
				debugIndex++;
			}
		}
	}

	private void SpawnManagerSequenceCompleted()
	{
		if (!completed)
		{
			OnSpawnerCompleted();
			completed = true;
			Debug.Log("SpawnManager Sequence is COMPLETED!");
		}
	}

	public void UpdateSequencedSpawnIndex()
	{
		_sequencedSpawnIndex++;
		_sequencedCountDown = spawnData.TimeBetweenSequencedSpawns;
	}
	
	public void EnableSequencedSpawner()
	{
		SpawnSequenced = true;
	}
	
	public void DisableSequencedSpawner()
	{
		SpawnParallel = false;
	}

	public void EnableParallelSpawner()
	{
		SpawnParallel = true;
	}

	public void DisableParallelSpawner()
	{
		SpawnParallel = false;
	}

	public SpawnArea FindSpawnArea(SpawnAreaPosition spawnAreaPosition)
	{
		foreach (var sa in spawnAreas)
		{
			if (sa.SpawnPosition == spawnAreaPosition)
			{
				return sa;
			}
		}

		Debug.LogError("Not possible to find "+spawnAreaPosition+" returning a random spawnArea");
		return spawnAreas[Random.Range(0,spawnAreas.Count)];
	}

}

