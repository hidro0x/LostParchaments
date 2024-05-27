using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCam : MonoBehaviour
{
    [SerializeField] private List<QuestTarget> quests;

    private void OnEnable()
    {
        QuestManager.OnQuestCompleted += InvokeCam;
    }

    private void InvokeCam(Quest obj)
    {
        foreach (var questTarget in quests)
        {
            if (questTarget.Quest == obj)
            {
                q
            }
        }
    }

    private void OnDisable()
    {
        QuestManager.OnQuestCompleted -= InvokeCam;
    }
}

[Serializable]
public class QuestTarget
{
    public Quest Quest;
    public Transform Destination;
}


