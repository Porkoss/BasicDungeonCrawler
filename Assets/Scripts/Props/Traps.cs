using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    // Start is called before the first frame update

    public float tickDelay=0.5f;
    public float nextDamageTime=0f;
    private void OnTriggerStay(Collider collider) {
        
        if(collider.CompareTag("Player") && Time.time>=nextDamageTime){

            Health health=collider.gameObject.GetComponent<Health>();
            health.TakeDamage(1);
            nextDamageTime=Time.time + tickDelay;
            //PlayerController playerController=collider.gameObject.GetComponent<PlayerController>();
            //Vector3 towardsPlayer=(collider.transform.position-transform.position).normalized;
            //playerController.GettingPushed(towardsPlayer,10f);
        }
    }
}
