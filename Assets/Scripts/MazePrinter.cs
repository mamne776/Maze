using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePrinter : MonoBehaviour
{
    [Header("Blocks")]
    public GameObject basicHallWayPiece;
    public GameObject threeWallPiece;
    public GameObject cornerPiece;

    [Header("MazeGenerator")]
    public MazeGenerator mazeGenerator;

    //rotations
    private Quaternion faceLeftQ;
    private Quaternion faceRightQ;
    private Quaternion faceDownQ;
    private Quaternion faceUpQ;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //noice reduction :D

        faceLeftQ = Quaternion.Euler(0, -90, 0);
        faceRightQ = Quaternion.Euler(0, 90, 0);
        faceDownQ = Quaternion.Euler(0, 180, 0);
        faceUpQ = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            //int[,] maze = mazeGenerator.CreateTestMaze();
            Cell[,] cellMaze = mazeGenerator.CreateMaze(2,2);
            PrintMaze(cellMaze);

        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            DestroyMaze();            
        }
    }

    private void PrintMaze(Cell[,] givenMaze)
    {
        int h = givenMaze.GetLength(0);
        int w = givenMaze.GetLength(1);

        Quaternion q = Quaternion.identity;

        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                switch (givenMaze[i, j].surroundingWalls)
                {
                    //two walls
                    case 2:
                        //corridor


                        //corner pieces
                        if (givenMaze[i, j].walls[0] == false && givenMaze[i, j].walls[1] == false) q = faceLeftQ;
                        if (givenMaze[i, j].walls[1] == false && givenMaze[i, j].walls[2] == false) q = faceUpQ;
                        if (givenMaze[i, j].walls[2] == false && givenMaze[i, j].walls[3] == false) q = faceRightQ;
                        if (givenMaze[i, j].walls[3] == false && givenMaze[i, j].walls[0] == false) q = faceDownQ;
                        GameObject.Instantiate(cornerPiece, new Vector3(j * 8f, 0, i * 8f), q);
                        break;
                    case 1:
                        GameObject.Instantiate(cornerPiece, new Vector3(j * 8f, 0, i * 8f), Quaternion.identity);
                        break;
                    case 4:
                        GameObject.Instantiate(basicHallWayPiece, new Vector3(j * 8f, 0, i * 8f), Quaternion.identity);
                        break;


                    default:
                    break;
                }

                /*
                if (givenMaze[i, j] == 0)
                {
                    GameObject.Instantiate(basicHallWayPiece, new Vector3(j * 8f, 0, i * 8f), Quaternion.identity);
                }

                if (givenMaze[i,j] == 1)
                {
                    GameObject.Instantiate(cornerPiece, new Vector3(j * 8f, 0, i * 8f), Quaternion.identity);
                }
                */
                //GameObject.Instantiate(BasicHallWayPiece, new Vector3(j * 8f, 0, i * 8f), Quaternion.identity);
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
