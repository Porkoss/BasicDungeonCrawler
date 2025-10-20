using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage=1f;

    public bool bCanAttack=false;

    public int Durability;

    public bool DamageFrame;

    private HashSet<GameObject> hitObjects = new HashSet<GameObject>();

    protected PlayerController player;

    void Start()
    {
        
        //Debug.Log("Gaining Weapon");
        player=GetComponentInParent<PlayerController>();
    }

    public virtual void Attacks(){
        if(bCanAttack && gameObject.activeSelf){
            bCanAttack=false;
            //Debug.Log("Weapon Attacks");
            Durability-=1;
            player.animator.SetTrigger("Attacks");
            player.entitySoundManager.PlayAttackSoundDelay();
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
        
        bCanAttack=false;
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<EntitySoundManager>().PlayBreakSound();
    }


    private void OnTriggerEnter(Collider other) {
        if (DamageFrame && !hitObjects.Contains(other.gameObject)) {
            //Debug.Log(other.gameObject.name);
            if (other.CompareTag("Enemy")) {
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
