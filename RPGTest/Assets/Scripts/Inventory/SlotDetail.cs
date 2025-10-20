using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotDetail : MonoBehaviour
{
    private Transform UIItemIcon;
    private Transform UIItemName;
    private Transform UIItemType;
    private Transform UIItemInfo;
    private Transform UIItemProperty;
    private Transform UIUseItemBtn;

    public SlotData slotData;
    private Bag uiParent;
    private void Awake()
    {
        InitName();
        InitClick();
    }

    private void InitName()
    {
        UIItemIcon = transform.Find("Top/ItemIcon");
        UIItemName = transform.Find("Top/ItemName");
        UIItemType = transform.Find("Top/ItemType");
        UIItemInfo = transform.Find("Middle/ItemInfo");
        UIItemProperty = transform.Find("Middle/ItemProperty");
        UIUseItemBtn = transform.Find("Bottom/UseItem");
    }

    private void InitClick()
    {
        UIUseItemBtn.GetComponent<Button>().onClick.AddListener(OnUseItem);
    }

    public void Refresh(SlotData _slotData, Bag parent)
    {
        slotData = _slotData;
        uiParent = parent;
        UIItemIcon.GetComponent<Image>().sprite = slotData.itemData.itemIcon;
        UIItemName.GetComponent<TextMeshProUGUI>().text = slotData.itemData.itemName;
        UIItemType.GetComponent<TextMeshProUGUI>().text = slotData.itemData.itemType.ToString();
        UIItemInfo.GetComponent<TextMeshProUGUI>().text = slotData.itemData.itemInfo;
        for (int i = 0; i < 4; i++)
        {
            if (i < slotData.itemData.itemProperty.Count)
            {
                ItemData.ItemPropertyType type = slotData.itemData.itemProperty[i].propertyType;
                int value = slotData.itemData.itemProperty[i].value;
                TextMeshProUGUI detail = UIItemProperty.GetChild(i).GetComponent<TextMeshProUGUI>();
                detail.text = $"{type} : {value}";
            }
            else
            {
                UIItemProperty.GetChild(i).GetComponent<TextMeshProUGUI>().text = string.Empty;
            }
        }
    }

    private void OnUseItem()
    {
        bool isUsed = false;
        if (slotData.itemData.itemType == ItemData.ItemType.WeaponItem)
        {
            isUsed = HandleWeaponItem();
        }
        else if (slotData.itemData.itemType == ItemData.ItemType.HealItem)
        {
            isUsed = HandleHealItem();
        }
        if (isUsed)
        {
            Inventory.Instance.RemoveItem(slotData.itemUID);
            uiParent.RefreshUI();
        }
    }

    private bool HandleWeaponItem()
    {
        PlayerManager.Instance.player.currentWeapon.UnArm();
        var prefab = slotData.itemData.itemPrefab;
        if (prefab != null)
        {
            prefab.GetComponentInChildren<IEquip>().Arm();
            return true;
        }
        else return false;

    }

    private bool HandleHealItem()
    {
        var prefab = slotData.itemData.itemPrefab;
        IUse useItem = prefab.GetComponentInChildren<IUse>();
        return useItem.Use();
    }
}
