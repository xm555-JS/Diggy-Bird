using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable 0649

public class CustomMapEditorWindow : EditorWindow
{
    List<GameObject> treeList = new List<GameObject>();
    GameObject defaultMap;
    GameObject selectObj;

    Vector2 scrollVec;

    [MenuItem("Window/Editor/MapWindow")]
    static void SetUp()
    {
        GetWindow<CustomMapEditorWindow>();
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void OnGUI()
    {
        // begin initialize
        if (defaultMap == null)
            Resources.Load<GameObject>("DefaultMap");

        if (treeList.Count == 0)
        {
            GameObject[] objs = Resources.LoadAll<GameObject>("Tree");
            foreach (var obj in objs)
                treeList.Add(obj);
        }
        // end initialize

        // begin set "Default Map" button
        GUILayout.Label("Default Map", EditorStyles.boldLabel);
        if (GUILayout.Button("Create Default Map"))
        {
            Debug.Log("Create Default Map!");
        }
        GUILayout.Space(5f);
        // end set "Default Map" button

        // begin set "Tree" horizontal scroll view button
        GUILayout.Label("Create Tree Object: ", EditorStyles.boldLabel);

        float scrollHeight = 70f;
        float buttonWidth = 100f;
        float buttonHeight = 50f;

        scrollVec = GUILayout.BeginScrollView(scrollVec, true, false, GUILayout.Height(scrollHeight));
        GUILayout.BeginHorizontal(GUILayout.Width(buttonWidth * treeList.Count));

        foreach (var tree in treeList)
        {
            if (tree == null) continue;

            Texture2D preview = AssetPreview.GetAssetPreview(tree) ?? Texture2D.grayTexture;
            Rect rect = GUILayoutUtility.GetRect(buttonWidth, buttonHeight);

            if (Event.current.type == EventType.MouseDrag && rect.Contains(Event.current.mousePosition))
            {
                //Debug.Log("드래그 시작!");
                DragAndDrop.PrepareStartDrag();
                DragAndDrop.objectReferences = new Object[] { tree };
                DragAndDrop.StartDrag(tree.name);
                Event.current.Use();
            }

            GUI.Button(rect, preview);
            {
                //Debug.Log(tree.name + " Create!");
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();

        GUILayout.Space(5f);
        // end set "Tree" horizontal scroll view button
    }

    void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.DragUpdated)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            if (selectObj == null)
            {
                GameObject dragObj = DragAndDrop.objectReferences[0] as GameObject;
                if (dragObj != null)
                {
                    selectObj = (GameObject)PrefabUtility.InstantiatePrefab(dragObj);
                    selectObj.hideFlags = HideFlags.HideAndDontSave;
                }
            }
            if (selectObj != null)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                LayerMask layerMast = 1 << LayerMask.NameToLayer("Surface");
                if (Physics.Raycast(ray, out RaycastHit hit, layerMast))
                {
                    selectObj.transform.position = hit.point;
                }
                else
                {
                    selectObj.transform.position = ray.GetPoint(10f); // 히트 없으면 카메라 앞
                }
            }
            e.Use();
        }
        else if (e.type == EventType.DragPerform)
        {
            foreach (var obj in DragAndDrop.objectReferences)
            {
                DragAndDrop.AcceptDrag();

                GameObject prefab = obj as GameObject;
                if (prefab != null)
                {
                    //Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                    //LayerMask layerMast = 1 << LayerMask.NameToLayer("Surface");

                    //GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    //Undo.RegisterCreatedObjectUndo(instance, "Tree");
                    //if (Physics.Raycast(ray, out RaycastHit hit, layerMast))
                    //{
                    //    instance.transform.position = hit.point;
                    //}
                    //else
                    //{
                    //    instance.transform.position = ray.GetPoint(10f); // 히트 없으면 카메라 앞
                    //}
                    //Selection.activeObject = instance;

                    Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                    Vector3 pos = selectObj ? selectObj.transform.position : ray.GetPoint(10f);

                    GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    Undo.RegisterCreatedObjectUndo(instance, "Tree");
                    instance.transform.position = pos;
                    Selection.activeObject = instance;
                }


                //GameObject dragObj = obj as GameObject;
                //if (dragObj != null)
                //{
                //    Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                //    LayerMask layerMast = 1 << LayerMask.NameToLayer("Surface");
                //    if (Physics.Raycast(ray, out RaycastHit hit, layerMast))
                //        dragObj.transform.position = hit.point;
                //}
            }

            if (selectObj != null)
            {
                DestroyImmediate(selectObj);
                selectObj = null;
            }

            e.Use();
        }
    }
}

    //void InstanceObj(GameObject obj)
    //{
    //    GameObject instanceObj = (GameObject)PrefabUtility.InstantiatePrefab(obj);
    //    selectObj = instanceObj;

    //    Undo.RegisterCreatedObjectUndo(instanceObj, obj.name);
    //    Selection.activeObject = instanceObj;
    //}
