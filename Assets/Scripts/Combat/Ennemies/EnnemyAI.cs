using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed=1f;
    public float attackRange=1f;
    public float rotationSpeed = 5f;
    private GameObject player;
    

    public Animator enemyAnimator;
    public Vector3 towardsPlayer;

    public float sightRange = 15f;

    //public float fieldOfView = 45f;


    public bool bCanMove;

    private  NavMeshAgent agent;

    public float attackDelay=1f;

    public bool bIsActive=false;

    

    void Start()
    {
        agent=GetComponent<NavMeshAgent>();

        player=GameObject.FindGameObjectsWithTag("Player")[0];
        bCanMove=true;
    }

    // Update is called once per frame
    void Update()
    {
        towardsPlayer=(player.transform.position-transform.position);
        if(IsSeeingEnemy()){
            if(towardsPlayer.magnitude >= attackRange&&bCanMove){
            //Moves();
                if(!isFacingTarget(towardsPlayer)){
                    RotateTowardsPlayer(towardsPlayer);
                }
                else{
                    agent.SetDestination(player.transform.position);
                    enemyAnimator.SetBool("IsMoving",true);
                }
            }
            else if(bCanMove){
                transform.forward=towardsPlayer.normalized;
                agent.SetDestination(transform.position);
                StartCoroutine(DelayAttacks()); 
                bCanMove=false;
                enemyAnimator.SetBool("IsMoving",false);
            }
        }
    }

    private void RotateTowardsPlayer(Vector3 direction)
    {
        // Set rotation
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private bool isFacingTarget(Vector3 direction)
    {
        // Check if the enemy is almost facing the target (angle threshold)
        float angle = Vector3.Angle(transform.forward, direction);
        return angle < 15f; // Adjust the threshold as needed
    }
    bool IsSeeingEnemy()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        // Check if player is within the field of view and range
        // check if angle < fieldOfView / 2
        if ( Vector3.Distance(transform.position, player.transform.position) < sightRange)
        {
            Debug.Log("Trying to look for player");
            // Check if no obstacles are in the way
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightRange))
            {
                if(hit.collider.gameObject.CompareTag("Player")){
                    Debug.Log("Seeing Player");
                    return true;
                }
                
            }
        }
        return false;

    }
    protected virtual void Attacks(){
        enemyAnimator.SetTrigger("Attacks");   
    }

    IEnumerator DelayAttacks(){
        yield return new WaitForSeconds(attackDelay);
        Attacks();
    }

}
