using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public bool IsAiming=false;
    public bool CanLaunchArrow=false;
    // Start is called before the first frame update
    public override void Attacks()
    {
        Durability-=1;
        player.animator.SetTrigger("Shoots");
    }

    void Update()
    {
        player.animator.SetBool("Aiming",IsAiming);
    }
    public void TryToLaunchArrow(){
        Debug.Log("TryingToLaunch");
        if(CanLaunchArrow){
            player.animator.SetTrigger("Shoots");
        }
    }

}
