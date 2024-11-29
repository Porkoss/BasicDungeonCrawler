using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAnimationHandler : MonoBehaviour
{
    public GameObject launchPoint;
    public GameObject[] arrowPrefab;
    public RangeEnemyAi rangeEnemyAi;

    public Animator bowAnimator;
    // Start is called before the first frame update
    public void LaunchArrow(){

        //Debug.Log("Fire Arrow");
        GameObject arrowInstance = Instantiate(RandomArrow(),launchPoint.transform.position,Quaternion.identity);
        arrowInstance.transform.forward=rangeEnemyAi.towardsPlayer;
        GetComponentInParent<EntitySoundManager>().PlayAttackSound();
    }
    public void ArrowFired(){
        rangeEnemyAi.bCanMove=true;
        bowAnimator.SetBool("Draws",false);
    }
    public void StartDrawing(){
        bowAnimator.SetBool("Draws",true);
    }

    private GameObject RandomArrow(){
        int rand=Random.Range(0,arrowPrefab.Length);
        return arrowPrefab[rand];
    }

}
