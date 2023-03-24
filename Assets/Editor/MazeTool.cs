using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MazeTool : EditorWindow
{
    Editor editor;

    public GameObject mazeUnderWorkGO;

    public ScriptableObject currentMazeSO;

    private Camera blockCamera;

    private MazeGenerator mazeMaker;
    private MazePrinter mazePrinter;

    public MazeToolSO mazeToolSO;

    int mazeHeight, mazeWidth;

    //the meat
    Cell[,] createdMaze;

    string objectBaseName;
    int objectID = 0;

    //block we're about to spawn
    public GameObject blockToSpawn;
    //its coordinates
    int xCoord;
    int zCoord;
    //and rotation
    Quaternion q;

    int index = 0;

    //Rect windowRect = new Rect(100, 100, 200, 200);

    enum Rotation { None, Left, Up, Right, Down };
    Rotation selectedRotation = Rotation.Up;


    [MenuItem("Tools/MazeTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MazeTool));
    }

    private void OnGUI()
    {
        string[] options = new string[mazeToolSO.blocks.Length];

        for (int i = 0; i < mazeToolSO.blocks.Length; i++)
        {
            options[i] = mazeToolSO.blocks[i].name;
        }

        blockCamera = mazeToolSO.camera;

        GUIStyle bgColor = new GUIStyle();
        bgColor.normal.background = EditorGUIUtility.whiteTexture;

        GUILayout.Label("Make a maze", EditorStyles.boldLabel);

        mazeMaker = mazeToolSO.mazeMaker;
        mazePrinter = mazeToolSO.mazePrinter;

        mazeWidth = EditorGUILayout.IntField("Maze Width", mazeWidth);
        mazeHeight = EditorGUILayout.IntField("Maze Height", mazeHeight);

        objectBaseName = mazeToolSO.baseNameForBlocks;

        //rotations
        Quaternion faceLeftQ = Quaternion.Euler(0, -90, 0);
        Quaternion faceRightQ = Quaternion.Euler(0, 90, 0);
        Quaternion faceDownQ = Quaternion.Euler(0, 180, 0);
        Quaternion faceUpQ = Quaternion.Euler(0, 0, 0);











        GUILayout.Label("Current maze: ", EditorStyles.boldLabel);
        mazeUnderWorkGO = EditorGUILayout.ObjectField(mazeUnderWorkGO, typeof(GameObject), true) as GameObject;

        GUILayout.Label("Current Maze as Scriptable Object: ", EditorStyles.boldLabel);
        currentMazeSO = EditorGUILayout.ObjectField(currentMazeSO, typeof(ScriptableObject), true) as ScriptableObject;


        //create and print a maze, put it in the mazeGameObject
        if (GUILayout.Button("MakeMaze"))
        {
            createdMaze = mazeMaker.CreateMaze(mazeWidth, mazeHeight);
            mazePrinter.PrintMaze(createdMaze);
            mazeUnderWorkGO = mazePrinter.mazeGO;
        }
















        GUILayout.Label("Spawn a block", EditorStyles.boldLabel);
        objectBaseName = EditorGUILayout.TextField("Base Name", objectBaseName);

        objectID = EditorGUILayout.IntField("Object ID", objectID);

        xCoord = EditorGUILayout.IntField("X-coordinate", xCoord);
        zCoord = EditorGUILayout.IntField("Z-coordinate", zCoord);

        GUILayout.Label("Type of Block to create:", EditorStyles.wordWrappedLabel);
        EditorGUI.BeginChangeCheck();
        index = EditorGUILayout.Popup(index, options);
        EditorGUI.EndChangeCheck();
        blockToSpawn = mazeToolSO.blocks[index];







        Rect cameraRect = GUILayoutUtility.GetRect(0, 512, 0, 1028);
        //cameraRect = GUILayout.Window(1, cameraRect, DoWindow, "Yes");
        //cameraRect.position = new Vector2(0, 256);
        Handles.DrawCamera(cameraRect, blockCamera);







        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Face Up", GUILayout.Width(80), GUILayout.Height(30)))
        {
            q = faceUpQ;
            selectedRotation = Rotation.Up;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Face Left", GUILayout.Width(80), GUILayout.Height(30)))
        {
            q = faceLeftQ;
            selectedRotation = Rotation.Left;
        }
        if (GUILayout.Button("Face Right", GUILayout.Width(80), GUILayout.Height(30)))
        {
            q = faceRightQ;
            selectedRotation = Rotation.Right;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Face Down", GUILayout.Width(80), GUILayout.Height(30)))
        {
            q = faceDownQ;
            selectedRotation = Rotation.Down;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Spawn Block"))
        {
            SpawnBlock();
        }
        if (GUILayout.Button("Destroy Block"))
        {
            RemoveBlock();
        }
        //replace block
        if (GUILayout.Button("Replace block"))
        {
            ReplaceBlock();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        /*
        //save current maze (as a prefab)
        if (GUILayout.Button("Save Maze"))
        {
            SaveMaze();
        }
        */

        if (GUILayout.Button("Save Maze as SO"))
        {
            SaveMazeAsScriptableObject();
        }

        //delete all blocks
        if (GUILayout.Button("Delete all blocks"))
        {
            DeleteAllBlocks();
        }

        //if any changes are made in the tool
        if (GUI.changed)
        {
            //Debug.Log("GUI changed");
        }
    }

    private void OnSelectionChange()
    {
        //Debug.Log("Selection change");
    }

    private void SaveMazeAsScriptableObject()
    { 
        if(!Directory.Exists("Assets/Mazes"))
        {
            AssetDatabase.CreateFolder("Assets", "Mazes");
        }
        string localPath = "Assets/Mazes/" + mazeUnderWorkGO.name + ".asset";

        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        MazeSO mazeSO = ScriptableObject.CreateInstance<MazeSO>();

        mazeSO.mazeWidth = createdMaze.GetLength(0);
        mazeSO.mazeHeight = createdMaze.GetLength(1);

        mazeSO.mazeCells = createdMaze;

        AssetDatabase.CreateAsset(mazeSO, localPath);

    }

    private void LoadMazeFromScriptableObject(ScriptableObject givenMazeSO)
    { 
        
    }


    private void SaveMaze()
    {
        if (!Directory.Exists("Assets/Mazes"))
        {
            AssetDatabase.CreateFolder("Assets", "Mazes");
        }
        string localPath = "Assets/Mazes/" + mazeUnderWorkGO.name + ".prefab";

        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        PrefabUtility.SaveAsPrefabAsset(mazeUnderWorkGO, localPath);
    }


    private void DrawCurrentMaze()
    {
        //Cell[,] mazeToPrint;
        //mazePrinter.PrintMaze()
    }

    private void ReplaceBlock()
    {
        if (blockToSpawn == null)
        {
            Debug.Log("Assign block to spawn");
            return;
        }
        if (objectBaseName == string.Empty)
        {
            Debug.Log("Assign a base name for the object");
            return;
        }

        //replace in array
        createdMaze[xCoord, zCoord] = new Cell() { xPos = xCoord, zPos = zCoord, hasBeenChecked = true };

        //still need neighbours Cell[4], walls bool[4] and surroundingwalls int < 5
        mazeMaker.SetNeighbours(createdMaze[xCoord, zCoord]);

        //set walls
        SetWalls(createdMaze[xCoord, zCoord]);

        //newBlock.name = objectBaseName + objectID;
        objectID++;

        //testing
        DeleteAllBlocks();
        mazePrinter.PrintMaze(createdMaze);
        //
    }

    //
    private void SetWalls(Cell cell)
    {
        //no walls
        if (blockToSpawn.GetComponent<Block>().blockType == Block.BlockType.FullCrossingBlock)
        {
            cell.surroundingWalls = 0;

            for (int i = 0; i < 4; i++)
            {
                cell.walls[i] = false;
            }

            for (int i = 0; i < 4; i++)
            {
                if (cell.neighbours[i] != null)
                {
                    CheckWalls(cell, cell.neighbours[i]);
                }
            }
        }
        //one wall
        if (blockToSpawn.GetComponent<Block>().blockType == Block.BlockType.TCrossingBlock)
        {
            cell.surroundingWalls = 1;

            for (int i = 0; i < 4; i++)
            {
                cell.walls[i] = false;
            }

            switch (selectedRotation)
            {
                case Rotation.Left:
                    cell.walls[2] = true;
                    break;
                case Rotation.Up:
                    cell.walls[3] = true;
                    break;
                case Rotation.Right:
                    cell.walls[0] = true;
                    break;
                case Rotation.Down:
                    cell.walls[1] = true;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                if (cell.neighbours[i] != null)
                {
                    CheckWalls(cell, cell.neighbours[i]);
                }
            }
        }

        //two walls
        //Hallways
        if (blockToSpawn.GetComponent<Block>().blockType == Block.BlockType.HallwayBlock)
        {
            cell.surroundingWalls = 2;

            if (selectedRotation == Rotation.Up || selectedRotation == Rotation.Down)
            {
                cell.walls[0] = true;
                cell.walls[1] = false;
                cell.walls[2] = true;
                cell.walls[3] = false;
            }

            if (selectedRotation == Rotation.Left || selectedRotation == Rotation.Right)
            {
                cell.walls[0] = false;
                cell.walls[1] = true;
                cell.walls[2] = false;
                cell.walls[3] = true;
            }

            for (int i = 0; i < 4; i++)
            {
                if (cell.neighbours[i] != null)
                {
                    CheckWalls(cell, cell.neighbours[i]);
                }
            }
        }
        //Corners
        if (blockToSpawn.GetComponent<Block>().blockType == Block.BlockType.CornerBlock)
        {
            cell.surroundingWalls = 2;
            if (selectedRotation == Rotation.Left)
            {
                cell.walls[0] = false;
                cell.walls[1] = false;
                cell.walls[2] = true;
                cell.walls[3] = true;
            }
            if (selectedRotation == Rotation.Up)
            {
                cell.walls[0] = true;
                cell.walls[1] = false;
                cell.walls[2] = false;
                cell.walls[3] = true;
            }
            if (selectedRotation == Rotation.Right)
            {
                cell.walls[0] = true;
                cell.walls[1] = true;
                cell.walls[2] = false;
                cell.walls[3] = false;
            }
            if (selectedRotation == Rotation.Down)
            {
                cell.walls[0] = false;
                cell.walls[1] = true;
                cell.walls[2] = true;
                cell.walls[3] = false;
            }

            for (int i = 0; i < 4; i++)
            {
                if (cell.neighbours[i] != null)
                {
                    CheckWalls(cell, cell.neighbours[i]);
                }
            }
        }

        //three walls
        if (blockToSpawn.GetComponent<Block>().blockType == Block.BlockType.DeadEndBlock)
        {
            cell.surroundingWalls = 3;
            for (int i = 0; i < 4; i++)
            {
                cell.walls[i] = true;
            }

            switch (selectedRotation)
            {
                case Rotation.None:
                    break;
                case Rotation.Left:
                    cell.walls[0] = false;
                    break;
                case Rotation.Up:
                    cell.walls[1] = false;
                    break;
                case Rotation.Right:
                    cell.walls[2] = false;
                    break;
                case Rotation.Down:
                    cell.walls[3] = false;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                if (cell.neighbours[i] != null)
                {
                    CheckWalls(cell, cell.neighbours[i]);
                }
            }
        }

        //four walls
        //hollow block
        if (blockToSpawn.GetComponent<Block>().blockType == Block.BlockType.FullBlock)
        {
            cell.surroundingWalls = 4;
            for (int i = 0; i < 4; i++)
            {
                cell.walls[i] = true;
            }

            for (int i = 0; i < 4; i++)
            {
                if (cell.neighbours[i] != null)
                {
                    CheckWalls(cell, cell.neighbours[i]);
                }
            }
        }
    }


    private void CheckWalls(Cell cell, Cell neighbourCell)
    {
        //if cell to the left of its neighbour
        if (cell.xPos < neighbourCell.xPos)
        {
            //has a wall on the right edge
            if (cell.walls[2])
            {
                //but the neighbour doesnt have a wall on its left edge
                if (!neighbourCell.walls[0])
                {
                    neighbourCell.walls[0] = true;
                    neighbourCell.surroundingWalls++;
                }
            }
            //doesnt have a wall on the right edge
            else
            {
                //but the neighbour does have a wall on its left edge
                if (neighbourCell.walls[0])
                {
                    neighbourCell.walls[0] = false;
                    neighbourCell.surroundingWalls--;
                }
            }
        }
        //if cell to the right of its neighbour
        if (cell.xPos > neighbourCell.xPos)
        {
            //has a wall on the left edge
            if (cell.walls[0])
            {
                //but the neighbour doesnt have a wall on its right edge
                if (!neighbourCell.walls[2])
                {
                    neighbourCell.walls[2] = true;
                    neighbourCell.surroundingWalls++;
                }
            }
            //doesnt have a wall on the left edge
            else
            {
                //but the neighbour does have a wall on its right edge
                if (neighbourCell.walls[2])
                {
                    neighbourCell.walls[2] = false;
                    neighbourCell.surroundingWalls--;
                }
            }
        }
        //if cell below its neighbour
        if (cell.zPos < neighbourCell.zPos)
        {
            //has a wall on the top edge
            if (cell.walls[1])
            {
                //but the neighbour doesnt have a wall on its bottom edge
                if (!neighbourCell.walls[3])
                {
                    neighbourCell.walls[3] = true;
                    neighbourCell.surroundingWalls++;
                }
            }
            //doesnt have a wall on the top edge
            else
            {
                //but the neighbour does have a wall on its bottom edge
                if (neighbourCell.walls[3])
                {
                    neighbourCell.walls[3] = false;
                    neighbourCell.surroundingWalls--;
                }
            }
        }
        //if cell abowe its neighbour
        if (cell.zPos > neighbourCell.zPos)
        {
            //has a wall on the bottom edge
            if (cell.walls[3])
            {
                //but the neighbour doesnt have a wall on its top edge
                if (!neighbourCell.walls[1])
                {
                    neighbourCell.walls[1] = true;
                    neighbourCell.surroundingWalls++;
                }
            }
            //doesnt have a wall on the bottom edge
            else
            {
                //but the neighbour does have a wall on its top edge
                if (neighbourCell.walls[1])
                {
                    neighbourCell.walls[1] = false;
                    neighbourCell.surroundingWalls--;
                }
            }
        }
    }

    private void SpawnBlock()
    {
        if (blockToSpawn == null)
        {
            Debug.Log("Assign block to be spawned");
            return;
        }
        if (objectBaseName == string.Empty)
        {
            Debug.Log("Assign a base name for the object");
            return;
        }

        Vector3 spawnPos = new Vector3(xCoord * 8f, 0, zCoord * 8);
        GameObject newBlock = Instantiate(blockToSpawn, spawnPos, q);

        newBlock.name = objectBaseName + objectID;
        objectID++;
    }

    private void RemoveBlock()
    {
        if (Selection.activeGameObject.CompareTag("Block"))
        {
            DestroyImmediate(Selection.activeGameObject, false);
        }
    }

    private void DeleteAllBlocks()
    {
        GameObject[] destroyThese = GameObject.FindGameObjectsWithTag("Block");
        foreach (var item in destroyThese)
        {
            DestroyImmediate(item, false);
        }
    }
}
