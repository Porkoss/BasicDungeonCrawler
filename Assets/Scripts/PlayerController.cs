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
    public float GravityModifier=1.5f;

    public float JumpForce=500f;

    private Rigidbody rb;
    public PlayerControls playerControls;

    private InputAction Move;
    private InputAction Jump;
    
    public bool bIsOnGround=true;
    private Vector2 moveDirection;

    public bool bHasPowerUp=false;

    private Coroutine activeCoroutine;
    private void Awake()
    {
        playerControls=new PlayerControls();
    }
    
    private void  OnEnable()
    {
        Move=playerControls.Player.Move;
        Move.Enable();
        Jump=playerControls.Player.Jump;
        Jump.Enable();
    }
    private void  OnDisable()
    {
        Move.Disable();
        Jump.Disable();
    }
    
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        Physics.gravity*=GravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        Moves();
        Jumps();
    }

    void Jumps(){
        if(Jump.IsPressed()&&bIsOnGround){
            rb.AddForce(Vector3.up * JumpForce,ForceMode.Impulse);
            bIsOnGround=false;
        }
    }
    void Moves(){
        moveDirection=Move.ReadValue<Vector2>();
        //rb.velocity=new Vector3(moveDirection.x*moveSpeed,0,moveDirection.y*moveSpeed);
        transform.Translate(new Vector3(moveDirection.x*moveSpeed*Time.deltaTime,0,moveDirection.y*moveSpeed*Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground")){
            bIsOnGround=true;
        }
        else if (other.gameObject.CompareTag("Enemy")&& bHasPowerUp){
            Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PowerUp")){
            Destroy(other.gameObject);
            bHasPowerUp=true;
            activeCoroutine=StartCoroutine(RemovePowerUp());
        }
        else if (other.gameObject.CompareTag("Chest")){
            SpawnManager spawnManager= GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            spawnManager.bChestLooted=true;
            spawnManager.bChestSpawned=false;
            Destroy(other.gameObject);
            StopCoroutine(activeCoroutine);
            bHasPowerUp=false;
        }
    }

    IEnumerator RemovePowerUp(){
        yield return new WaitForSeconds(5);
        bHasPowerUp=false;
    }



    
}
