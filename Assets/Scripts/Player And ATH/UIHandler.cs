using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textHealth;

    public TextMeshProUGUI textLevel;

    public GameObject gameOverText;

    public GameObject startText;

    public GameObject tutoText;

    public GameObject waitingText;

    public GameObject pauseMenu;

    public GameObject player;

    private Health health;

    private PlayerController playerController;

    private GameManager gameManager;

    private bool bIsGamePaused=false;

    public bool bgameStarted=false;

    void Start(){
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update(){
        if(bgameStarted){
            textHealth.SetText("Health: "+health.health);
            textLevel.SetText("Level : "+(gameManager.Level));
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseMenu();
        }
    }

    public void GameOver(){
        ///gameOverText activation and retry button
        gameOverText.SetActive(true);
    }
    public void Restart()
    {
        gameManager.Restart();
    }

    public void LaunchButton(){
        gameManager.GenerateLevel();
        startText.SetActive(false);
        tutoText.SetActive(false);
        WaitingScreenOn();
    }

    public void WaitingScreenOn(){
        waitingText.SetActive(true);
    }

    public void WaitingScreenOff(){
        
        player=GameObject.FindGameObjectsWithTag("Player")[0];
        health=player.GetComponent<Health>();
        playerController=player.GetComponent<PlayerController>();
        waitingText.SetActive(false);
        textHealth.gameObject.SetActive(true);
        textLevel.gameObject.SetActive(true);
        bgameStarted=true;
        
    }

    public void EnterGame(){
        startText.SetActive(true);
        tutoText.SetActive(true);
    }

    public void PauseMenu(){
        if(bIsGamePaused){
            pauseMenu.SetActive(false);
            Time.timeScale=1f;
            bIsGamePaused=false;
        }
        else{
            pauseMenu.SetActive(true);
            Time.timeScale=0f;
            bIsGamePaused=true;
        }
    }
    public void QuitGame(){
        Application.Quit();
    }

}
