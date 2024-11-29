using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidBody;
    public float arrowLifeSpan=5f;

    public float Damage=1f;
    
    public float arrowForce=10f;
    void Start()
    {
        rigidBody =  GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.forward*arrowForce, ForceMode.Impulse);
        StartCoroutine(ShortLife());
    }

    // Update is called once per frame

    IEnumerator ShortLife(){
        yield return new WaitForSeconds(arrowLifeSpan);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if(collider.CompareTag("Player")){
            Health health=collider.gameObject.GetComponent<Health>();
            health.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
