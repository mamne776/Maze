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

    //Blocks
    [Header("Blocks")]
    public GameObject basicBlock;
    public GameObject deadEndBlock;
    public GameObject hallwayBlock;
    public GameObject cornerBlock;
    public GameObject TBlock;
    public GameObject crossingBlock;
    //public GameObject BasicBlock;



}
