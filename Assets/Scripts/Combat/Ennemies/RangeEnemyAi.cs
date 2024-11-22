using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAi : EnnemyAI
{

    protected override void Attacks(){
        enemyAnimator.SetTrigger("Shoots");
    }
}
