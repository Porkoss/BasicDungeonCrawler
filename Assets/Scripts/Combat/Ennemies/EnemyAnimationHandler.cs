using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public EnnemyWeapon enemyWeapon;

    public EnnemyAI ennemyAI;
    public float radius =1.5f;

    public float swordLength=1f;

    public float damage=1f;
    void Start()
    {
        enemyWeapon=GameObject.Find("1 handed sword").GetComponent<EnnemyWeapon>();
    }

    public void StartHitFrame(){
        enemyWeapon.DamageFrame=true;
        GetComponentInParent<EntitySoundManager>().PlayAttackSound();
    }
    public void StopHitFrame(){
        enemyWeapon.DamageFrame=false;
    }
    public void StopAttack(){
        ennemyAI.bCanMove=true;
    }    

}
