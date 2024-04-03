using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The GoalManager class is a Singleton MonoBehaviour that manages the goals in a level.
/// It holds a reference to a GoalObject prefab and a parent transform for instantiating new GoalObjects.
/// It provides methods to initialize goals, update a level goal based on an ItemType, and check if all goals are completed.
/// When all goals are completed, it invokes the OnGoalsCompleted action.
/// </summary>
public class GoalManager : Singleton<GoalManager>
{
    [SerializeField] private GoalObject goalPrefab;
    [SerializeField] private Transform goalsParent;
    private List<GoalObject> goalObjects = new List<GoalObject>();

    public Action OnGoalsCompleted;
    private bool allGoalsCompleted = false;
    public void Init(List<LevelGoal> goals)
    {
        foreach(LevelGoal goal in goals)
        {
            GoalObject goalObject = Instantiate(goalPrefab, goalsParent);
            goalObject.Prepare(goal);
            goalObjects.Add(goalObject);
        }
    }

    public void UpdateLevelGoal(ItemType itemType)
    {
        if (allGoalsCompleted) return;

        var goalObject = goalObjects.Find(goal => goal.LevelGoal.ItemType.Equals(itemType));

        if (goalObject != null)
        {
            goalObject.DecreaseCount();
            CheckAllGoalsCompleted();
        }

    }

    public bool CheckAllGoalsCompleted()
    {
        foreach(GoalObject goal in goalObjects)
        {
            if (!goal.IsCompleted())
                return false;
        }

        allGoalsCompleted = true;
        OnGoalsCompleted?.Invoke();
        return true;
    }
}