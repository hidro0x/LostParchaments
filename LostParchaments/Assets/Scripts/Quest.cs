using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Quest : ScriptableObject
{
    public string Name;
    public string Description;
    public string Dialogue;
    [SerializeField]private QuestStates state;
    [SerializeField] private QuestType type;

    public QuestStates State => state;
    public QuestType Type => type;
    
    public abstract bool Check();
    public abstract string Progress();

    protected void CompleteQuest()
    {
        state = QuestStates.QUEST_COMPLETED;
        QuestManager.OnQuestCompleted?.Invoke(this);
    } 

    public bool GetQuest()
    {
        if (state == QuestStates.QUEST_AVAILABLE)
        {
            state = QuestStates.QUEST_IN_PROGRESS;
            return true;
        }
        Debug.Log("Görev alınamıyor. DURUM: " + state);
        return false;
    }
}

[System.Serializable]
public enum QuestStates
{
    QUEST_AVAILABLE,
    QUEST_IN_PROGRESS,
    QUEST_COMPLETED,
}

[System.Serializable]
public enum QuestType
{
    KILL_MOB,
    PUZZLE,
}