using System;

/// <summary>
/// The LevelGoal class represents a goal for a specific level in the game. 
/// Each goal is defined by an ItemType and a Count, representing the type of item needed to complete the goal and the number of those items respectively.
/// </summary>
[Serializable]
public class LevelGoal
{
    public ItemType ItemType;
    public int Count;
}
