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
        TCrossingBlock
    };

    public BlockType blockType; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
