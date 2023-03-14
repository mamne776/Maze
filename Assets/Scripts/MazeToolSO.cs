using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MazeToolData", menuName = "ScriptableObjects/MazeToolSO", order = 1)]
public class MazeToolSO : ScriptableObject
{
    public int testInt;

    public string BaseName;
    public MazeGenerator mazeMaker;
    public MazePrinter mazePrinter;

}
