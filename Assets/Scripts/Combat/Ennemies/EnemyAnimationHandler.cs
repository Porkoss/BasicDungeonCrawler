using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyWeapon;

    public EnnemyAI ennemyAI;
    public float radius =1.5f;

    public float swordLength=1f;

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
    public void StopAttack(){
        ennemyAI.bCanMove=true;
    }
    public void CheckCollision() {
        //Debug.Log("Checking collision");
        // Assuming your player has a Collider with the tag "Player"

        // Define the start and end points for the capsule, based on the sword's position and Y direction (up or down).
        Vector3 capsuleStart = enemyWeapon.transform.position;  // Start position (e.g., sword's handle)
        
        // Use the Y-axis (up or down) direction for the capsule's length (along the Y-axis)
        Vector3 capsuleEnd = enemyWeapon.transform.position - enemyWeapon.transform.up * swordLength; // End position (tip of the sword)

        // Perform the capsule overlap check (with the specified radius for the capsule's thickness).
        Collider[] hitColliders = Physics.OverlapCapsule(capsuleStart, capsuleEnd, radius); // radius here is the thickness of the capsule

        // Check if the colliders hit the player and handle damage.
        foreach (Collider hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Player") && canHitPlayer) {
                Health playerHealth = hitCollider.GetComponent<Health>();
                if (playerHealth != null) {
                    playerHealth.TakeDamage(damage);
                    canHitPlayer = false; // Optionally, stop hitting continuously
                }
            }
        }
    }
    

    void Update(){
        if(bIsHitting&&canHitPlayer){
            CheckCollision();
            }
    }

    

    

}
