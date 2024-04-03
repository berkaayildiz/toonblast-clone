using UnityEngine;
using DG.Tweening;

/// <summary>
/// 
/// The FallAnimation class is responsible for managing the fall animation of an Item.
/// It contains methods to validate the target cell, update the target cell, and animate the fall.
/// 
/// </summary>
public class FallAnimation : MonoBehaviour
{
    public Item item;
    [HideInInspector] public Cell targetCell;

    [SerializeField] private const float ANIMATION_DURATION = 0.35f;
    private Vector3 targetPosition;

    public void Awake()
    {
        DOTween.SetTweensCapacity(500, 50);
    }

    public void FallTo(Cell targetCell)
    {
        if (IsInvalidTargetCell(targetCell)) return;

        UpdateTargetCell(targetCell);
        AnimateFall();
    }

    private bool IsInvalidTargetCell(Cell targetCell)
    {
        return this.targetCell != null && targetCell.Y >= this.targetCell.Y;
    }

    private void UpdateTargetCell(Cell targetCell)
    {
        this.targetCell = targetCell;
        item.Cell = this.targetCell;
        targetPosition = this.targetCell.transform.position;
    }

    private void AnimateFall()
    {
        item.transform.DOMoveY(targetPosition.y, ANIMATION_DURATION)
            .SetEase(Ease.InCubic)
            .OnComplete(() => targetCell = null);
    }
}
