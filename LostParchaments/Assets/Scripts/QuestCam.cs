using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class QuestCam : MonoBehaviour
{
    [SerializeField] private List<QuestTarget> quests;
    [SerializeField] private CinemachineVirtualCamera questCam;

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
                StartCoroutine(LookAtDestination(questTarget.Destination));
            }
        }
    }

    private void OnDisable()
    {
        QuestManager.OnQuestCompleted -= InvokeCam;
    }
    
    
    void SetCam(Transform pos)
    {
        questCam.LookAt = pos;
        questCam.Follow = pos;
        questCam.Priority = 11;
    }

    void OffCam() => questCam.Priority = 9;
    
    private IEnumerator LookAtDestination(Transform pos)
    {
        SetCam(pos);

        yield return new WaitForSeconds(4f);
        
        OffCam();
    }
}



[Serializable]
public class QuestTarget
{
    public Quest Quest;
    public Transform Destination;
}


