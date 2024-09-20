using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed=1f;
    public float attackRange=1f;
    private GameObject player;
    private CharacterController enemyController;

    public Animator enemyAnimator;
    private Vector3 towardsPlayer;
    private bool bCanMove;

    public float attackDelay=0.5f;

    private bool bIsAttacking=false;

        void Start()
    {
        enemyController = GetComponent<CharacterController>();
        player=GameObject.Find("Player");
        bCanMove=true;
    }

    // Update is called once per frame
    void Update()
    {
        towardsPlayer=(player.transform.position-transform.position);
        
        
        if(towardsPlayer.magnitude >= attackRange&&bCanMove){
            Moves();
        }
        else{
            
            StartCoroutine(DelayAttacks());
            bCanMove=false;
        }
    }


    void Moves(){
        
        gameObject.transform.forward = towardsPlayer.normalized;
        enemyController.Move(towardsPlayer.normalized*speed*Time.deltaTime);
    }
    void Attacks(){
        enemyAnimator.SetTrigger("Attacks");   
    }

    IEnumerator DelayAttacks(){
        yield return new WaitForSeconds(attackDelay);
        Attacks();
        bCanMove=true;
        
    }




}
