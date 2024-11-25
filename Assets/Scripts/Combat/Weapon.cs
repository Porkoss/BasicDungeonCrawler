using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage=1f;

    public float weaponLength;

    public float radius;
    public bool bCanAttack=false;


    public int Durability;

    public bool DamageFrame;
    

    

    void Start()
    {
        
        //Debug.Log("Gaining Weapon");
        bCanAttack=true;
    }
    void Update(){
        if(DamageFrame){
            CheckCollision();
        }
    }

    public void Attacks(){
        if(bCanAttack && gameObject.activeSelf){
            bCanAttack=false;
            Debug.Log("Weapon Attacks");
            Durability-=1;
        }
    }

    public void ResetDurability(){
        Durability=3;
        bCanAttack=true;
    }
    public bool CanAttack(){
        return bCanAttack;
    }
    public void BreakWeapon(){
        
        
        gameObject.SetActive(false);
    }
    public void CheckCollision() 
    {

        Vector3 capsuleStart = transform.position;  

        Vector3 capsuleEnd = transform.position - transform.up * weaponLength;

        Collider[] hitColliders = Physics.OverlapCapsule(capsuleStart, capsuleEnd, radius); 
        foreach (Collider hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Enemy")) {
                Health enemyHealth = hitCollider.GetComponent<Health>();
                if (enemyHealth != null) {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }
}
