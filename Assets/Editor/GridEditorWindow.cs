using UnityEngine;
using UnityEditor;

public class GridEditorWindow : EditorWindow
{
    private GridDataSO gridData; // The ScriptableObject being edited
    private int selectedType = 0; // The currently selected type to place
    private string[] types = { "Empty", "Small Decoration","Decoration", "Trap", "Bonus", "Enemy", "Chest", "Player" };
    private char[] typeChars = { '0', 'd','D', 'T', 'B', 'E', 'C', 'P' };

    [MenuItem("Tools/Grid Editor")]
    public static void ShowWindow()
    {
        GetWindow<GridEditorWindow>("Grid Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Layout Editor", EditorStyles.boldLabel);

        // Select the GridDataSO to edit
        gridData = (GridDataSO)EditorGUILayout.ObjectField("Grid Data", gridData, typeof(GridDataSO), false);

        if (gridData == null) return;

        gridData.width = EditorGUILayout.IntField("Width", gridData.width);
        gridData.height = EditorGUILayout.IntField("Height", gridData.height);

        // Create grid array if necessary
        if (string.IsNullOrEmpty(gridData.gridText))
        {
            InitializeGridText();
        }

        // Display dropdown for selecting type
        selectedType = EditorGUILayout.Popup("Selected Type", selectedType, types);

        // Display and edit the grid
        GUILayout.BeginVertical();
        string[] gridLines = gridData.gridText.Split('\n');

        for (int y = 0; y < gridData.height; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < gridData.width; x++)
            {
                char currentChar = gridLines[y][x];
                string buttonText = currentChar.ToString();
                if (GUILayout.Button(buttonText, GUILayout.Width(30), GUILayout.Height(30)))
                {
                    SetGridCell(x, y, typeChars[selectedType]);
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        // Generate random layout button
        if (GUILayout.Button("Generate Random Layout"))
        {
            GenerateRandomLayout();
            SaveGridText();
            EditorUtility.SetDirty(gridData);
        }

        // Save the changes
        if (GUILayout.Button("Save Grid"))
        {
            SaveGridText();
            EditorUtility.SetDirty(gridData);
        }
    }

    // Initialize grid text with empty cells
    private void InitializeGridText()
    {
        gridData.gridText = "";
        for (int y = 0; y < gridData.height; y++)
        {
            gridData.gridText += new string('0', gridData.width) + "\n";
        }
    }

    // Set a cell in the grid
    private void SetGridCell(int x, int y, char newType)
    {
        string[] lines = gridData.gridText.Split('\n');
        char[] lineChars = lines[y].ToCharArray();
        lineChars[x] = newType;
        lines[y] = new string(lineChars);
        gridData.gridText = string.Join("\n", lines);
    }

    // Generate a random layout using RoomGenerator
    private void GenerateRandomLayout()
    {
        // Use RoomGenerator to get a random configuration
        string randomLayout = RoomGenerator.GenerateRandomRoom();

        // Apply the generated layout to the gridData
        gridData.gridText = randomLayout;
        Debug.Log("Random layout generated!");
    }

    // Save the grid text back to the ScriptableObject
    private void SaveGridText()
    {
        Debug.Log("Grid saved!");
    }
}
