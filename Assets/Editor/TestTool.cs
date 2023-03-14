using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestTool : EditorWindow
{
    public GameObject selectedObject;
    Editor editor;
    public Camera camera;


    [MenuItem("Tools/TestTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TestTool));
    }
    private void OnGUI()
    {
        GUIStyle bgColor = new GUIStyle();
        bgColor.normal.background = EditorGUIUtility.whiteTexture;

        camera = EditorGUILayout.ObjectField("Camera", camera, typeof(Camera), false) as Camera;

        Handles.DrawCamera(GUILayoutUtility.GetRect(256, 256), camera);

        //Display the selected block
        if (selectedObject != null)
        {
            if (editor == null)
            {
                Debug.Log(selectedObject);
                editor = Editor.CreateEditor(selectedObject);
            }
            editor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgColor);
            //editor = Editor.CreateEditor(selectedObject);
            editor.DrawHeader();
        } 
    }

    private void OnSelectionChange()
    {
        Debug.Log("Selection change");
        //Debug.Log("editor.target: " + Selection.activeObject);
        EditorGUI.BeginChangeCheck();
        //selectedObject = (GameObject)Selection.activeObject;
        EditorGUI.EndChangeCheck();
        //Debug.Log(selectedObject);
        Repaint();
    }
}
