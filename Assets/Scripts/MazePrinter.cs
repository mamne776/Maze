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

    [Header("Rooms")]
    public GameObject room;

    [Header("MazeGenerator")]
    public MazeGenerator mazeGenerator;

    //rotations
    private Quaternion faceLeftQ = Quaternion.Euler(0, -90, 0);
    private Quaternion faceUpQ = Quaternion.Euler(0, 0, 0);
    private Quaternion faceRightQ = Quaternion.Euler(0, 90, 0);
    private Quaternion faceDownQ = Quaternion.Euler(0, 180, 0);


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30; //noise reduction :D
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Cell[,] cellMaze = mazeGenerator.CreateMaze(5, 3);
            PrintMaze(cellMaze);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            DestroyAllBlocks();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Cell[,] mazeWithRooms = mazeGenerator.CreateMazeWithRooms(12, 12);
            PrintMaze(mazeWithRooms);
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            PrintTest();
        }


    }

    private void PrintTest()
    {
        Cell[,] testMaze = new Cell[3, 2];
        testMaze[0, 0] = new Cell { xPos = 0, zPos = 0, hasBeenChecked = true, surroundingWalls = 2, walls = new bool[4] { true, false, false, true } };
        testMaze[0, 1] = new Cell { xPos = 0, zPos = 0, hasBeenChecked = true, surroundingWalls = 2, walls = new bool[4] { true, false, false, true } };
        testMaze[1, 0] = new Cell { xPos = 0, zPos = 0, hasBeenChecked = true, surroundingWalls = 2, walls = new bool[4] { true, false, false, true } };
        testMaze[1, 1] = new Cell { xPos = 0, zPos = 0, hasBeenChecked = true, surroundingWalls = 2, walls = new bool[4] { true, false, false, true } };
        testMaze[2, 0] = new Cell { xPos = 0, zPos = 0, hasBeenChecked = true, surroundingWalls = 2, walls = new bool[4] { true, false, false, true } };
        testMaze[2, 1] = new Cell { xPos = 0, zPos = 0, hasBeenChecked = true, surroundingWalls = 2, walls = new bool[4] { true, false, false, true } };
        PrintMaze(testMaze);
    }

    public void PrintMaze(Cell[,] givenMaze)
    {
        int mazeWidth = givenMaze.GetLength(0);
        int mazeHeight = givenMaze.GetLength(1);

        Quaternion q = Quaternion.identity;

        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeHeight; j++)
            {
                //Debug.Log(givenMaze[i, j].walls[0] + ", " + givenMaze[i, j].walls[1] + ", " + givenMaze[i, j].walls[2] + ", " + givenMaze[i, j].walls[3]);
                switch (givenMaze[i, j].surroundingWalls)
                {
                    //no walls, ie. a crossing
                    case 0:
                        GameObject.Instantiate(crossingPiece, new Vector3(i * 8f, 0, j * 8f), Quaternion.identity);
                        break;

                    //one wall
                    case 1:
                        if (givenMaze[i, j].walls[0] == true) q = faceRightQ;
                        if (givenMaze[i, j].walls[1] == true) q = faceDownQ;
                        if (givenMaze[i, j].walls[2] == true) q = faceLeftQ;
                        if (givenMaze[i, j].walls[3] == true) q = faceUpQ;

                        GameObject.Instantiate(threeWayPiece, new Vector3(i * 8f, 0, j * 8f), q);
                        break;

                    //two walls
                    case 2:
                        //corridors
                        if (givenMaze[i, j].walls[0] == false && givenMaze[i, j].walls[2] == false)
                        {
                            GameObject.Instantiate(straightHallway, new Vector3(i * 8f, 0, j * 8f), faceRightQ);
                            break;
                        }
                        if (givenMaze[i, j].walls[1] == false && givenMaze[i, j].walls[3] == false)
                        {
                            GameObject.Instantiate(straightHallway, new Vector3(i * 8f, 0, j * 8f), faceUpQ);
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

                        GameObject.Instantiate(cornerPiece, new Vector3(i * 8f, 0, j * 8f), q);
                        break;

                    case 3:
                        //deadend 
                        if (givenMaze[i, j].walls[0] == false)
                        {
                            q = faceLeftQ;
                            //Debug.Log(q);
                            //Debug.Log("faceLeftQ:" + faceLeftQ);
                        }
                        if (givenMaze[i, j].walls[1] == false)
                        {
                            q = faceUpQ;
                            //Debug.Log(q);
                            //Debug.Log("faceUpQ: " + faceUpQ);
                        }
                        if (givenMaze[i, j].walls[2] == false)
                        {
                            q = faceRightQ;
                            //Debug.Log(q);
                            //Debug.Log("faceRightQ: " + faceRightQ);
                        }
                        if (givenMaze[i, j].walls[3] == false)
                        {
                            q = faceDownQ;
                            //Debug.Log(q);
                            //Debug.Log("faceDownQ: " + faceDownQ);
                        }

                        GameObject.Instantiate(deadEndPiece, new Vector3(i * 8f, 0, j * 8f), q);
                        break;

                    //four walls, ie. a solid block
                    case 4:
                        GameObject.Instantiate(basicHallWayPiece, new Vector3(i * 8f, 0, j * 8f), Quaternion.identity);
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public void DestroyAllBlocks()
    {
        GameObject[] destroyThese = GameObject.FindGameObjectsWithTag("Block");
        foreach (var item in destroyThese)
        {
            Destroy(item);
        }
    }
}
