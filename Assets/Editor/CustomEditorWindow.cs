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

        EditorGUILayout.LabelField("���� ��� ���� : ");
        EditorGUILayout.LabelField("�� ������ ��� �� : ");
        EditorGUILayout.LabelField("�� �ı��� ��� �� : ");
        EditorGUILayout.LabelField("FPS : ");
    }

    //if (!window)
    //{
    //    // ���ο� �����츦 ����
    //    window = CreateInstance<CustomEditorWindow>();
    //}

    //// �����츦 ȭ�鿡 ���
    //window.Show();
}
