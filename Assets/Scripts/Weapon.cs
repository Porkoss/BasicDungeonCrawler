using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage=1f;
    public AttackArea attackArea;
    private bool bCanAttack=false;

    private float attackSpeed=1;

    public int Durability;

    public bool DamageFrame;
    

    

    void Start()
    {
        attackArea.damage=damage;
        Debug.Log("Gaining Weapon");
        bCanAttack=true;
    }
    void Update(){
        if(DamageFrame){
            attackArea.gameObject.SetActive(true);
        }
        else{
            attackArea.gameObject.SetActive(false);
        }
    }

    public void Attacks(){
        if(bCanAttack && gameObject.activeSelf){
        bCanAttack=false;
        StartCoroutine(RechargeAttack());
        Debug.Log("Weapon Attacks");
        Durability-=1;
        if(Durability<=0){
            StopCoroutine(RechargeAttack());
            bCanAttack=false;
            StartCoroutine(BreakWeapon());
        }   
        }
    }
    IEnumerator RechargeAttack(){
        yield return new WaitForSeconds(attackSpeed);
        bCanAttack=true;
    }

    public void ResetDurability(){
        Durability=3;
        bCanAttack=true;
    }

    IEnumerator BreakWeapon(){
        yield return new WaitForSeconds(Time.deltaTime*60);
        attackArea.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public bool CanAttack(){
        return bCanAttack;
    }
}
