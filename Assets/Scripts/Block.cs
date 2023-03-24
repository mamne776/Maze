using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Block : MonoBehaviour
{
    public enum BlockType
    {
        Block,
        BasicBlock,
        CornerBlock,
        DeadEndBlock,
        FullBlock,
        FullCrossingBlock,
        HallwayBlock,
        TCrossingBlock,
        RoomCornerBlock,
        RoomWallBlock,
        RoomWallWithOpeningBlock,
        Floor4x4Block
    };

    public BlockType blockType;

    public bool isStartPoint, isEndPoint;


}
