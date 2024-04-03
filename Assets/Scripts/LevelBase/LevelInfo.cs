/// <summary>
/// 
/// LevelInfo is a class that is used to store the information of a level.
/// It is serializable and compatible with JSON format to be used in the game level system.
/// 
/// </summary>
[System.Serializable]
public class LevelInfo
{
    public int level_number;
    public int grid_width;
    public int grid_height;
    public int move_count;
    public string[] grid;
}
