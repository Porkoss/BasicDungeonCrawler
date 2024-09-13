using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;
    private GameObject player;
    private CharacterController enemyController;

    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        player=GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 towardsPlayer=(player.transform.position-transform.position).normalized;
               
        enemyController.Move(towardsPlayer*speed*Time.deltaTime);
        
    }
}
