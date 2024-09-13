using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage=1f;
    public AttackArea attackArea;
    public bool bCanAttack=false;

    private float attackSpeed=1;

    public int Durability;

    

    void Start()
    {
        attackArea.damage=damage;
        attackArea.gameObject.SetActive(false);
        Debug.Log("Gaining Weapon");
        bCanAttack=true;
    }

    public void Attacks(){
        if(bCanAttack && gameObject.activeSelf){
        attackArea.gameObject.SetActive(true);
        bCanAttack=false;
        StartCoroutine(StopAttacking());
        StartCoroutine(RechargeAttack());
        Debug.Log("Weapon Attacks");
        Durability-=1;
        if(Durability<=0){
            StopCoroutine(RechargeAttack());
            bCanAttack=true;
            StartCoroutine(BreakWeapon());
        }
        }
    }


    IEnumerator StopAttacking(){
        yield return new WaitForSeconds(Time.deltaTime*5);
        attackArea.gameObject.SetActive(false);
    }
    IEnumerator RechargeAttack(){
        yield return new WaitForSeconds(attackSpeed);
        bCanAttack=true;
    }

    public void ResetDurability(){
        Durability=3;
    }

    IEnumerator BreakWeapon(){
        yield return new WaitForSeconds(Time.deltaTime*5);
        gameObject.SetActive(false);
    }
}
