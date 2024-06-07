using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<Puzzle> puzzles;

    public List<Puzzle> Puzzles => puzzles;
    
    public static PuzzleManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Puzzle GetPuzzleByID(int id)
    {
        return puzzles.Find(x => x.PuzzleID == id);
    }
}
