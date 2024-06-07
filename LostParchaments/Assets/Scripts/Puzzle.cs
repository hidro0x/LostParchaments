using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] private int puzzleID;
    private bool _isCompleted;

    public bool IsCompleted => _isCompleted;
    public int PuzzleID => puzzleID;

    protected void CompletePuzzle() => _isCompleted = true;

    public abstract bool CheckPuzzle();
}
