using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public float health=1;

    public float maxHealth=1;
    

    public SpawnManager spawnManager;

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
            Destroy(gameObject);  
    }

    public void GameOver(){
        GameObject.Find("Player").GetComponent<PlayerController>().GameOver();
        GameObject.Find("Canvas").GetComponent<UIHandler>().GameOver();

    }

    public void GainMaxHealth(int bonusHealth){
        maxHealth+=bonusHealth;
        health+=bonusHealth;
    }

}
