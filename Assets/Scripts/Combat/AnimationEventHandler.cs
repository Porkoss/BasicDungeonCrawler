using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapon weapon;
    public GameObject launchPoint;
    public GameObject arrowPrefab;

    public Bow bow;

    public void DamageFrameON(){
        weapon.DamageFrame=true;
    }
    public void DamageFrameOff(){
        weapon.DamageFrame=false;
        if(weapon.Durability<=0){
            weapon.BreakWeapon();
        }
        weapon.bCanAttack=true;
    }

    public void LaunchArrow(){
        GameObject arrowInstance = Instantiate(arrowPrefab,launchPoint.transform.position,Quaternion.identity);
        arrowInstance.transform.forward=launchPoint.transform.forward;
    }
    public void EndOfShot(){

    }

    public void CanShoot(){
        Debug.Log("CanShoot");
        bow.CanLaunchArrow=true;
        //GetComponentInParent<EntitySoundManager>().PlayAttackSound();
    }

    public void CantShoot(){
        Debug.Log("CantShootAnymore");
        bow.CanLaunchArrow=false;
    }
}
