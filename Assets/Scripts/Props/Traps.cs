using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    // Start is called before the first frame update

    float tickDelay;
    bool bCanBeDamaged=true;
    private void OnTriggerStay(Collider collider) {
        
        if(collider.CompareTag("Player") ){
            if(bCanBeDamaged){
                Health health=collider.gameObject.GetComponent<Health>();
                health.TakeDamage(1);
                bCanBeDamaged=false;
            }
            PlayerController playerController=collider.gameObject.GetComponent<PlayerController>();
            Vector3 towardsPlayer=(collider.transform.position-transform.position).normalized;
            playerController.GettingPushed(towardsPlayer,10f);

        }
    }

    IEnumerator DamageDelay(){
        yield return new WaitForSeconds(0.5f);
        bCanBeDamaged=true;
    }
}
