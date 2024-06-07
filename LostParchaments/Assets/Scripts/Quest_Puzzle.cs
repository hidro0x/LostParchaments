using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle Gorevi")]
public class Quest_Puzzle : Quest
{
    [SerializeField] private int targetPuzzleID;

    public override bool Check()
    {
        if (PuzzleManager.Instance.GetPuzzleByID(targetPuzzleID).CheckPuzzle())
        {
            CompleteQuest();
            return true;
        }

        return false;
    }

    public override string Progress()
    {
        return "0/1";
    }
}
