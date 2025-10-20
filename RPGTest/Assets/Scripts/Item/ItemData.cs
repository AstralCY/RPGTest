using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory/New ItemData")]
public class ItemData : ScriptableObject
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public List<ItemProperty> itemProperty;
    [TextArea]
    public string itemInfo;
    public GameObject itemPrefab;

    public enum ItemType
    {
        WeaponItem,
        HealItem
    }

    [Serializable]
    public class ItemProperty
    {
        public ItemPropertyType propertyType;
        public int value;
    }

    public enum ItemPropertyType
    {
        HP,
        MP,
        Energy,
        Speed,
        ATK
    }


}
