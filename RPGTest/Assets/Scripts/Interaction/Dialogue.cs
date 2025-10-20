using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : BasePanel
{
    private Transform UINameText;
    private Transform UIContentText;
    private Transform UIContinueBtn;

    private readonly List<string> contentList = new();
    private int contentIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        InitUI();
    }


    private void InitUI()
    {
        InitName();
        InitClick();
    }


    private void InitName()
    {
        UINameText = transform.Find("NameTextBg/NameText");
        UIContentText = transform.Find("ContentText ");

        UIContinueBtn = transform.Find("ContinueButton");

    }
    private void InitClick()
    {
        UIContinueBtn.GetComponent<Button>().onClick.AddListener(OnContinue);
    }

    public void Refresh(string name, List<string> content)
    {
        contentIndex = 0;
        UINameText.GetComponent<TextMeshProUGUI>().text = name;
        contentList.Clear();
        if (content != null && content.Count > 0)
        {
            contentList.AddRange(content);
            UIContentText.GetComponent<TextMeshProUGUI>().text = contentList[contentIndex];
        }
    }
    private void OnContinue()
    {
        contentIndex++;
        if (contentIndex >= contentList.Count)
        {
            ClosePanel();
            return;
        }
        UIContentText.GetComponent<TextMeshProUGUI>().text = contentList[contentIndex];
    }
}
