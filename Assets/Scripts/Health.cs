using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public float health=1;

    public float maxHealth=1;


    public void TakeDamage(float damage)
    {
        
        health-=damage;
        Debug.Log(gameObject.name +" : " +health);
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die(){
        Destroy(gameObject);
    }
}
