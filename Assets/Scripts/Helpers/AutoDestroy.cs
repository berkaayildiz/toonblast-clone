using UnityEngine;

/// <summary>
/// 
/// Destroy the GameObject after a specified time.
/// 
/// </summary>
public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2.0f;
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
