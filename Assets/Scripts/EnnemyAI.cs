using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed=1f;
    public float attackRange=1f;
    private GameObject player;
    private CharacterController enemyController;

    public Animator enemyAnimator;
    private Vector3 towardsPlayer;
    public bool bCanMove;

    private  NavMeshAgent agent;

    public float attackDelay=0.5f;

    

        void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        enemyController = GetComponent<CharacterController>();
        player=GameObject.Find("Player");
        bCanMove=true;
    }

    // Update is called once per frame
    void Update()
    {
        towardsPlayer=(player.transform.position-transform.position);
        
        
        if(towardsPlayer.magnitude >= attackRange&&bCanMove){
            //Moves();
            agent.SetDestination(player.transform.position);
        }
        else if(bCanMove){
            agent.SetDestination(transform.position);
            StartCoroutine(DelayAttacks());
            bCanMove=false;
        }
    }
    void Attacks(){
        enemyAnimator.SetTrigger("Attacks");   
    }

    IEnumerator DelayAttacks(){
        yield return new WaitForSeconds(attackDelay);
        Attacks();
    }

}
