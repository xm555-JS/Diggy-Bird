using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable 0649

public class CustomMapEditorWindow : EditorWindow
{
    static List<GameObject> treeList = new List<GameObject>();
    static List<GameObject> grassList = new List<GameObject>();
    static List<GameObject> rockList = new List<GameObject>();
    static List<GameObject> etcList = new List<GameObject>();

    static GameObject defaultMap;
    static GameObject parent;

    GameObject selectObj;

    Vector2 treeScrollVec;
    Vector2 grassScrollVec;
    Vector2 rockScrollVec;
    Vector2 etcScrollVec;

    const float scrollHeight = 70f;
    const float buttonWidth = 100f;
    const float buttonHeight = 50f;

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
        Initialize();

        CreateDefaultMap();

        SetScroll("Create Tree Object: ", ref treeScrollVec, treeList);
        SetScroll("Create Grass Object: ", ref grassScrollVec, grassList);
        SetScroll("Create Rock Object: ", ref rockScrollVec, rockList);
        SetScroll("Create Etc Object: ", ref etcScrollVec, etcList);
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
                LayerMask layerMask = 1 << LayerMask.NameToLayer("Surface");
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
                    selectObj.transform.position = hit.point;
                else
                    selectObj.transform.position = ray.GetPoint(10f);

                if (e.keyCode == KeyCode.Q)
                    Debug.Log("Q");
                if (e.type == EventType.KeyDown)
                    Debug.Log("KeyDown");
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
                    Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                    Vector3 pos = selectObj ? selectObj.transform.position : ray.GetPoint(10f);

                    GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    Undo.RegisterCreatedObjectUndo(instance, "Tree");
                    instance.transform.position = pos;
                    instance.transform.SetParent(parent.transform);
                    Selection.activeObject = instance;
                }
            }

            if (selectObj != null)
            {
                DestroyImmediate(selectObj);
                selectObj = null;
            }

            e.Use();
        }
    }

    void SetScroll(string name, ref Vector2 scrollVec, List<GameObject> objectList)
    {
        GUILayout.Label(name, EditorStyles.boldLabel);

        scrollVec = GUILayout.BeginScrollView(scrollVec, true, false, GUILayout.Height(scrollHeight));
        GUILayout.BeginHorizontal(GUILayout.Width(buttonWidth * treeList.Count));

        foreach (var obj in objectList)
        {
            if (obj == null) continue;

            Texture2D preview = AssetPreview.GetAssetPreview(obj) ?? Texture2D.grayTexture;
            Rect rect = GUILayoutUtility.GetRect(buttonWidth, buttonHeight);

            if (Event.current.type == EventType.MouseDrag && rect.Contains(Event.current.mousePosition))
            {
                DragAndDrop.PrepareStartDrag();
                DragAndDrop.objectReferences = new Object[] { obj };
                DragAndDrop.StartDrag(obj.name);
                Event.current.Use();
            }

            GUI.Button(rect, preview);
        }

        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();

        GUILayout.Space(5f);
    }

    void CreateDefaultMap()
    {
        GUILayout.Label("Default Map", EditorStyles.boldLabel);
        if (GUILayout.Button("Create Default Map"))
        {
            GameObject map = (GameObject)PrefabUtility.InstantiatePrefab(defaultMap);
            if (map == null)
                return;

            int layer = LayerMask.NameToLayer("Surface");
            foreach (Transform child in map.transform)
                child.gameObject.layer = layer;

            GameObject parentMap = GameObject.Find("Surface");
            if (!parentMap)
            {
                Debug.LogError("parentMap is null");
                return;
            }
            map.transform.SetParent(parentMap.transform);
            map.transform.position = Vector3.zero;
        }
        GUILayout.Space(5f);
    }

    void Initialize()
    {
        if (parent == null)
            parent = GameObject.FindWithTag("MapObjectParent");

        if (defaultMap == null)
            defaultMap = Resources.Load<GameObject>("DefaultMap/DefaultMap");

        if (treeList.Count == 0)
        {
            GameObject[] objs = Resources.LoadAll<GameObject>("Tree");
            foreach (var obj in objs)
                treeList.Add(obj);
        }

        if (grassList.Count == 0)
        {
            GameObject[] objs = Resources.LoadAll<GameObject>("Grass");
            foreach (var obj in objs)
                grassList.Add(obj);
        }

        if (rockList.Count == 0)
        {
            GameObject[] objs = Resources.LoadAll<GameObject>("Rock");
            foreach (var obj in objs)
                rockList.Add(obj);
        }

        if (etcList.Count == 0)
        {
            GameObject[] objs = Resources.LoadAll<GameObject>("Etc");
            foreach (var obj in objs)
                etcList.Add(obj);
        }
    }
}
