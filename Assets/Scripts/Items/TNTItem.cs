using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// TNTItem is a class that represents a TNT item in the game. It inherits from the Item class.
/// 
/// </summary>
public class TNTItem : Item
{
    private readonly MatchType matchType = MatchType.Special;

    public void PrepareTNTItem(ItemBase itemBase)
    {
        SoundID = SoundID.TNT;
        var bombSprite = ItemImageLibrary.Instance.TNTSprite;
        Prepare(itemBase, bombSprite);
    }

    public override MatchType GetMatchType()
    {
        return matchType;
    }

    public override void TryExecute()
    {
        var explodeCellArea = Cell.allArea;

        AudioManager.Instance.PlayEffect(SoundID);
        base.TryExecute();

        for(int i = 0; i < explodeCellArea.Count; i++)
        {
            var currentCell = explodeCellArea[i];
            if(currentCell.item == null) continue;
            var item = currentCell.item;
            item.TryExecute();
        }
    }
}
