using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Canavar Oldurme")]
public class Quest_KillMob : Quest
{
    [SerializeField] private int target;
    [SerializeField] private EntityType targetType;
    public override bool Check()
    {
        var amount = DataHolder.Instance.GetData_Enemy(targetType);
        if (amount >= target)
        {
            CompleteQuest();
            return true;
        }
        
        return false;
    }

    public override string Progress()
    {
        return $"{DataHolder.Instance.GetData_Enemy(targetType)}/{target}";
    }
}
