using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : Drops
{
    public float HealthGained=1;
    // Start is called before the first frame update    public float HealthGained=1;
    public override void ActivateItem()
    {
        base.ActivateItem();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Health health = player.GetComponent<Health>();
        health.Heal(HealthGained);
    }






}
