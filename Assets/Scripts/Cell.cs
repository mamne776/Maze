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
    public Cell[] neighbours; //0 is left, 1 is up, 2 is right and 3 is down

    public bool[] walls; //true means that the cell has a wall in the corresponding edge, walls[0] = top edge, walls[1] = right etc. 

    public Cell()
    {
        widthPos = 0;
        heightPos = 0;
        hasBeenChecked = false;
        neighbours = new Cell[4] { null, null, null, null };
        //in the beginning the cell has all of its walls
        walls = new bool[4] { true, true, true, true };
        surroundingWalls = 4;
    }       
}
