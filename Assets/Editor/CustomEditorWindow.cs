using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class CustomEditorWindow : EditorWindow
{
    static CustomEditorWindow window;

    public static int currentBlock;

    // Block
    cBlockManager blockManager;

    [MenuItem("Window/Editor/DebugWindow")]
    static void SetUp()
    {
        window = GetWindow<CustomEditorWindow>();
    }

    void OnGUI()
    {
        if (!blockManager)
            blockManager = FindObjectOfType<cBlockManager>();

        GUILayout.Label("Block Count", EditorStyles.boldLabel);
        GUILayout.Space(5);

        BlockCountLayout();

        GUILayout.Space(5);
        GUILayout.Label("Block Destroy", EditorStyles.boldLabel);
        GUILayout.Space(5);

        BlockDestroyLayout();

        Repaint();
    }

    void BlockCountLayout()
    {
        EditorGUILayout.LabelField("현재 블록 개수 : ", blockManager.GetallBlocksCount.ToString());
        EditorGUILayout.LabelField("현재 표면 블록 개수 : ", blockManager.GetSurfaceBlockCount.ToString());
        EditorGUILayout.LabelField("현재 층 수 : ", blockManager.GetCurLine.ToString());
    }

    void BlockDestroyLayout()
    {
        if (GUILayout.Button("Destroy_Surface"))
            blockManager.DestroyBlock();
        if (GUILayout.Button("Destroy_Surface x 5"))
            blockManager.DestroyBlockX5();
    }
}
