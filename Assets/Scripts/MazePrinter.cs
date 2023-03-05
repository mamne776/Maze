using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePrinter : MonoBehaviour
{
    [Header("Blocks")]
    public GameObject basicHallWayPiece;
    public GameObject threeWayPiece;
    public GameObject cornerPiece;
    public GameObject straightHallway;
    public GameObject deadEndPiece;
    public GameObject crossingPiece;

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
        Application.targetFrameRate = 30; //noice reduction :D

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
            Cell[,] cellMaze = mazeGenerator.CreateMaze(24, 24);
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
                    //no walls, ie. a crossing
                    case 0:
                        GameObject.Instantiate(crossingPiece, new Vector3(j * 8f, 0, i * 8f), Quaternion.identity);
                        break;

                    //one wall
                    case 1:
                        if (givenMaze[i, j].walls[0] == true) q = faceRightQ;
                        if (givenMaze[i, j].walls[1] == true) q = faceDownQ;
                        if (givenMaze[i, j].walls[2] == true) q = faceLeftQ;
                        if (givenMaze[i, j].walls[3] == true) q = faceUpQ;

                        GameObject.Instantiate(threeWayPiece, new Vector3(j * 8f, 0, i * 8f), q);
                        break;

                    //two walls
                    case 2:
                        //corridors
                        if (givenMaze[i, j].walls[0] == false && givenMaze[i, j].walls[2] == false)
                        {
                            GameObject.Instantiate(straightHallway, new Vector3(j * 8f, 0, i * 8f), faceRightQ);
                            break;
                        }
                        if (givenMaze[i, j].walls[1] == false && givenMaze[i, j].walls[3] == false)
                        {
                            GameObject.Instantiate(straightHallway, new Vector3(j * 8f, 0, i * 8f), faceUpQ);
                            break;
                        }

                        //corner pieces
                        //no wall left and top
                        if (givenMaze[i, j].walls[0] == false && givenMaze[i, j].walls[1] == false) q = faceLeftQ;
                        //no wall top and right
                        if (givenMaze[i, j].walls[1] == false && givenMaze[i, j].walls[2] == false) q = faceUpQ;
                        //no wall right and bottom
                        if (givenMaze[i, j].walls[2] == false && givenMaze[i, j].walls[3] == false) q = faceRightQ;
                        //no wall bottom and left
                        if (givenMaze[i, j].walls[3] == false && givenMaze[i, j].walls[0] == false) q = faceDownQ;
                       
                        GameObject.Instantiate(cornerPiece, new Vector3(j * 8f, 0, i * 8f), q);
                        break;

                    case 3:
                        //deadend
                        if (givenMaze[i, j].walls[0] == false) q = faceLeftQ;
                        if (givenMaze[i, j].walls[1] == false) q = faceUpQ;
                        if (givenMaze[i, j].walls[2] == false) q = faceRightQ;
                        if (givenMaze[i, j].walls[3] == false) q = faceDownQ;

                        GameObject.Instantiate(deadEndPiece, new Vector3(j * 8f, 0, i * 8f), q);

                        break;

                    //four walls, ie. a solid block
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
