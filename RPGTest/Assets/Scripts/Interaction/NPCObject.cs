using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCObject : InteractableObject
{
    public string npcName;
    public List<string> contentList;
    public NPCDetailSO npcSO;

    private void Awake()
    {
        if (npcSO != null)
        {
            npcName = npcSO.NPCName;
            contentList = npcSO.contentList;
        }
    }

    public override void Interact()
    {
        if (contentList.Count > 0)
        {
            BasePanel dialogue = UIManager.Instance.OpenPanel(UIConst.Dialogue);
            dialogue.GetComponent<Dialogue>().Refresh(npcName, contentList);
        }
    }
}
