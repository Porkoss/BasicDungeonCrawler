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

    public GameObject player;

    private Health health;

    private PlayerController playerController;

    private GameManager gameManager;

    public bool bgameStarted=false;

    void Start(){
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update(){
        if(bgameStarted){
            textHealth.SetText("Health: "+health.health);
            textLevel.SetText("Level : "+(gameManager.Level));
        }
    }

    public void GameOver(){
        ///gameOverText activation and retry button
        gameOverText.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
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
}
