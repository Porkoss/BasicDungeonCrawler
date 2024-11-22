using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public float health=1;

    public float maxHealth=1;

    public DropsHandler dropsHandler;
    

    public SpawnManager spawnManager;

    void Start(){
        dropsHandler=GetComponent<DropsHandler>();
    }
    public void TakeDamage(float damage)
    {
        
        health-=damage;
        Debug.Log(gameObject.name +" : " +health);
        if (health <= 0)
        {
            if(!CompareTag("Player")){
            Die();
            }
            else{
                GameOver();
            }
        }
    }
    public void Die(){
        dropsHandler.GenerateDrops();
        Destroy(gameObject);  

    }

    public void GameOver(){
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>().GameOver();
        GameObject.Find("Canvas").GetComponent<UIHandler>().GameOver();

    }

    public void GainMaxHealth(float bonusHealth){
        maxHealth+=bonusHealth;
        health+=bonusHealth;
    }


    public void Heal(float healthRecovered){
        health+=healthRecovered;
        if(health<maxHealth){

            health=maxHealth;
        }
    }

}
