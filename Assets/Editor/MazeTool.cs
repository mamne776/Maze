using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MazeTool : EditorWindow
{
    Editor editor;

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
    //string[] options = new string[] { "test", "test2", "test3" };
    //string[] options = new string[];

    Rect windowRect = new Rect(100, 100, 200, 200);


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

        mazeHeight = EditorGUILayout.IntField("Maze Height", mazeHeight);
        mazeWidth = EditorGUILayout.IntField("Maze Width", mazeWidth);

        objectBaseName = mazeToolSO.baseNameForBlocks;

        //rotations
        Quaternion faceLeftQ = Quaternion.Euler(0, -90, 0);
        Quaternion faceRightQ = Quaternion.Euler(0, 90, 0);
        Quaternion faceDownQ = Quaternion.Euler(0, 180, 0);
        Quaternion faceUpQ = Quaternion.Euler(0, 0, 0);

        //create and print a maze
        if (GUILayout.Button("MakeMaze"))
        {
            createdMaze = mazeMaker.CreateMaze(mazeHeight, mazeWidth);
            mazePrinter.PrintMaze(createdMaze);
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




        

        
        Rect cameraRect = GUILayoutUtility.GetRect(0, 512, 0, 1028 );
        //cameraRect = GUILayout.Window(1, cameraRect, DoWindow, "Yes");
        //cameraRect.position = new Vector2(0, 256);
        Handles.DrawCamera(cameraRect, blockCamera);




        


        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Face Up", GUILayout.Width(80), GUILayout.Height(30))) { q = faceUpQ; }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Face Left", GUILayout.Width(80), GUILayout.Height(30))) { q = faceLeftQ; }
        if (GUILayout.Button("Face Right", GUILayout.Width(80), GUILayout.Height(30))) { q = faceRightQ; }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Face Down", GUILayout.Width(80), GUILayout.Height(30))) { q = faceDownQ; }
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

        Debug.Log("Block to spawn: " + blockToSpawn);

        Vector3 spawnPos = new Vector3(xCoord * 8f, 0, zCoord * 8);
        GameObject newBlock = Instantiate(blockToSpawn, spawnPos, q);

        //new stuff
        //replace in array
        createdMaze[zCoord, xCoord] = new Cell() { widthPos = xCoord, heightPos = zCoord, hasBeenChecked = true };

        //still need neighbours Cell[4], walls bool[4] and surroundingwalls int < 5
        mazeMaker.SetNeighbours(createdMaze[xCoord, zCoord]);      
        
        //set walls

        //NO STUPID
        /*
        //always wall on the top if on the top row
        Debug.Log("heightpos: " + createdMaze[zCoord, xCoord].heightPos + ", createdMaze.GetLength(0): " + createdMaze.GetLength(0));
        if (createdMaze[zCoord, xCoord].heightPos < createdMaze.GetLength(0))
        {
            //check the neighbours wall situation. if neighbour has wall there, so does this cell on the opposing edge            
                createdMaze[zCoord, xCoord].walls[1] = createdMaze[zCoord + 1, xCoord].walls[3];            
        }

        //always wall on bottom if on the bottom row
        if (createdMaze[zCoord, xCoord].heightPos > 0)
        {
            createdMaze[zCoord, xCoord].walls[3] = createdMaze[zCoord - 1, xCoord].walls[1];
        }
        */

        newBlock.name = objectBaseName + objectID;
        objectID++;

        //testing
        DeleteAllBlocks();
        mazePrinter.PrintMaze(createdMaze);
        //
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
