using UnityEngine;

/// <summary>
/// 
/// The Singleton class is a generic class that ensures only one instance of a MonoBehaviour subclass is created.
/// 
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(T)) as T;
            
            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
