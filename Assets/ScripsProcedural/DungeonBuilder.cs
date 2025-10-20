using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Unity.AI.Navigation;

public class DungeonBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Room1Opening;
    public GameObject Room2OpeningAcross;
    public GameObject Room2OpeningAngle;
    public GameObject Room3Opening;
    
    public GameObject Room4Opening;

    public GameObject EndRoom;
    public NavMeshSurface navMeshSurface;
    public List<GameObject> bigDecorationPrefabs;
    public List<GameObject> smallDecorationPrefabs;
    public List<GameObject> trapPrefabs;
    public List<GameObject> bonusPrefabs;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> chestPrefabs;
    public GameObject playerPrefab;
    public List<GridDataSO> gridDataList;
    public GridDataSO StartGrid;
    public GridDataSO EndGrid;

    public int width;
    public int length;

    public bool EndReached;

    int[,] grid ;

    public float roomWidth=20f;

    public float roomLength=20f;

    public GameManager gameManager;

    public void Launch(int NewWidth,int NewLength){
        width = NewWidth;
        length=NewLength;
        grid= new int[width, length];
        StartCoroutine(SequencerLaunch());
    }

    private IEnumerator SequencerLaunch(){
        yield return LaunchBuilder();
        gameManager.GameIsReady();

    }

    private IEnumerator LaunchBuilder()
    {
        SolutionPathBuilder();
        BonusRoomFiller();
        PrintGrid(grid);
        if (navMeshSurface != null)
        {
            Invoke(nameof(BuildNavMesh),0.1f);
        }
        Invoke(nameof(PopulateRooms),0.2f);
        yield return new WaitForSeconds(0.3f);
    }

    private void BuildNavMesh(){
        print("building navmesh");
        navMeshSurface.BuildNavMesh();
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
                ///X is down  to up and is length //  j'ai inversélmao
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
                            CreateTRoom(widthActual,lengthActual,Quaternion.Euler(0, 180, 0));
                           
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
                            CreateTRoom(widthActual,lengthActual,Quaternion.Euler(0, 180, 0));
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
                            CreateTinvertedRoom(widthActual,lengthActual,Quaternion.identity);
                           
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
        grid[0,length/2]=6; // Specifying type for starting room
        //complete diagram
       

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
        for (int actualWidth = 0; actualWidth < width; actualWidth++)
        {
            for (int actualLength = 0; actualLength < length; actualLength++)
            {
                if (grid[actualWidth, actualLength] == 6)//Start room
                {
                    PopulateRoomWithObjects(StartGrid, actualWidth, actualLength);
                }
                else if (grid[actualWidth, actualLength] == 5)//end room
                {
                    PopulateRoomWithObjects(EndGrid, actualWidth, actualLength);
                }
                else if (grid[actualWidth, actualLength] != 0)//add different selected Grid for different room type after ? 
                {
                    int randomIndex = Random.Range(0, gridDataList.Count);
                    GridDataSO selectedGrid = gridDataList[randomIndex];
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
                Vector3 position = new Vector3(roomX * roomLength + x-(roomLength/2)-0.5f, 0, roomY * roomWidth-(roomWidth/2)+(rows.Length - 1.5f - y));

                switch (cell)
                {
                    case 'd':
                        InstantiateRandomPrefab(smallDecorationPrefabs, position,0f);
                        break;
                    case 'D': //Big  Decoration
                        InstantiateRandomPrefab(bigDecorationPrefabs, position,0f);
                        break;
                    case 'T': // Trap
                        InstantiateRandomPrefab(trapPrefabs, position,0);
                        break;
                    case 'B': // Bonus
                        InstantiateRandomPrefab(bonusPrefabs, position,0.5f);
                        break;
                    case 'E': // Enemy
                        InstantiateRandomPrefab(enemyPrefabs, position,0.3f);
                        break;
                    case 'C': // Chest
                        InstantiateRandomPrefab(chestPrefabs, position,0);
                        break;
                    case 'P': //PlayerStart
                        
                        Instantiate(playerPrefab, position, Quaternion.identity);
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
        Instantiate(Room1Opening,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),quaternion);
    }
    void CreateRoom2Opening(int widthActual, int lengthActual, Quaternion quaternion){
        if(length==2){
            Instantiate(Room1Opening,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),quaternion);
        }
        else{
            Instantiate(Room2OpeningAcross,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),quaternion);
        }
        
    }
    
    void CreateRoom3OpeningDown(int widthActual, int lengthActual, Quaternion quaternion){
        if (widthActual == width - 1 ){
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,180,0));
        }
        else if (lengthActual == 0)
        {
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.identity);
            
        }
        else if (lengthActual == length - 1)
        {
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,90,0));
        }
        else{
            Instantiate(Room3Opening,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),quaternion);
        }
        
    }

    void CreateRoom3Opening(int widthActual, int lengthActual, Quaternion quaternion){
        if (widthActual == width - 1 ){
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,180,0));
        }
        else if (lengthActual == 0)
        {
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.identity);
            
        }
        else if (lengthActual == length - 1)
        {
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,90,0));
        }
        else{
            Instantiate(Room3Opening,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),quaternion);
        }
        
    }

    void CreateTRoom(int widthActual, int lengthActual, Quaternion quaternion)
    {
        if(lengthActual == length - 1)//gauche
        {
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,180,0));
        }
        else if (lengthActual == 0){//droite
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,-90,0));
        }
        else{
            Instantiate(Room3Opening,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,180,0));
        }
    }

    void CreateTinvertedRoom(int widthActual, int lengthActual, Quaternion quaternion)
    {
        if(lengthActual == length - 1)//gauche
        {
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,90,0));
        }
        else if (lengthActual == 0){//droite
            Instantiate(Room2OpeningAngle,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.identity);
        }
        else{
            Instantiate(Room3Opening,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.identity);
        }
    }

    void CreateRoom4Opening(int widthActual, int lengthActual, Quaternion quaternion){
        if (lengthActual == 0)//droite
        {
            Instantiate(Room3Opening,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,-90,0));
            
        }
        else if (lengthActual == length - 1)//gauche
        {
            Instantiate(Room3Opening,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),Quaternion.Euler(0,90,0));
        }
        else{
            Instantiate(Room4Opening,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),quaternion);
        }
        
    }

    void CreateEndRoom(int widthActual, int lengthActual, Quaternion quaternion){
        Instantiate(EndRoom,new Vector3(widthActual*roomWidth,0,lengthActual*roomLength),quaternion);
    }


}


//lengthActual == length - 1 gauche

//lenghtActual == 0 droite

//widthActual == width - 1 haut

//withActual == 0 bas