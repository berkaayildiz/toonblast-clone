using UnityEngine;


/// <summary>
/// 
/// LevelCompletionManager is a MonoBehaviour that listens to the OnGoalsCompleted event from GoalManager.
/// 
/// </summary>
public class LevelCompletionManager : MonoBehaviour
{
    [SerializeField] private GameGrid board;

    private void OnEnable()
    {
        GoalManager.Instance.OnGoalsCompleted += HandleGoalsCompleted;
    }

    private void OnDisable()
    {
        GoalManager.Instance.OnGoalsCompleted -= HandleGoalsCompleted;
    }

    private void HandleGoalsCompleted()
    {
        UIManager.Instance.SetLevelCompletedPanel();
    }
}
