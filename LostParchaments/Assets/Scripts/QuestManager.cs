using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Quest _activeQuest;
    private QuestUI _questUI;
    
    public static Action<Quest> OnQuestStarted;
    public static Action<Quest> OnQuestCompleted;
    public static Action<QuestType> UpdateQuestProgress; 

    private void OnEnable()
    {
        OnQuestStarted += SetActiveQuest;
        OnQuestCompleted += CompleteQuest;
        UpdateQuestProgress += CheckProgress;
    }
    
    private void OnDisable()
    {
        OnQuestStarted -= SetActiveQuest;
        OnQuestCompleted -= CompleteQuest;
        UpdateQuestProgress -= CheckProgress;
    }
    
    private void CheckProgress(QuestType type)
    {
        if(_activeQuest.Type != type) return;
        if (_activeQuest.Check())
        {
            CompleteQuest(_activeQuest);
        }
        
        _questUI.RefreshUI(_activeQuest);
    }

    private void CompleteQuest(Quest quest)
    {
        _questUI.CompleteUI();
    }

    private void SetActiveQuest(Quest quest)
    {
        if (quest.GetQuest())
        {
            _activeQuest = quest;
            _questUI.SetUI(_activeQuest);
        }
        
    }
}