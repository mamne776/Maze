using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MazeToolData", menuName = "ScriptableObjects/MazeToolSO", order = 1)]
public class MazeToolSO : ScriptableObject
{
    public string baseNameForBlocks;
    public MazeGenerator mazeMaker;
    public MazePrinter mazePrinter;

    [Header("Camera for preview")]
    public Camera camera;

    //blocks used to print the maze
    public GameObject[] blocks;
}
