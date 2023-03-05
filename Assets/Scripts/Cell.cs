using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    //height and width -> coordinates
    public int heightPos;
    public int widthPos;
    public int surroundingWalls;
    public bool hasBeenChecked;
    //neighbouring cells
    public List<Cell> neighbours;

    public bool[] walls; //true means that the cell has a wall in the corresponding edge, walls[0] = top edge, walls[1] = right etc. 

    public Cell()
    {
        widthPos = 0;
        heightPos = 0;
        hasBeenChecked = false;
        neighbours = new List<Cell>();
        walls = new bool[4] { true, true, true, true }; //in the beginning the cell has all of its walls
        surroundingWalls = 4;
    }       
}
