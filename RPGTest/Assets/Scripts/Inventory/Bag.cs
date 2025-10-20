using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bag : BasePanel
{
    private Transform UIAllItemBtn;
    private Transform UIWeaponBtn;
    private Transform UIPotionBtn;
    private Transform UICloseBtn;
    private Transform UIScrollView;
    private Transform UISlotDetail;
    private Transform UIDeleteItemBtn;
    private Transform UICapacityText;

    private GameObject slotPrefab;
    private List<Slot> slots = new();

    private string _selectUID;
    public string SelectUID
    {
        get => _selectUID;
        set
        {
            _selectUID = value;
            RefreshDetail();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        InitUI();
    }

    private void Start()
    {
        RefreshUI();
    }

    private void InitUI()
    {
        InitName();
        InitClick();
        InitText();
        InitPrefab();
    }

    private void InitName()
    {
        UIAllItemBtn = transform.Find("LeftTop/Menus/AllItem");
        UIWeaponBtn = transform.Find("LeftTop/Menus/Weapon");
        UIPotionBtn = transform.Find("LeftTop/Menus/Potion");
        UICloseBtn = transform.Find("RightTop/Close");
        UIScrollView = transform.Find("Center/Scroll View");
        UISlotDetail = transform.Find("Center/SlotDetail");
        UIDeleteItemBtn = transform.Find("Bottom/DeleteItem");
        UICapacityText = transform.Find("Bottom/Capacity");

        UISlotDetail.gameObject.SetActive(false);
    }

    private void InitClick()
    {
        UIAllItemBtn.GetComponent<Button>().onClick.AddListener(OnAllItem);
        UIWeaponBtn.GetComponent<Button>().onClick.AddListener(OnWeapon);
        UIPotionBtn.GetComponent<Button>().onClick.AddListener(OnPotion);
        UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClose);
        UIDeleteItemBtn.GetComponent<Button>().onClick.AddListener(OnDelete);
    }

    private void InitText()
    {
        UICapacityText.GetComponent<TextMeshProUGUI>().text = string.Empty;
    }

    private void InitPrefab()
    {
        slotPrefab = Resources.Load<GameObject>("Prefabs/Inventory/Slot");
    }

    public void RefreshUI()
    {
        RefreshSlot();
    }

    private void RefreshSlot()
    {
        RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
        for (int i = scrollContent.childCount - 1; i >= 0; i--)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        slots.Clear();
        List<SlotData> slotDatas = Inventory.Instance.bagSlots;
        if (slotDatas == null || slotDatas.Count == 0) return;
        foreach (SlotData slotData in slotDatas)
        {
            GameObject slotObj = Instantiate(slotPrefab, scrollContent);
            Slot slot = slotObj.GetComponent<Slot>();
            slot.Refresh(slotData, this);
            slots.Add(slot);
        }
    }

    private void RefreshDetail()
    {
        UISlotDetail.gameObject.SetActive(true);
        SlotData slotData = Inventory.Instance.GetSlotData(SelectUID);
        UISlotDetail.GetComponent<SlotDetail>().Refresh(slotData, this);
    }

    private void OnAllItem()
    {
        SlotFilter(itemType => true);
    }

    private void OnWeapon()
    {
        SlotFilter(itemType => itemType == ItemData.ItemType.WeaponItem);
    }

    private void OnPotion()
    {
        SlotFilter(itemType => itemType == ItemData.ItemType.HealItem);
    }

    private void SlotFilter(Func<ItemData.ItemType, bool> filter)
    {
        foreach (Slot slot in slots)
        {
            bool shouldActive = filter(slot.slotData.itemData.itemType);
            slot.gameObject.SetActive(shouldActive);
        }
    }

    private void OnClose()
    {
        ClosePanel();
    }

    private void OnDelete()
    {
        Inventory.Instance.RemoveItem(SelectUID);
        RefreshUI();
    }
}
