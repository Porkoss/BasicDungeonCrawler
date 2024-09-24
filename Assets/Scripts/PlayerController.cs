using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed=10f;

     private float gravityValue = -9.81f;

     private float jumpHeight = 1.0f;

    public PlayerControls playerControls;

    private InputAction Move;
    private InputAction Jump;
    private InputAction Attack;
    
    public bool bIsOnGround;
    private Vector2 moveDirection;

    public bool bHasPowerUp=false;

    private CharacterController characterController;

    private Vector3 playerVelocity; 

    private Coroutine activeCoroutine;

    public GameObject followingCamera;

    public Animator animator;
    

    public Weapon weapon;
    private void Awake()
    {
        playerControls=new PlayerControls();
        characterController=GetComponent<CharacterController>();
    }

    public void Launch(){
        Move.Enable();
        Jump.Enable();
        Attack.Enable();
    }
    
    private void  OnEnable()
    {
        Move=playerControls.Player.Move;      
        Jump=playerControls.Player.Jump;        
        Attack=playerControls.Player.Fire;
        Move.Disable();
        Jump.Disable();
        Attack.Disable();
    }
    private void  OnDisable()
    {
        Move.Disable();
        Jump.Disable();
        Attack.Disable();
    }
    
    // Update is called once per frame
    void Update()
    {
        bIsOnGround=characterController.isGrounded;
        Moves();
        Jumps();
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        Attacks();

        followingCamera.transform.position=transform.position + new Vector3(0,8,-5);
    }

    void Jumps(){
        
        if(Jump.IsPressed()&&bIsOnGround){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Debug.Log(playerVelocity.y);
        }
    }
    void Moves(){
        moveDirection=Move.ReadValue<Vector2>();
        //rb.velocity=new Vector3(moveDirection.x*moveSpeed,0,moveDirection.y*moveSpeed);
        Vector3 move =new Vector3(moveDirection.x*moveSpeed*Time.deltaTime,0,moveDirection.y*moveSpeed*Time.deltaTime);
        characterController.Move(move);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        if(move!=new Vector3(0,0,0)){
            animator.SetBool("Moving",true);
        }
        else{
            animator.SetBool("Moving",false);
        }
    }

    void Attacks(){
        if(Attack.IsPressed() && weapon.CanAttack()){
            weapon.GetComponent<Weapon>().Attacks();
            Debug.Log("Attacking");
            animator.SetTrigger("Attacks");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PowerUp")){
            Destroy(other.gameObject);
            bHasPowerUp=true;
            weapon.gameObject.SetActive(true);
            weapon.ResetDurability();
            //activeCoroutine=StartCoroutine(RemovePowerUp());
        }
        else if (other.gameObject.CompareTag("Chest")){
            SpawnManager spawnManager= GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            spawnManager.bChestLooted=true;
            spawnManager.bChestSpawned=false;
            Destroy(other.gameObject);
            if(activeCoroutine!=null){
                StopCoroutine(activeCoroutine);
            }
            
            bHasPowerUp=false;
        }
    }

    IEnumerator RemovePowerUp(){
        yield return new WaitForSeconds(100);
        bHasPowerUp=false;
    }

    public void GameOver(){
        Move.Disable();
        Jump.Disable();
        Attack.Disable();
    }

}
