using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// An abstract class that grants that one object has exactly one instance
    /// <para>The object can be acessed with the GetInstance method, the prefab must be at Resources/Singleton and have the same name as the class
    /// </summary>


    static T Instance;
    public bool ResetInstanceOnDestroy;

    protected virtual void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this as T;
            DontDestroyOnLoad(Instance);
        }
    }

    public static T GetInstance()
    {
        if (!Instance)
        {
            Instance = (Instantiate(Resources.Load("Singletons/" + typeof(T).Name)) as GameObject).GetComponent<T>();
            DontDestroyOnLoad(Instance);
            if (!Instance)
            {
                Debug.LogError("No singleton found on resources folder at: " + "Singletons/" + typeof(T).Name + ".asset");
            }
        }

        return Instance;
    }
}
