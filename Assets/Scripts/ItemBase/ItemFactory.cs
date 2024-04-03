using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// The ItemFactory class is a Singleton that is responsible for creating different types of game items based on the ItemType.
/// It uses the Factory design pattern to create different types of items (CubeItem, TNTItem, BoxItem, StoneItem, VaseItem) based on the ItemType.
///
/// </summary>
public class ItemFactory : Singleton<ItemFactory>
{
    public ItemBase ItemBasePrefab;

    private Dictionary<ItemType, Func<ItemBase, Item>> itemCreators = new Dictionary<ItemType, Func<ItemBase, Item>>
    {
        { ItemType.GreenCube, (itemBase) => CreateCubeItem(itemBase, MatchType.Green) },
        { ItemType.BlueCube, (itemBase) => CreateCubeItem(itemBase, MatchType.Blue) },
        { ItemType.RedCube, (itemBase) => CreateCubeItem(itemBase, MatchType.Red) },
        { ItemType.YellowCube, (itemBase) => CreateCubeItem(itemBase, MatchType.Yellow) },
        { ItemType.Box, CreateBoxItem },
        { ItemType.Stone, CreateStoneItem },
        { ItemType.VaseLayer1, CreateVaseItem },
        { ItemType.VaseLayer2, CreateVaseItem },
        { ItemType.TNT, CreateTNTItem }
    };

    public Item CreateItem(ItemType itemType, Transform parent)
    {
        if(itemType == ItemType.None) return null;

        var itemBase = Instantiate(ItemBasePrefab, Vector3.zero, Quaternion.identity, parent);
        itemBase.ItemType = itemType;

        if (!itemCreators.TryGetValue(itemType, out var createItem))
        {
            Debug.LogWarning("Can not create item: " + itemType);
            return null;
        }

        return createItem(itemBase);
    }

    private static Item CreateCubeItem(ItemBase itemBase, MatchType matchType)
    {
        var cubeItem = itemBase.gameObject.AddComponent<CubeItem>();
        cubeItem.PrepareCubeItem(itemBase, matchType);
        return cubeItem;
    }

    private static Item CreateTNTItem(ItemBase itemBase)
    {
        var tntItem = itemBase.gameObject.AddComponent<TNTItem>();
        tntItem.PrepareTNTItem(itemBase);
        return tntItem;
    }

    private static Item CreateBoxItem(ItemBase itemBase)
    {
        var boxItem = itemBase.gameObject.AddComponent<BoxItem>();
        boxItem.PrepareBoxItem(itemBase);
        return boxItem;
    }

    private static Item CreateStoneItem(ItemBase itemBase)
    {
        var stoneItem = itemBase.gameObject.AddComponent<StoneItem>();
        stoneItem.PrepareStoneItem(itemBase);
        return stoneItem;
    }

    private static Item CreateVaseItem(ItemBase itemBase)
    {
        var vaseItem = itemBase.gameObject.AddComponent<VaseItem>();
        vaseItem.PrepareVaseItem(itemBase);
        return vaseItem;
    }
}
