using System;
using UnityEngine;

public class RoomGenerator
{
    private static readonly char[] blockObjects = {'d', 'D', 'T' }; // Big decorations ('D') and traps ('T')
    private static readonly char[] nonBlockObjects = { 'B', 'E'}; // Bonuses, Enemies, Chests
    private const int roomSize = 20;
    private const int bigDecorationBuffer = 1; // Radius buffer around big decorations
    private static System.Random random = new System.Random();

    // Generate a random room layout
    public static string GenerateRandomRoom()
    {
        char[,] grid = InitializeEmptyGrid();

        // Place big decorations (with buffer zone around them)
        int bigDecorationCount = random.Next(1, 3);
        PlaceBigDecorationsWithBuffer(grid, bigDecorationCount);

        // Place other blocking objects (Traps) anywhere on the grid
        int trapCount = random.Next(1, 10);
        PlaceObjects(grid, new char[] { 'T' }, trapCount, allowEdge: false);

        int smallDecorationCount=random.Next(10,20);
        PlaceObjects(grid, new char[] { 'd' }, trapCount, allowEdge: true);
        // Place non-blocking objects (Bonuses, Enemies) anywhere on the grid
        int nonBlockingCount = random.Next(3, 15);
        PlaceObjects(grid, nonBlockObjects, nonBlockingCount, allowEdge: true);

        return ConvertGridToString(grid);
    }

    // Initialize an empty grid
    private static char[,] InitializeEmptyGrid()
    {
        char[,] grid = new char[roomSize, roomSize];
        for (int y = 0; y < roomSize; y++)
        {
            for (int x = 0; x < roomSize; x++)
            {
                grid[y, x] = '0';
            }
        }
        return grid;
    }

    // Place a single object on the grid
    private static void PlaceObject(char[,] grid, char objType)
    {
        int x, y;
        do
        {
            x = random.Next(0, roomSize);
            y = random.Next(0, roomSize);
        } while (grid[y, x] != '0');

        grid[y, x] = objType;
    }

    // Place big decorations with a buffer zone around them
    private static void PlaceBigDecorationsWithBuffer(char[,] grid, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int x, y;
            do
            {
                x = random.Next(1, roomSize - 1);
                y = random.Next(1, roomSize - 1);
            } while (!CanPlaceBigDecoration(grid, x, y));

            grid[y, x] = 'D';
            // Mark buffer area around the big decoration
            MarkBufferZone(grid, x, y, bigDecorationBuffer);
        }
    }

    // Check if we can place a big decoration with a buffer zone
    private static bool CanPlaceBigDecoration(char[,] grid, int x, int y)
    {
        // Ensure the center cell is empty
        if (grid[y, x] != '0') return false;

        // Check surrounding cells in the buffer radius
        for (int dy = -bigDecorationBuffer; dy <= bigDecorationBuffer; dy++)
        {
            for (int dx = -bigDecorationBuffer; dx <= bigDecorationBuffer; dx++)
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && nx < roomSize && ny >= 0 && ny < roomSize)
                {
                    if (grid[ny, nx] != '0') return false;
                }
            }
        }
        return true;
    }

    // Mark buffer area around a big decoration
    private static void MarkBufferZone(char[,] grid, int x, int y, int buffer)
    {
        for (int dy = -buffer; dy <= buffer; dy++)
        {
            for (int dx = -buffer; dx <= buffer; dx++)
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && nx < roomSize && ny >= 0 && ny < roomSize && grid[ny, nx] == '0')
                {
                    grid[ny, nx] = 'X'; // Mark buffer area with 'X' (temporary mark)
                }
            }
        }
    }

    // Place multiple objects with the option to allow edge placement
    private static void PlaceObjects(char[,] grid, char[] objectTypes, int count, bool allowEdge)
    {
        for (int i = 0; i < count; i++)
        {
            char objType = objectTypes[random.Next(objectTypes.Length)];
            int x, y;
            do
            {
                x = random.Next(allowEdge ? 0 : 1, allowEdge ? roomSize : roomSize - 1);
                y = random.Next(allowEdge ? 0 : 1, allowEdge ? roomSize : roomSize - 1);
            } while (grid[y, x] != '0' && grid[y, x] != 'X'); // Skip cells occupied by buffer or existing objects

            grid[y, x] = objType;
        }
    }

    // Convert grid to string format
    private static string ConvertGridToString(char[,] grid)
    {
        string result = "";
        for (int y = 0; y < roomSize; y++)
        {
            for (int x = 0; x < roomSize; x++)
            {
                char cell = grid[y, x] == 'X' ? '0' : grid[y, x]; // Convert buffer marks back to empty cells
                result += cell;
            }
            result += "\n";
        }
        return result;
    }
}
