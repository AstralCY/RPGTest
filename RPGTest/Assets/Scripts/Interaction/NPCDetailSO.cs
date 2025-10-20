using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NPCDetailSO : ScriptableObject
{
    public string NPCName;
    public List<string> contentList;

    public bool isQuestGiver;          
    public List<QuestPhase> questPhases = new();
}

public enum QuestConditionType
{
    ItemCollected,
    EnemyKilled,
    DialogueCompleted
}

[System.Serializable]
public class QuestCondition
{
    public QuestConditionType conditionType;
    public string targetID;
    public int requiredAmount;
}

[System.Serializable]
public class QuestPhase
{
    public List<string> dialogues;

    public List<QuestCondition> startConditions;

    public List<QuestCondition> completeRewards;
}