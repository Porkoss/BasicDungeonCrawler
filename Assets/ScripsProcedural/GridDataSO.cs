using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewGridData", menuName = "Grid Data/Grid Layout")]
public class GridDataSO : ScriptableObject
{
    public string gridName;
    public int width;
    public int height;
    [TextArea(10, 10)]
    public string gridText; // Store the grid as a text area for easy editing
}

