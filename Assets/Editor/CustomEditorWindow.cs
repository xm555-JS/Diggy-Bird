using UnityEngine;
using UnityEditor;

public class CustomEditorWindow : EditorWindow
{
    static CustomEditorWindow window;

    [MenuItem("Window/Editor/DebugWindow")]
    static void SetUp()
    {
        window = GetWindow<CustomEditorWindow>();

        window.minSize = new Vector2(300, 300);
        window.maxSize = new Vector2(1920, 1080);
    }

    void OnGUI()
    {
        GUILayout.Label("Block Monitoring Tool", EditorStyles.boldLabel);
        GUILayout.Space(5);

        EditorGUILayout.LabelField("현재 블록 개수 : ");
        EditorGUILayout.LabelField("총 생성된 블록 수 : ");
        EditorGUILayout.LabelField("총 파괴된 블록 수 : ");
        EditorGUILayout.LabelField("FPS : ");
    }

    //if (!window)
    //{
    //    // 새로운 윈도우를 생성
    //    window = CreateInstance<CustomEditorWindow>();
    //}

    //// 윈도우를 화면에 출력
    //window.Show();
}
