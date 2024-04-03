using UnityEngine;

/// <summary>
/// 
/// The ItemBase class serves as a base class for all items in the game. It defines common properties that all items share.
/// 
/// </summary>
public class ItemBase : MonoBehaviour
{
    public ItemType ItemType;
    public bool Clickable = true;
    public bool IsFallable = true;
    public bool InterectWithExplode = false;
    public int Health = 1;
    public FallAnimation FallAnimation;
}
