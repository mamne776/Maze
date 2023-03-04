using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePrinter : MonoBehaviour
{
    [Header("Blocks")]
    public GameObject BasicHallWayPiece;
    public GameObject ThreeWallPiece;
    public GameObject CornerPiece;



    // Start is called before the first frame update
    void Start()
    {
        




    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            PrintMaze();
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            DestroyMaze();            
        }
    }

    private void PrintMaze()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject.Instantiate(BasicHallWayPiece, new Vector3(j * 8f, 0, i * 8f), Quaternion.identity);
            }
        }
    }

    private void DestroyMaze()
    {
        GameObject[] destroyThese = GameObject.FindGameObjectsWithTag("HallwayBlock");
        foreach (var item in destroyThese)
        {
            Destroy(item);
        }
    }

}
