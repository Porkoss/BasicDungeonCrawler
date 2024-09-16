using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapon weapon;
    public void DamageFrameON(){
        weapon.DamageFrame=true;
    }
    public void DamageFrameOff(){
        weapon.DamageFrame=false;
    }
}
