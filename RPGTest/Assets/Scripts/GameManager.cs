using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject thirdPP;
    [SerializeField] private GameObject firstPP;
    public SlotDetail SlotDetail;
    public ItemData itemData;

    private void Start()
    {
        if (thirdPP != null && firstPP != null)
        {
            thirdPP.SetActive(true);
            firstPP.SetActive(false);
        }
        else
        {
            Debug.LogError("Camera is not ready");
        }
    }


    private void Update()
    {
        OpenBag();
        CameraControll();
    }

    private void CameraControll()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            if (thirdPP.activeSelf == true)
            {
                thirdPP.SetActive(false);
                firstPP.SetActive(true);
            }
            else
            {
                thirdPP.SetActive(true);
                firstPP.SetActive(false);
            }
        }
    }

    void OpenBag()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            UIManager.Instance.OpenPanel(UIConst.Bag);
        }
    }
}
