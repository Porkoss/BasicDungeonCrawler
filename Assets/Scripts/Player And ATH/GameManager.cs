using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;

    public DungeonBuilder dungeonBuilder;

    public UIHandler uIHandler;
    
    public CameraController cameraController;

    public GameObject player;

    public int Level = 0;

    // Public property to access Leve
    public float health=0;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevents this object from being destroyed between scene loads

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);  // If instance already exists, destroy this duplicate
        }
    }

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    StartCoroutine(InitializeAfterSceneLoad());
}

private IEnumerator InitializeAfterSceneLoad()
{
    // Wait a frame to ensure all objects are fully initialized
    yield return null; 

    uIHandler = FindObjectOfType<UIHandler>();
    dungeonBuilder = FindObjectOfType<DungeonBuilder>();
    cameraController = FindObjectOfType<CameraController>();
    

    // Debugging to check if objects are found
    if (uIHandler == null)
    {
        Debug.LogError("UIHandler is not found in the scene!");
    }
    if (dungeonBuilder == null)
    {
        Debug.LogError("DungeonBuilder is not found in the scene!");
    }
    if (cameraController == null)
    {
        Debug.LogError("CameraController is not found in the scene!");
    }

    if (Level != 0 && uIHandler != null)
    {
        uIHandler.LaunchButton();  // Call method only if UIHandler is not null
    }
    else{
        uIHandler.EnterGame();
    }
}


        private void OnDisable()
    {
        // Unregister the callback to avoid potential memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void GenerateLevel(){
        dungeonBuilder.Launch(Level+2,Level+2);
    }

    
    public void GameIsReady(){
        player= GameObject.FindGameObjectsWithTag("Player")[0];
        int nb=GameObject.FindGameObjectsWithTag("Player").Length;
        player.GetComponent<PlayerController>().Launch();
        cameraController.CameraLaunch();
        uIHandler.WaitingScreenOff();
    }

    public void ChestLooted(){
        Level=Level+1;
        player= GameObject.FindGameObjectsWithTag("Player")[0];
        health=player.GetComponent<Health>().health;
        SceneManager.LoadScene(0);
    }

    public void Restart(){
        Level=0;
        player= GameObject.FindGameObjectsWithTag("Player")[0];
        health=0;
        SceneManager.LoadScene(0);
    }
}
