using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MazeTool : EditorWindow
{
    Editor editor;

    private MazeGenerator mazeMaker;
    private MazePrinter mazePrinter;

    public MazeToolSO mazeToolSO;

    int mazeHeight, mazeWidth;
    Cell[,] createdMaze;

    string objectBaseName = "";
    int objectID = 1;

    int xCoord;
    int zCoord;


    Quaternion q;

    public GameObject blockToSpawn;

    GameObject[] blocks;

    [MenuItem("Tools/MazeTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MazeTool));
    }

    private void OnGUI()
    {
        GUIStyle bgColor = new GUIStyle();
        bgColor.normal.background = EditorGUIUtility.whiteTexture;

        GUILayout.Label("Make a maze", EditorStyles.boldLabel);

        //mazeMaker = EditorGUILayout.ObjectField("MazeMaker", mazeMaker, typeof(MazeGenerator), false) as MazeGenerator;
        mazeMaker = mazeToolSO.mazeMaker;
        //mazePrinter = EditorGUILayout.ObjectField("Maze Printer", mazePrinter, typeof(MazePrinter), false) as MazePrinter;
        mazePrinter = mazeToolSO.mazePrinter;

        mazeHeight = EditorGUILayout.IntField("Maze Height", mazeHeight);
        mazeWidth = EditorGUILayout.IntField("Maze Width", mazeWidth);

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
        if (GUILayout.Button("Face Left"))
        {
            q = faceLeftQ;
            //Debug.Log(q);
        }
        if (GUILayout.Button("Face Up"))
        {
            q = faceUpQ;
            //Debug.Log(q);
        }
        if (GUILayout.Button("Face Right"))
        {
            q = faceRightQ;
        }
        if (GUILayout.Button("Face Down"))
        {
            q = faceDownQ;
        }
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
            //Debug.Log("Test Button pressed");
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        //Display the block we are spawning
        if (blockToSpawn != null)
        {
            if (editor == null)
            {
                editor = Editor.CreateEditor(blockToSpawn);
            }
            editor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgColor);            
            //editor.DrawPreview(GUILayoutUtility.GetRect(256, 256));
        }

        if (GUILayout.Button("Save changes to tool"))
        {
            SaveChanges();
        }

        if (GUILayout.Button("Load tool"))
        {
            //
        }

        if(GUILayout.Button("Delete all blocks"))
        {
            DeleteAllBlocks();
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

    /*
    private void ShowInScene()
    {
        if (blockToSpawn == null)
        {
            Debug.Log("Assign block to be spawned");
            return;
        }

        Vector3 spawnPos = new Vector3(xCoord * 8f, 0, zCoord * 8);
        GameObject newBlock = Instantiate(blockToSpawn, spawnPos, Quaternion.identity);

    }
    */
}
