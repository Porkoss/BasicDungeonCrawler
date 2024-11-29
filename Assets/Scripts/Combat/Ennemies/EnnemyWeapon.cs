using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage=1f;

    public bool bCanAttack=false;

    public int Durability;

    public bool DamageFrame;

    private HashSet<GameObject> hitObjects = new HashSet<GameObject>();

    void Start()
    {   
        //Debug.Log("Gaining Weapon");
        bCanAttack=true;
    }

    public void Attacks(){
        if(bCanAttack && gameObject.activeSelf){
            bCanAttack=false;
            //Debug.Log("Weapon Attacks");
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<EntitySoundManager>().PlayBreakSound();
    }


    private void OnTriggerEnter(Collider other) {
        if (DamageFrame && !hitObjects.Contains(other.gameObject)) {
            Debug.Log(other.gameObject.name);
            if (other.CompareTag("Player")) {
                Health enemyHealth = other.GetComponent<Health>();
                if (enemyHealth != null) {
                    enemyHealth.TakeDamage(damage);
                    hitObjects.Add(other.gameObject);
                }
            }
        }
    }

    private void Update() {
    if (!DamageFrame) {
        hitObjects.Clear(); // Reset when the damage frame ends
    }
}
}
