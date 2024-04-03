using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingManager : Singleton<MatchingManager>
{
    [SerializeField] private GameGrid board;

    private bool[,] visitedCells;

    private int minimumMatchCount = 2;

    public void Start()
    {
        visitedCells = new bool[board.Cols, board.Rows];
    }

    public List<Cell> FindMatches(Cell cell, MatchType matchType)
    {
        var matchedCells = new List<Cell>();
        ClearVisitedCells();
        FindMatches(cell, matchType, matchedCells);

        return matchedCells;
    }

    public void FindMatches(Cell cell, MatchType matchType, List<Cell> matchedCells)
    {
        if (cell == null) return;

        var x = cell.X; 
        var y = cell.Y;

        if (visitedCells[x,y]) return;

        if(cell.item != null && cell.item.GetMatchType() == matchType && cell.item.GetMatchType() != MatchType.None)
        {
            visitedCells[x,y] = true;
            matchedCells.Add(cell);

            if (!cell.item.Clickable) return;

            var neighbours = cell.neighbours;

            if (neighbours.Count == 0) return;

            for (int i = 0; i < neighbours.Count; i++)
            {
                FindMatches(neighbours[i] , matchType, matchedCells);
            }
        }
    }

    private void ClearVisitedCells()
    {
        for(int x = 0; x < visitedCells.GetLength(0); x++)
        {
            for(int y = 0; y < visitedCells.GetLength(1); y++)
            {
                visitedCells[x, y] = false;
            }
        }
    }

    public int CountMatchedCubeItem(List<Cell> cells)
    {
        int count = 0;
        foreach(Cell cell in cells)
        {
            if(cell.item.Clickable)
                count++;
        }
        return count;
    }

    public void ExplodeMatchingCells(Cell cell)
    {
        var previousCells = new List<Cell>();

        var matchedCells = FindMatches(cell, cell.item.GetMatchType());
        var matchedCubeItemCount = CountMatchedCubeItem(matchedCells);

        if (matchedCubeItemCount < minimumMatchCount) return;

        for (int i = 0; i < matchedCells.Count; i++)
        {
            var explodedCell = matchedCells[i];

            ExplodeMatchingCellsInNeightbours(explodedCell, previousCells);

            var item = explodedCell.item;
            item.TryExecute();
        }

        _ = MovesManager.Instance.DecreaseMovesAsync();
        SpawnBonus(cell, matchedCubeItemCount);
    }
    private void SpawnBonus(Cell cell,int matchedCellCount)
    {
        switch (matchedCellCount)
        {
            case >= 5:
                cell.item = ItemFactory.Instance.CreateItem(ItemType.TNT, board.itemsParent);
                break;
          
            default: 
                break;
        }

        if(cell.item == null) return;
        cell.item.transform.position = cell.transform.position;
    }
    private void ExplodeMatchingCellsInNeightbours(Cell cell, List<Cell> previousCells)
    {
        var explodedCellNeightbours = cell.neighbours;

        for(int j = 0; j < explodedCellNeightbours.Count; j++)
        {
            var neighbourCell = explodedCellNeightbours[j];
            var neighbourCellItem = neighbourCell.item;

            if(neighbourCellItem != null && !previousCells.Contains(neighbourCell))
            {
                previousCells.Add(neighbourCell);

                if (neighbourCellItem.InterectWithExplode)
                    neighbourCellItem.TryExecute();
            }
        }
    }
}
