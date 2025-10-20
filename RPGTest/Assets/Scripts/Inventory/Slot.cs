using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Transform UIIcon;

    public SlotData slotData;
    private Bag uiParent;

    private Animator selectAnim;


    private void Awake()
    {
        UIIcon = transform.Find("Icon");

        selectAnim = GetComponent<Animator>();

    }

    public void Refresh(SlotData _slotData, Bag parent)
    {
        slotData = _slotData;
        uiParent = parent;
        Image itemIcon = UIIcon.GetComponent<Image>();
        if (slotData != null)
        {
            itemIcon.sprite = slotData.itemData != null ? slotData.itemData.itemIcon : null;
            itemIcon.enabled = slotData.itemData != null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiParent.SelectUID = slotData.itemUID;
        selectAnim.SetTrigger("Select");
    }
}
