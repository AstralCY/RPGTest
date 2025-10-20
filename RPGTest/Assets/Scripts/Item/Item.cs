using System;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    private Dictionary<ItemData.ItemPropertyType, int> propertyCache;

    protected void InitCache()
    {
        if (itemData != null)
        {
            propertyCache = new Dictionary<ItemData.ItemPropertyType, int>();
            foreach (var property in itemData.itemProperty)
            {
                propertyCache[property.propertyType] = property.value;
            }
        }
    }

    public int GetValue(ItemData.ItemPropertyType propertyType)
    {
        if (propertyCache == null) InitCache();
        return propertyCache.TryGetValue(propertyType, out var value) ? value : 0;
    }
}

public interface IUse
{
    bool Use();
}

public interface IEquip
{
    void Arm();
    void UnArm();
}
