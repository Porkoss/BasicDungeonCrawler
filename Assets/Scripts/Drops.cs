using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    // Start is called before the first frame update
    public float HealthGained=1;
    public float Chance=0.25f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.GetComponent<Health>().GainMaxHealth(HealthGained);
            Destroy(gameObject);
        }
    }    
}
