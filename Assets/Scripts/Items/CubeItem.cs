using UnityEngine;

/// <summary>
/// 
/// CubeItem is a class that represents a cube item in the game. It inherits from the Item class.
/// 
/// </summary>
public class CubeItem : Item
{
    private MatchType matchType;
    
    public void PrepareCubeItem(ItemBase itemBase,MatchType matchType)
    {
        SoundID = SoundID.Cube;
        this.matchType = matchType;
        itemBase.Clickable = true;
        Prepare(itemBase, GetSpritesForMatchType());
    }
    private Sprite GetSpritesForMatchType()
    {
        var imageLibrary = ItemImageLibrary.Instance;
        switch(matchType)
        {
            case MatchType.Green:
                return imageLibrary.GreenCubeSprite;
            case MatchType.Yellow:
                return imageLibrary.YellowCubeSprite;
            case MatchType.Blue:
                return imageLibrary.BlueCubeSprite;
            case MatchType.Red:
                return imageLibrary.RedCubeSprite;
        }
        return null;
    }
    public override MatchType GetMatchType()
    {
        return matchType;
    }
    public override void HintUpdateToSprite(ItemType itemType)
    {
        var imageLibrary = ItemImageLibrary.Instance;

        switch(itemType)
        {
            case ItemType.TNT:
                UpdateColorfulBombSprite(imageLibrary);
                break;
            default:
                UpdateSprite(GetSpritesForMatchType());
                break;
        }
    }
    private void UpdateColorfulBombSprite(ItemImageLibrary imageLibrary)
    {
        Sprite newSprite;
        switch (matchType)
        {
            case MatchType.Green:
                newSprite = imageLibrary.GreenCubeBombHintSprite;
                break;
            case MatchType.Yellow:
                newSprite = imageLibrary.YellowCubeBombHintSprite;
                break;
            case MatchType.Blue:
                newSprite = imageLibrary.BlueCubeBombHintSprite;
                break;
            case MatchType.Red:
                newSprite = imageLibrary.RedCubeBombHintSprite;
                break;
            default:
                return;
        }
        UpdateSprite(newSprite);
    }
    
    public override void TryExecute()
    {
        ParticleManager.Instance.PlayParticle(this);
        AudioManager.Instance.PlayEffect(SoundID);
        base.TryExecute();
    }
}