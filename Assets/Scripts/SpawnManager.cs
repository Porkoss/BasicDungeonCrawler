
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyPrefabs;
    public GameObject[] powerUpPrefabs;

    private GameObject player;

    public float MaxSpawnRange=5f;

    public GameObject chestPrefab;

    public int waveCounter=1;

    public bool bChestLooted=false;

    public bool bChestSpawned=false;

    void Start()
    {
        SpawnWave(1);
        player=GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int ennemyCount=GameObject.FindObjectsOfType<EnnemyMovement>().Length;
        if(ennemyCount==0 && !bChestSpawned && !bChestLooted){
            Instantiate(chestPrefab,player.transform.position+new Vector3(3,0,3),chestPrefab.transform.rotation);
            bChestSpawned=true;
        }
        if(bChestLooted)
        {
            SpawnWave(waveCounter);
            bChestLooted=false;
        }

    }

    Vector3 generateRandomPosition(float MaxRange){
        float X=Random.Range(-MaxRange,MaxRange);
        float Z=Random.Range(-MaxRange,MaxRange);
        return new Vector3(X,0,Z);
    }

    void SpawnWave(int number){
        for(int i=0;i<number;i++){
            int RandomEnemy=Random.Range(0,enemyPrefabs.Length);
            Instantiate(enemyPrefabs[RandomEnemy],generateRandomPosition(MaxSpawnRange),enemyPrefabs[RandomEnemy].transform.rotation);
        }
        int RandomPowerUp=Random.Range(0,powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[RandomPowerUp],generateRandomPosition(MaxSpawnRange),powerUpPrefabs[RandomPowerUp].transform.rotation); 
        waveCounter++;

    }



}
