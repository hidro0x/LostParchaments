using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class QuestCam : MonoBehaviour
{
    [SerializeField] private List<QuestTarget> questsStart;
    [SerializeField] private List<QuestTarget> questsEnd;
    [SerializeField] private CinemachineVirtualCamera questCam;

    private void OnEnable()
    {
        QuestManager.OnQuestCompleted += InvokeCamEnd;
        QuestManager.OnQuestStarted += InvokeCamStart;
    }

    private void InvokeCamEnd(Quest obj)
    {
        foreach (var questTarget in questsEnd)
        {
            if (questTarget.Quest.Name == obj.Name)
            {
                StartCoroutine(LookAtDestination(questTarget.Destination, questTarget.Object));
                return;
            }
        }
    }
    
    private void InvokeCamStart(Quest obj)
    {
        foreach (var questTarget in questsStart)
        {
            if (questTarget.Quest.Name == obj.Name)
            {
                StartCoroutine(LookAtDestination(questTarget.Destination, questTarget.Object));
                return;
            }
        }
    }

    private void OnDisable()
    {
        QuestManager.OnQuestCompleted -= InvokeCamEnd;
        QuestManager.OnQuestStarted -= InvokeCamStart;
    }
    
    
    void SetCam(Transform pos)
    {
        questCam.LookAt = pos;
        questCam.Follow = pos;
        questCam.Priority = 11;
    }

    void OffCam() => questCam.Priority = 9;
    
    private IEnumerator LookAtDestination(Transform pos, GameObject obj)
    {
       if(obj !=null) obj.SetActive(true);
       if(pos == null) yield break;
       
        yield return new WaitForSeconds(1f);
        
        SetCam(pos);

        yield return new WaitForSeconds(3f);
        
        OffCam();
    }
}



[Serializable]
public class QuestTarget
{
    public Quest Quest;
    public Transform Destination;
    public GameObject Object;
}


