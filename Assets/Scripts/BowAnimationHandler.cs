using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAnimationHandler : MonoBehaviour
{
    public GameObject launchPoint;
    public GameObject arrowPrefab;
    public RangeEnemyAi rangeEnemyAi;
    // Start is called before the first frame update
    public void LaunchArrow(){
        Debug.Log("Fire Arrow");
        GameObject arrowInstance = Instantiate(arrowPrefab,launchPoint.transform.position,Quaternion.identity);
        arrowInstance.transform.forward=rangeEnemyAi.towardsPlayer;
    }
    public void ArrowFired(){
        rangeEnemyAi.bCanMove=true;
    }

}
