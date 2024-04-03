using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// The Item class represents an item in the game. It contains properties such as ItemType, Clickable, InterectWithExplode, IsFallable, Health, and others.
/// It also contains methods for preparing the item, adding a sprite, getting the match type, executing the item, removing the item, updating the sprite, hinting update to sprite, and making the item fall.
/// The class is derived from MonoBehaviour, allowing it to be attached to a GameObject and use Unity's methods.
/// 
/// </summary>
public class Item : MonoBehaviour
{
    private const int BaseSortingOrder = 10;
    private static int childSpriteOrder;
    public SpriteRenderer SpriteRenderer;

    public ItemType ItemType;
    public bool Clickable;
    public bool InterectWithExplode;
    public bool IsFallable;
    public int Health;

    public FallAnimation FallAnimation;
    public ParticleSystem Particle;
    private Cell cell;
    public SoundID SoundID = SoundID.None;
    public Cell Cell
    {
        get { return cell; }
        set
        {
            if(cell == value) return;

            var oldCell = cell;
            cell = value;

            if (oldCell != null && oldCell.item == this)
                oldCell.item = null;
    
            if(value != null)
            {
                value.item = this;
                gameObject.name = cell.gameObject.name + " " + GetType().Name;
            }
        }
    }


    public void Prepare(ItemBase itemBase, Sprite sprite)
    {
        SpriteRenderer = AddSprite(sprite);

        ItemType = itemBase.ItemType;
        Clickable = itemBase.Clickable;
        InterectWithExplode = itemBase.InterectWithExplode;
        IsFallable = itemBase.IsFallable;
        FallAnimation = itemBase.FallAnimation;
        Health = itemBase.Health;
        FallAnimation.item = this;
    }

    public SpriteRenderer AddSprite(Sprite sprite)
    {
        var spriteRenderer = new GameObject("Sprite_" + childSpriteOrder).AddComponent<SpriteRenderer>();
        spriteRenderer.transform.SetParent(transform);
        spriteRenderer.transform.localPosition = Vector3.zero;
        spriteRenderer.transform.localScale = new Vector2(0.7f, 0.7f);
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingLayerID = SortingLayer.NameToID("Cell");
        spriteRenderer.sortingOrder = BaseSortingOrder + childSpriteOrder++;

        return spriteRenderer;
    }

    public virtual MatchType GetMatchType() { return MatchType.None; }

    public virtual void TryExecute()
    {
        GoalManager.Instance.UpdateLevelGoal(ItemType);
        RemoveItem();
    }
    public void RemoveItem()
    {
        Cell.item = null;
        Cell = null;
        Destroy(gameObject);
    }

    public void UpdateSprite(Sprite sprite)
    {
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite; 
    }

    public virtual void HintUpdateToSprite(ItemType itemType)
    {
        return;
    }
    public void Fall()
    {
        if (!this.IsFallable) return;

        FallAnimation.FallTo(cell.GetFallTarget());
    }
}
