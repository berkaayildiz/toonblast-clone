using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
public abstract class ComboEffect : ScriptableObject
{
    public abstract void ApplyEffect(Cell cell, List<Cell> matchedCells);
    public abstract List<Cell> GetAffectedCells(Cell cell);

    public float mergeAnimationTime = 0.3f;
    public Tween mergeTween;

    protected virtual void CreateMergeAnimationForMatchedCells(Cell cell, List<Cell> matchedCells)
    {
        cell.item.SpriteRenderer.sortingOrder += 10;

        foreach (var matchedCell in matchedCells)
        {
            if (matchedCell.item == cell) return;

            mergeTween = matchedCell.item.transform.DOMove(cell.transform.position, mergeAnimationTime);
        }
    }
    protected virtual void ExecuteItemsInAffectedCells(Cell cell)
    {
        List<Cell> affectedCells = GetAffectedCells(cell);
        foreach (Cell affectedCell in affectedCells)
        {
            if (affectedCell.item == null) continue;

            affectedCell.item.TryExecute();
        }
    }
    protected virtual void RemoveItemFromMatchedCells(List<Cell> matchedCells)
    {
        foreach (var matchedCell in matchedCells)
        {
            if (matchedCell.item == null) continue;
            matchedCell.item.RemoveItem();
        }
    }
    protected void PrepareCellForAnimation(Cell cell)
    {
        cell.item.IsFallable = false;
        cell.item.SpriteRenderer.enabled = false;
    }
}