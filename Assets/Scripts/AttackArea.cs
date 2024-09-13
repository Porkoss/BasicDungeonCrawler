using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>()!=null&&other.gameObject.CompareTag("Enemy")){
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
