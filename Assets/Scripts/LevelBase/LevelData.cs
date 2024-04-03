using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 
/// LevelData is a class that represents the data of a level in the game.
/// Constructor takes a LevelInfo object and creates the level data from it.
/// 
/// </summary>
public class LevelData
{

    public ItemType[,] GridData { get; protected set; }
    public List<LevelGoal> Goals { get; protected set; }
    public int Moves { get; protected set; }

    public LevelData(LevelInfo levelInfo)
    {
        // Count obstacles for goal data
        int numberOfBoxes = 0;
        int numberOfStones = 0;
        int numberOfVases = 0;

        // Set the grid data
        GridData = new ItemType[levelInfo.grid_height, levelInfo.grid_width];

        int gridIndex = 0;
        for (int i = levelInfo.grid_height - 1; i >= 0; --i)
            for (int j = 0; j < levelInfo.grid_width; ++j)
            {
                switch (levelInfo.grid[gridIndex++])
                {
                    // Obstacles
                    case "bo":
                        GridData[i, j] = ItemType.Box;
                        ++numberOfBoxes;
                        break;
                    case "s":
                        GridData[i, j] = ItemType.Stone;
                        ++numberOfStones;
                        break;
                    case "v":
                        GridData[i, j] = ItemType.VaseLayer2;
                       ++numberOfVases;
                        break;
                    // Cubes
                    case "b":
                        GridData[i, j] = ItemType.BlueCube;
                        break;
                    case "g":
                        GridData[i, j] = ItemType.GreenCube;
                        break;
                    case "r":
                        GridData[i, j] = ItemType.RedCube;
                        break;
                    case "y":
                        GridData[i, j] = ItemType.YellowCube;
                        break;
                    case "rand":
                        GridData[i, j] = ((ItemType[]) Enum.GetValues(typeof(ItemType)))[Random.Range(1, 5)];
                        break;
                    case "t":
                        GridData[i, j] = ItemType.TNT;
                        break;
                    default:
                        GridData[i, j] = ((ItemType[])Enum.GetValues(typeof(ItemType)))[Random.Range(1, 5)];
                        break;
                }
            }

        // Set the goals data
        Goals = new List<LevelGoal>();
        if (numberOfBoxes != 0) Goals.Add(new LevelGoal { ItemType = ItemType.Box, Count = numberOfBoxes });
        if (numberOfStones != 0) Goals.Add(new LevelGoal { ItemType = ItemType.Stone, Count = numberOfStones });
        if (numberOfVases != 0) Goals.Add(new LevelGoal { ItemType = ItemType.VaseLayer2, Count = numberOfVases });

        // Set moves
        Moves = levelInfo.move_count;
    }

    public static ItemType GetRandomCubeItemType()
    {
        return ((ItemType[])Enum.GetValues(typeof(ItemType)))[Random.Range(1, 5)]; // 1,5 represents number of blocks
    }

}
