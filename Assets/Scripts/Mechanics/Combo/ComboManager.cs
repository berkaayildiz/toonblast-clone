using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ComboManager : Singleton<ComboManager>
{
    private Dictionary<ComboType, ComboEffect> comboEffects;
    private List<Cell> matchedCells;
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        comboEffects = new Dictionary<ComboType, ComboEffect>
        {
            {ComboType.TNTtoTNT, ScriptableObject.CreateInstance<TNTtoTNTCombo>()},
        };
    }

    public ComboType GetComboType(Cell cell)
    {
        matchedCells = MatchingManager.Instance.FindMatches(cell, MatchType.Special);
        if (matchedCells.Count <= 1) return ComboType.None;

        int bombCount = 0;


        foreach(var matchedCell in matchedCells)
        {
            var matchedItem = matchedCell.item;
            if (matchedItem.ItemType == ItemType.TNT)
                bombCount++;    
        }

       return ComboType.TNTtoTNT;
       
    }

    public async void TryExecute(Cell cell)
    {
        ComboType comboType = GetComboType(cell);

        if(comboEffects.TryGetValue(comboType, out var comboEffect))
        {
            comboEffect.ApplyEffect(cell, matchedCells);

            // Disable the inputs during animation
            this.GetComponent<TouchManager>().enabled = false;
            await Task.Delay(TimeSpan.FromSeconds(1));
            this.GetComponent<TouchManager>().enabled = true;
        }
        else
            cell.item.TryExecute();

        _ = MovesManager.Instance.DecreaseMovesAsync();


    }
}
