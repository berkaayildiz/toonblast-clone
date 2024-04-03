using System;

/// <summary>
/// 
/// StoneItem is a class that represents a stone item in the game. It inherits from the Item class.
/// 
/// </summary>
public class StoneItem : Item
{
    private const int HEALTH = 1;

    public void PrepareStoneItem(ItemBase itemBase)
    {
        SoundID = SoundID.Stone;
        itemBase.IsFallable = false;
        itemBase.Health = HEALTH;
        itemBase.InterectWithExplode = false;
        itemBase.Clickable = false;
        Prepare(itemBase, ItemImageLibrary.Instance.GetSpriteForItemType(itemBase.ItemType));
       
    }

    public override void TryExecute()
    {
        ParticleManager.Instance.PlayParticle(this);
        AudioManager.Instance.PlayEffect(SoundID);
        base.TryExecute();
    }
}
