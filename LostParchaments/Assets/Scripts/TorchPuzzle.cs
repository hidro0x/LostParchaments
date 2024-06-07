using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPuzzle : Puzzle
{
    [SerializeField] private List<Torch> torches;

    public override bool CheckPuzzle()
    {
        foreach (var torch in torches)
        {
            if (!torch.isBurning) return false;
        }

        CompletePuzzle();
        return true;
    }
}
