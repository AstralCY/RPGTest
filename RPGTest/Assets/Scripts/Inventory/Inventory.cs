using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private static Inventory instance;

    private ItemData[] itemDatas;
    public List<SlotData> bagSlots;

    public static Inventory Instance
    {
        get
        {
            instance ??= new Inventory();
            return instance;
        }
    }

    private Inventory()
    {
        LoadAllItemData();
    }

    private void LoadAllItemData()
    {
        if (itemDatas == null || itemDatas.Length == 0)
        {
            itemDatas = Resources.LoadAll<ItemData>("SOData/Item");
        }
    }

    public void AddItem(ItemData itemData)
    {
        bagSlots ??= new List<SlotData>();
        SlotData slotData = new()
        {
            itemUID = System.Guid.NewGuid().ToString(),
            itemData = itemData
        };
        bagSlots.Add(slotData);
    }

    public void RemoveItem(string _itemUID)
    {
        bagSlots.RemoveAll(slotData => slotData.itemUID == _itemUID);
    }

    public SlotData GetSlotData(string UID)
    {
        foreach (SlotData slotData in bagSlots)
        {
            if (slotData.itemUID == UID) return slotData;
        }
        return null;
    }

    public void GetRandomItem(PickableObject dropItemPrefab, Transform transform)
    {
        int randomNum = Random.Range(0, 5);
        for (int i = 0; i < randomNum; i++)
        {
            int targetID = Random.Range(0, 5);
            ItemData randomItem = GetItem(targetID);
            if (randomItem != null)
            {
                PickableObject newItem = GameObject.Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
                newItem.CopyDetail(randomItem);
            }
        }
    }

    public ItemData GetItem(int targetID)
    {
        foreach (ItemData itemData in itemDatas)
        {
            if (itemData.itemID == targetID)
            {
                return itemData;
            }
        }
        return null;
    }
}

public class SlotData
{
    public string itemUID;
    public ItemData itemData;
}
