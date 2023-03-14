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

    [MenuItem("Tools/MazeTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MazeTool));
    }

    private void OnGUI()
    {
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

        blockToSpawn = EditorGUILayout.ObjectField("Block to Spawn", blockToSpawn, typeof(GameObject), false) as GameObject;

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
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        //Display the block we are spawning

        Handles.DrawCamera(GUILayoutUtility.GetRect(256, 256), blockCamera);


        /*
        if (blockToSpawn != null)
        {
            if (editor == null)
            {
                editor = Editor.CreateEditor(blockToSpawn);
            }
            editor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgColor);
            //editor.DrawPreview(GUILayoutUtility.GetRect(256, 256));
        }
        */

        //delete all blocks
        if (GUILayout.Button("Delete all blocks"))
        {
            DeleteAllBlocks();
        }

        //if any changes are made in the tool
        if (GUI.changed)
        {
            Debug.Log("GUI changed");
        }
    }

    private void OnSelectionChange()
    {
        Debug.Log("Selection change");
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
