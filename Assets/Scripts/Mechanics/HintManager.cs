using System;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField] private GameGrid board;
    private void ShowHints()
    {
        var visitedCells = new List<Cell>();

        for (var y = 0; y < board.Rows; y++)
        {
            for(var x = 0; x < board.Cols; x++)
            {
                var cell = board.Cells[x, y];

                if(cell.item == null || visitedCells.Contains(cell)) continue;

                var matchedCells = MatchingManager.Instance.FindMatches(cell, cell.item.GetMatchType());
                var matchedCubeCount = MatchingManager.Instance.CountMatchedCubeItem(matchedCells);
                
                visitedCells.AddRange(matchedCells);

                for (int i = 0; i < matchedCubeCount; i++)
                {
                    var currentItem = matchedCells[i].item;

                    CheckHintForCombo(currentItem, matchedCubeCount);
                    HintSpriteUpdate(currentItem, matchedCubeCount);
                }
            }
        }
    }
    private void CheckHintForCombo(Item item,int comboCount)
    {
        if(item.GetMatchType() == MatchType.Special && comboCount > 1)
        {
            if (item.Particle != null) return;

            var particle = ParticleManager.Instance.ComboHintParticle;
            var particleObj = Instantiate(particle, item.transform.position, Quaternion.identity, item.transform);
            item.Particle = particleObj;
        }
        else if(item.Particle != null)
        {
            Destroy(item.Particle.gameObject);
        }
    }
    private void HintSpriteUpdate(Item item, int matchedCount)
    {
        switch (matchedCount)
        {
            case < 5:
                item.HintUpdateToSprite(item.ItemType);
                break;
            default:
                item.HintUpdateToSprite(ItemType.TNT);
                break;
        }

    }
    private void Update()
    {
        ShowHints();
    }
}
