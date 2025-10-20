using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance;

    private Transform _uiRoot;
    private Dictionary<string, string> pathDict;
    private Dictionary<string, GameObject> prefabDict;
    public Dictionary<string, BasePanel> panelDict;

    public static UIManager Instance
    {
        get
        {
            instance ??= new UIManager();
            return instance;
        }
    }

    private Transform UIRoot
    {
        get
        {
            if (_uiRoot == null)
            {
                _uiRoot = GameObject.Find("Canvas").transform;
            }
            return _uiRoot;
        }
    }

    private UIManager()
    {
        InitDict();
    }

    private void InitDict()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();
        pathDict = new Dictionary<string, string>()
        {
            {UIConst.Bag, "UI/Bag" },
            {UIConst.Dialogue, "UI/Dialogue" }
        };
    }

    public BasePanel OpenPanel(string name)
    {
        if (panelDict.ContainsKey(name))
        {
            Debug.LogError("�����Ѵ�" + name);
            return null;
        }

        if (!pathDict.TryGetValue(name, out string path))
        {
            Debug.LogError("�������ƴ���,����·��δ����" + name);
            return null;
        }

        //ʹ�û����Ԥ�Ƽ�
        if (!prefabDict.TryGetValue(name, out GameObject panelPrefab))
        {
            string realpath = "Prefabs/" + path;
            panelPrefab = Resources.Load<GameObject>(realpath);
            prefabDict.Add(name, panelPrefab);
        }

        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        BasePanel panel = panelObject.GetComponent<BasePanel>();
        panel.OpenPanel(name);
        panelDict.Add(name, panel);
        return panel;
    }

    public bool ClosePanel(string name)
    {
        if (!panelDict.TryGetValue(name, out BasePanel panel))
        {
            Debug.LogError("����δ��" + name);
            return false;
        }

        panel.ClosePanel();
        return true;

    }
}

//UI�ĳ�����
public class UIConst
{
    public const string Bag = "Bag";
    public const string Dialogue = "Dialogue";
}

