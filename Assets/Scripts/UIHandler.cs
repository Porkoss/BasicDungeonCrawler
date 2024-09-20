using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textHealth;

    public TextMeshProUGUI textWave;

    public GameObject gameOverText;

    public GameObject player;

    private Health health;

    private SpawnManager spawnManager;

    void Start()
    {
        health=player.GetComponent<Health>();
        spawnManager=GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update(){
        textHealth.SetText("Health: "+health.health);
        textWave.SetText("Wave : "+spawnManager.waveCounter);
    }

    public void GameOver(){
        ///gameOverText activation and retry button
        gameOverText.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
