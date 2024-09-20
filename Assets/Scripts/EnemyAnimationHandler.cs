using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyWeapon;
    public float radius =1f;

    public float damage=1f;
    private bool bIsHitting=false;

    private bool canHitPlayer=true;
    void Start()
    {
        enemyWeapon=GameObject.Find("1 handed sword");

    }

    public void StartHitFrame(){
        bIsHitting=true;
        canHitPlayer=true;
    }
    public void StopHitFrame(){
        bIsHitting=false;
    
    }
    public void CheckCollision() {
    // Assuming your player has a Collider with the tag "Player"
    Collider[] hitColliders = Physics.OverlapSphere(enemyWeapon.transform.position,radius);
    foreach (var hitCollider in hitColliders) {
        if (hitCollider.CompareTag("Player")&&canHitPlayer) {
            Health playerHealth= hitCollider.GetComponent<Health>();
            playerHealth.TakeDamage(damage);
            canHitPlayer=false;
        }
    }
}
    void Update(){
        if(bIsHitting&&canHitPlayer){
            CheckCollision();
            
            }
    }

}
