using System;
using UnityEngine;

/// <summary>
/// 
/// VaseItem is a class that represents a vase item in the game. It inherits from the Item class.
/// 
/// </summary>
public class VaseItem : Item
{
    public void PrepareVaseItem(ItemBase itemBase)
    {
        SoundID = SoundID.Vase;
        itemBase.IsFallable = true;
        itemBase.Health = GetVaseHealth(itemBase.ItemType);
        itemBase.InterectWithExplode = true;
        itemBase.Clickable = false;
        Prepare(itemBase, ItemImageLibrary.Instance.GetSpriteForItemType(itemBase.ItemType));
    }

    private int GetVaseHealth(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.VaseLayer1: return 1;
            case ItemType.VaseLayer2: return 2;
            default: return 0;
        }
    }

    public override void TryExecute()
    {
        AudioManager.Instance.PlayEffect(SoundID);
        Health--;
        if (Health < 1)
        {
            ParticleManager.Instance.PlayParticle(this);
            base.TryExecute();
        }
        else
        {
            UpdateSprite(ItemImageLibrary.Instance.VaseLayer1Sprite); 
        }
    }
}
