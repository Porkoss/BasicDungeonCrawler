using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Palmmedia.ReportGenerator.Core;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class DungeonBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Room1Opening;
    public GameObject Room2OpeningAcross;
    public GameObject Room2OpeningAngle;
    public GameObject Room3Opening;
    
    public GameObject Room4Opening;

    public GameObject EndRoom;


    public List<GameObject> decorationPrefabs;
    public List<GameObject> trapPrefabs;
    public List<GameObject> bonusPrefabs;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> chestPrefabs;

    public List<GridDataSO> gridDataList;

    public static int width=6; 
    public static int length=6; 

    public bool EndReached;

    int[,] grid = new int[width, length];


    void Start()
    {
        SolutionPathBuilder();
        BonusRoomFiller();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrintGrid(int[,] grid)
    {
        int width = grid.GetLength(0);
        int length = grid.GetLength(1);
        
        for (int x = 0; x < width; x++)
        {
            string row = "Row " + x + ": ";
            for (int y = 0; y < length; y++)
            {
                row += grid[x, y] + " ";
            }
            Debug.Log(row); // Print each row to the console with a row label
        }
    }


    int GetRandomDirection()
    {
        int random = Random.Range(1,5);

        switch(random){
            case 1:
                return 1;
            case 2:
                return 1;
            case 3:
                return 2;
            case 4:
                return 2;
            case 5:
                return 3;
        }
        return 1;
    }
    void SolutionPathBuilder(){
        int widthActual=0;
        int lengthActual=length/2;
        bool bGoingDown=false;
        while(!EndReached){
            //pick random direction
            int direction = GetRandomDirection();
            switch(direction){

                ///
                ///Z is right to left and is Width
                ///X is down  to up and is length // lmao j'ai inversé
                case 1:
                    //right
                    //check if path is taken or end of width length
                    if(lengthActual-1<0){
                        goto case 2;
                    } 
                    else if (grid[widthActual,lengthActual-1]!=0){
                        goto case 2;
                    }
                    else{
                        if(!bGoingDown){//rajoute conditions bord de map (a faire dans un truc qui instantie)
                            CreateRoom2Opening(widthActual,lengthActual,Quaternion.Euler(0, 90, 0));
                        }
                        else{
                            CreateRoom3Opening(widthActual,lengthActual,Quaternion.Euler(0, 180, 0));
                           
                        }
                        grid[widthActual,lengthActual]=1;
                        lengthActual=lengthActual-1;
                        bGoingDown=false;
                        break;
                    }
                case 2:

                    //left
                    if(lengthActual+1>=length){
                        goto case 3;
                    }
                    else if (grid[widthActual,lengthActual+1]!=0){
                        goto case 3;
                    }
                    else{
                        if(!bGoingDown){
                            CreateRoom2Opening(widthActual,lengthActual,Quaternion.Euler(0, 90, 0));
                            
                        }
                        else{
                            CreateRoom3Opening(widthActual,lengthActual,Quaternion.Euler(0, 180, 0));
                        }
                        grid[widthActual,lengthActual]=2;
                        lengthActual=lengthActual+1;
                        bGoingDown=false;
                        break;
                        
                    }
                    
                case 3:
                    //down
                    if(widthActual+1>=width){
                        grid[widthActual,lengthActual]=5;
                        EndReached=true;
                        CreateEndRoom(widthActual,lengthActual,Quaternion.Euler(0, 180, 0));
                        break;
                    }
                    else{
                        if(!bGoingDown){
                            CreateRoom3Opening(widthActual,lengthActual,Quaternion.identity);
                           
                        }
                        else{
                            CreateRoom4Opening(widthActual,lengthActual,Quaternion.identity);
                            
                        }
                        grid[widthActual,lengthActual]=3;
                        widthActual=widthActual+1;
                        bGoingDown=true;
                        break;
                    }
            }
        }
        //complete diagram
        PrintGrid(grid);

        //force room change


    }

    void BonusRoomFiller(){
        for (int actualWidth=0; actualWidth<width;actualWidth++)
        {
            for(int actualLength=0;actualLength<length;actualLength++)
            {
                if(grid[actualWidth,actualLength]==0){
                    if(actualLength+1<width && grid[actualWidth,actualLength+1]!=0 && grid[actualWidth,actualLength+1]!=4)
                    {
                        CreateRoom1Opening(actualWidth,actualLength,Quaternion.Euler(0, -90, 0));                       
                        grid[actualWidth,actualLength]=4;
                    }
                    else if(actualLength-1>0 && grid[actualWidth,actualLength-1]!=0 && grid[actualWidth,actualLength-1]!=4)
                    {
                        CreateRoom1Opening(actualWidth,actualLength,Quaternion.Euler(0, 90, 0));
                        grid[actualWidth,actualLength]=4;
                    }
                }
            }
        }
    }

    void PopulateRooms()
    {
        int randomIndex = Random.Range(0, gridDataList.Count);
        GridDataSO selectedGrid = gridDataList[randomIndex];

        for (int actualWidth = 0; actualWidth < width; actualWidth++)
        {
            for (int actualLength = 0; actualLength < length; actualLength++)
            {
                if (grid[actualWidth, actualLength] != 0)
                {
                    PopulateRoomWithObjects(selectedGrid, actualWidth, actualLength);
                }
            }
        }
    }

    void PopulateRoomWithObjects(GridDataSO gridData, int roomX, int roomY)
    {
        string[] rows = gridData.gridText.Split('\n');
        for (int y = 0; y < rows.Length; y++)
        {
            string row = rows[y];
            for (int x = 0; x < row.Length; x++)
            {
                char cell = row[x];
                Vector3 position = new Vector3(roomX * 10 + x, 0, roomY * 10 + (rows.Length - 1 - y));

                switch (cell)
                {
                    case 'D': // Decoration
                        InstantiateRandomPrefab(decorationPrefabs, position,0.7f);
                        break;
                    case 'T': // Trap
                        InstantiateRandomPrefab(trapPrefabs, position);
                        break;
                    case 'B': // Bonus
                        InstantiateRandomPrefab(bonusPrefabs, position,0.5f);
                        break;
                    case 'E': // Enemy
                        InstantiateRandomPrefab(enemyPrefabs, position);
                        break;
                    case 'C': // Chest
                        InstantiateRandomPrefab(chestPrefabs, position,0);
                        break;
                }
            }
        }
    }


    void InstantiateRandomPrefab(List<GameObject> prefabList, Vector3 position, float skipProbability = 0.2f)
    {
        // Determine if we should skip instantiation based on the probability
        if (Random.value < skipProbability || prefabList.Count == 0)
        {
            return; // Skip instantiation
        }

        // Select a random prefab from the list
        int randomIndex = Random.Range(0, prefabList.Count);
        GameObject selectedPrefab = prefabList[randomIndex];
        Instantiate(selectedPrefab, position, Quaternion.identity);
    }


    void CreateRoom1Opening(int widthActual, int lengthActual, Quaternion quaternion){
        //bonusRoom
        Instantiate(Room1Opening,new Vector3(widthActual*10,0,lengthActual*10),quaternion);
    }
    void CreateRoom2Opening(int widthActual, int lengthActual, Quaternion quaternion){
        Instantiate(Room2OpeningAcross,new Vector3(widthActual*10,0,lengthActual*10),quaternion);
    }
    
    void CreateRoom3Opening(int widthActual, int lengthActual, Quaternion quaternion){
        Instantiate(Room3Opening,new Vector3(widthActual*10,0,lengthActual*10),quaternion);
    }

    void CreateRoom4Opening(int widthActual, int lengthActual, Quaternion quaternion){
        Instantiate(Room4Opening,new Vector3(widthActual*10,0,lengthActual*10),quaternion);
    }

    void CreateEndRoom(int widthActual, int lengthActual, Quaternion quaternion){
        Instantiate(EndRoom,new Vector3(widthActual*10,0,lengthActual*10),quaternion);
    }


}
