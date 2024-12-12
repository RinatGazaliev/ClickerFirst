using UnityEditor;
using UnityEngine;

public class ClearPrefsWindow : EditorWindow
{
    [MenuItem("Tools/Clear PlayerPrefs")] // Добавляем пункт в меню Tools
    public static void ShowWindow()
    {
        // Создаем или отображаем окно
        GetWindow<ClearPrefsWindow>("Clear PlayerPrefs");
    }

    private void OnGUI()
    {
        GUILayout.Label("Clear PlayerPrefs", EditorStyles.boldLabel);

        if (GUILayout.Button("Clear PlayerPrefs"))
        {
            // Очистка PlayerPrefs
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs очищены!");
        }
    }
}