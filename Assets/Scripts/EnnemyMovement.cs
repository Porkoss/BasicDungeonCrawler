using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;
    private GameObject player;

    void Start()
    {
        player=GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 towardsPlayer=(player.transform.position-transform.position).normalized;
        transform.Translate(towardsPlayer*speed*Time.deltaTime);
    }
}
