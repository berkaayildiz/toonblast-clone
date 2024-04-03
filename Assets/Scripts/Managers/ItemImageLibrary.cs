using UnityEngine;

/// <summary>
/// 
/// The ItemImageLibrary class is a Singleton that is responsible for storing all the item images in the game.
/// 
/// </summary>
public class ItemImageLibrary : Singleton<ItemImageLibrary>
{
    [Header("Cubes")]
    public Sprite GreenCubeSprite;
    public Sprite GreenCubeBombHintSprite;

    public Sprite YellowCubeSprite;
    public Sprite YellowCubeBombHintSprite;

    public Sprite BlueCubeSprite;
    public Sprite BlueCubeBombHintSprite;

    public Sprite RedCubeSprite;
    public Sprite RedCubeBombHintSprite;

    [Header("Obstacles")]
    public Sprite BoxSprite;
    public Sprite StoneSprite;
    public Sprite VaseLayer1Sprite;
    public Sprite VaseLayer2Sprite;

    [Header("TNT")]
    public Sprite TNTSprite;


    public Sprite GetSpriteForItemType(ItemType itemType)
    {
        switch(itemType)
        {
            // Cubes
            case ItemType.GreenCube: return GreenCubeSprite;
            case ItemType.YellowCube: return YellowCubeSprite;
            case ItemType.BlueCube: return BlueCubeSprite;
            case ItemType.RedCube: return RedCubeSprite;
            // Obstacles
            case ItemType.Box: return BoxSprite;
            case ItemType.Stone: return StoneSprite;
            case ItemType.VaseLayer1: return VaseLayer1Sprite;
            case ItemType.VaseLayer2: return VaseLayer2Sprite;
            // TNT
            case ItemType.TNT: return TNTSprite;

            default: return null;
        }
    }
}
