using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AnimationEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapon weapon;
    public VisualEffect AttackEffect;
    public VisualEffect AttackEffect2;
    public void DamageFrameON(){
        weapon.DamageFrame=true;
        AttackEffect.Play();
    }
    public void DamageFrameOff(){
        weapon.DamageFrame=false;
    }

    public void DamageFrameOnJump()
    {
        weapon.DamageFrame = true;
        AttackEffect2.Play();
    }
    public void resettingAnimator()
    {
        print("StartReset");
        PlayerInstance.Instance.PlayerAnimator.applyRootMotion = false;
        
        PlayerInstance.Instance.PlayerController.ResetPositionAfterAnimation();
        PlayerInstance.Instance.PlayerController.weapon.bCanAttack = true;
    }
}
