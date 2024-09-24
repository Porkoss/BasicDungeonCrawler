
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

    public int waveCounter=0;

    public bool bChestLooted=false;

    public bool bChestSpawned=false;

    private bool bIsGamePaused;

    void Start()
    {
        bIsGamePaused=true;
    }
    public void Launch()
    {
        SpawnWave(1);
        player=GameObject.Find("Player");
        bIsGamePaused=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bIsGamePaused)
        {

        
            int ennemyCount=GameObject.FindObjectsOfType<EnnemyAI>().Length;
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
    }

    Vector3 generateRandomPositionOnMesh(float MaxRange){
        float X=Random.Range(-MaxRange,MaxRange);
        float Z=Random.Range(-MaxRange,MaxRange);
        NavMeshHit hit;
        
        
        if( NavMesh.SamplePosition(new Vector3(X,0,Z), out hit, 1.0f, NavMesh.AllAreas))
        {
            return new Vector3(X,0,Z);
        }
        else{
            return generateRandomPositionOnMesh(MaxRange);
        }
    }

    void SpawnWave(int number){
        for(int i=0;i<number;i++){
            int RandomEnemy=Random.Range(0,enemyPrefabs.Length);
            Instantiate(enemyPrefabs[RandomEnemy],generateRandomPositionOnMesh(MaxSpawnRange),enemyPrefabs[RandomEnemy].transform.rotation);
        }
        SpawnWeapon();
        waveCounter++;
    }

    public void SpawnWeapon()
    {
        int RandomPowerUp=Random.Range(0,powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[RandomPowerUp],generateRandomPositionOnMesh(MaxSpawnRange),powerUpPrefabs[RandomPowerUp].transform.rotation);         
    }



}
