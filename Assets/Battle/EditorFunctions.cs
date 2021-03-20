using UnityEngine;
using UnityEditor;

public class EditFunctions : EditorWindow
{
    [MenuItem("Window/Edit Mode Functions")]
    public static void ShowWindow()
    {
        GetWindow<EditFunctions>("Edit Mode Functions");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Run Function"))
        {
            FunctionToRun();
        }
    }

    private void FunctionToRun()
    {
        var grid = FindObjectOfType<CellGrid>();
        grid.Reinit();
    }
}
