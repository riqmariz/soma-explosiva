using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName="New SpawnGroup",menuName="Spawn Group")]
public class SpawnGroup : ScriptableObject
{
    [Header("Spawn Group")]
    [Tooltip("Name of the Group Selection")]
    public string name;
    private GameObject Parent = null;
    private bool parentAlloc= false;

    [Header("Selection Enemy Of Group Settings")]
    [Tooltip("The group of enemies that will be used in the spawn")]
    public GameObject[] enemySpawnGroup;
    [Tooltip("The selection in the enemy spawn group will be in order, from to first to the last")]
    [ConditionalHide( new[]{"randomEnemy","randomWeightedEnemy"},new[]{true,true},false,false)]
    public bool inOrderEnemy = true;
    [Tooltip("The selection in the enemy spawn group will be randomly")]
    [ConditionalHide( new[]{"inOrderEnemy","randomWeightedEnemy"},new[]{true,true},false,false)]
    public bool randomEnemy = false;
    [Tooltip("The selection in the enemy spawn group will be randomly, but with weights associated")]
    [ConditionalHide( new[]{"inOrderEnemy","randomEnemy"},new[]{true,true},false,false)]
    public bool randomWeightedEnemy = false;
    [Tooltip("The weights in the same order as the enemy spawn group, just used when random weighted enemy selected")]
    public float[] weightsEnemy;

    [Header("Selection Spawn Area Settings")]
    [Tooltip("The group of spawn area position that will be used in the spawn, the enum is associated to a screen position previous setted")]
    public SpawnAreaPosition[] spawnPositions;
    [Tooltip("The selection in the spawn area group will be in order, from to first to the last")]
    [ConditionalHide( new[]{"randomSpawnArea","randomWeightedSpawnArea"},new[]{true,true},false,false)]
    public bool inOrderSpawn = true;
    [Tooltip("The selection in the spawn area group will be randomly")]
    [ConditionalHide( new[]{"inOrderSpawn","randomWeightedSpawnArea"},new[]{true,true},false,false)]
    public bool randomSpawnArea = false;
    [Tooltip("The selection in the spawn area group will be randomly, but with weights associated")]
    [ConditionalHide( new[]{"inOrderSpawn","randomSpawnArea"},new[]{true,true},false,false)]
    public bool randomWeightedSpawnArea = false;
    [Tooltip("The weights in the same order as the spawn areas group, just used when random weighted enemy selected")]
    public float[] weightsSpawnArea;
    
    [Header("Choose your spawner")]
    [Tooltip("the spawn type that will be used to spawn the enemies")]
    public Spawner spawner;
 
    private int _indexEnemy = 0;
    private int _indexSpawn = 0;

    private int GetIndexOfOrderEnemy()
    {
        if (_indexEnemy >= enemySpawnGroup.Length)
        {
            _indexEnemy = 0;
        }

        return _indexEnemy;
    }

    private void UpdateIndexEnemy()
    {
        _indexEnemy++;
    }
    
    private int GetIndexOfOrderSpawn()
    {
        if (_indexSpawn >= spawnPositions.Length)
        {
            _indexSpawn = 0;
        }

        return _indexSpawn;
    }

    private void UpdateIndexSpawn()
    {
        _indexSpawn++;
    }

    public GameObject GetEnemyFromSpawnGroup()
    {
        GameObject obj = null;

        if (inOrderEnemy)
        {
            obj = enemySpawnGroup[GetIndexOfOrderEnemy()];
            UpdateIndexEnemy();
        }else if (randomEnemy)
        {
            obj = enemySpawnGroup[Random.Range(0, enemySpawnGroup.Length)];
        }else if (randomWeightedEnemy)
        {
            obj = enemySpawnGroup[GetRandomWeightedIndex(weightsEnemy, enemySpawnGroup.Length)];
        }
        else
        {
            Debug.LogError("none of the options were chosen in enemy selection");
        }
        
			
        return obj;
    }
    
    public int GetRandomWeightedIndex(float[] weights, int size)
    {
        if (weights.Length != size)
        {
            Debug.LogWarning("the random weights is not the same size as the group");
        }
        
        // Get the total sum of all the weights.
        float weightSum = 0f;
        for (int i = 0; i < weights.Length; ++i)
        {
            weightSum += weights[i];
        }
 
        // Step through all the possibilities, one by one, checking to see if each one is selected.
        int index = 0;
        int lastIndex = weights.Length - 1;
        while (index <= lastIndex)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (Random.Range(0, weightSum) < weights[index])
            {
                return index;
            }
 
            // Remove the last item from the sum of total untested weights and try again.
            weightSum -= weights[index++];
        }
 
        // No other item was selected, so return very last index.
        return index;
    }

    public SpawnAreaPosition GetSpawnAreaPosition()
    {
        SpawnAreaPosition sa;
        if (inOrderSpawn)
        {
            sa = spawnPositions[GetIndexOfOrderSpawn()];
           UpdateIndexSpawn();
        }else if (randomSpawnArea)
        {
           sa = spawnPositions[Random.Range(0,spawnPositions.Length)];
        }else if (randomWeightedSpawnArea)
        {
            sa = spawnPositions[GetRandomWeightedIndex(weightsSpawnArea, spawnPositions.Length)];
        }
        else
        {
            Debug.LogError("none of the options were chosen in spawn selection, putting on center");
            sa = SpawnAreaPosition.InsideCenter;
        }
			
        return sa;
    }

    public Transform GetParent()
    {
        if (!parentAlloc) //placeholder parent
        {
            Parent = new GameObject(name);
            parentAlloc = true;
        }

        return Parent.transform;
    }

    public void SetParent(GameObject gameObject)
    {
        Parent = gameObject;
        parentAlloc = true;
    }

}