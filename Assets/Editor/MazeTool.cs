using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MazeTool : EditorWindow
{
    Editor editor;

    string objectBaseName = "";
    int objectID = 1;

    int xCoord;
    int zCoord; 

    GameObject blockToSpawn;

    [MenuItem("Tools/MazeTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MazeTool));
    }

    private void OnGUI()
    {
        GUIStyle bgColor = new GUIStyle();
        bgColor.normal.background = EditorGUIUtility.whiteTexture;

        GUILayout.Label("Spawn a block", EditorStyles.boldLabel);
        objectBaseName = EditorGUILayout.TextField("Base Name", objectBaseName);
        objectID = EditorGUILayout.IntField("Object ID", objectID);

        xCoord = EditorGUILayout.IntField("X-coordinate", xCoord);
        zCoord = EditorGUILayout.IntField("Z-coordinate", zCoord);

        blockToSpawn = EditorGUILayout.ObjectField("Block to Spawn", blockToSpawn, typeof(GameObject), false) as GameObject;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Spawn Block"))
        {
            SpawnBlock();
        }
        if (GUILayout.Button("Show in Scene"))
        {
            ShowInScene();
            //Debug.Log("Test Button pressed");
        }
        if (GUILayout.Button("Test Button 2"))
        {
            Debug.Log("Test Button 2 pressed");
        }
        GUILayout.EndHorizontal();

        //Display the block we are spawning
        if (blockToSpawn != null)
        {
            if (editor == null)
            {
                editor = Editor.CreateEditor(blockToSpawn);
            }
            editor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgColor);
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
        GameObject newBlock = Instantiate(blockToSpawn, spawnPos, Quaternion.identity);
        newBlock.name = objectBaseName + objectID;

        objectID++;
    }

    private void ShowInScene()
    {
        if (blockToSpawn == null)
        {
            Debug.Log("Assign block to be spawned");
            return;
        }

        Vector3 spawnPos = new Vector3(xCoord * 8f, 0, zCoord * 8);
        GameObject newBlock = Instantiate(blockToSpawn, spawnPos, Quaternion.identity);
        //newBlock.GetComponents<Shader>().

    }


}
