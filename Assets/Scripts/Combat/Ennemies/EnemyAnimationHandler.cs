using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public EnnemyWeapon enemyWeapon;

    public EnnemyAI ennemyAI;
    void Start()
    {
        enemyWeapon=GetComponentInChildren<EnnemyWeapon>();
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
