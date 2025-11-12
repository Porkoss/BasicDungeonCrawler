using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject JumpVFX;
    
    public bool bIsOnGround=true;
    private Vector2 moveDirection;

    public bool bHasPowerUp=false;

    private CharacterController characterController;

    private Vector3 playerVelocity; 

    private Coroutine activeCoroutine;

    public bool bCanJumpAttack = true;

    public GameObject followingCamera;

    public Animator animator;
    
    private AudioSource swordSound;
    public Weapon weapon;



    public bool bBypassMenu = false;

    #region StartingProcedure
    private void Awake()
    {
        playerControls=new PlayerControls();
        characterController=GetComponent<CharacterController>();
        swordSound=GetComponent<AudioSource>();
        if(followingCamera == null)
        {
            followingCamera = GameObject.Find("Camera");
        }

    }

    private void Start()
    {
        if (bBypassMenu)
        {
            Move.Enable();
            Jump.Enable();
            Attack.Enable();
        }
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
        //Move.Disable();
        //Jump.Disable();
        //Attack.Disable();
    }
    private void  OnDisable()
    {
        Move.Disable();
        Jump.Disable();
        Attack.Disable();
    }

    #endregion StartingProcedure

    // Update is called once per frame
    void Update()
    {
        Moves();
        Jumps();
        bIsOnGround = characterController.isGrounded;
        Attacks();

    }
    #region Movement
    void Jumps(){
        
        if(Jump.IsPressed()&&bIsOnGround&&bCanJumpAttack){
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            GameObject ps = Instantiate(JumpVFX, transform.position, Quaternion.identity);
            ps.GetComponentInChildren<ParticleSystem>().Play();
            //Debug.Log(playerVelocity.y);
        }
        animator.SetBool("Jumping",!bIsOnGround);
    }


    void Moves(){

        
        moveDirection = Move.ReadValue<Vector2>();

        // Get the camera's forward and right directions
        Vector3 cameraForward = followingCamera.transform.forward;
        Vector3 cameraRight = followingCamera.transform.right;

        // Project forward and right onto the XZ plane (ignore Y axis)
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction relative to the camera

        Vector3 move = (cameraRight * moveDirection.x + cameraForward * moveDirection.y) * moveSpeed * Time.deltaTime;
        if(!bIsOnGround || !bCanJumpAttack) move = (cameraForward * moveDirection.y) *moveSpeed * Time.deltaTime;
        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Small negative to keep controller grounded
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        // Move the character   

        Vector3 finalMove = move + Vector3.up * playerVelocity.y*Time.deltaTime;
        characterController.Move(finalMove);



        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        animator.SetBool("Moving", move != Vector3.zero);
    }

    #endregion Movement

    #region Attacks
    void Attacks(){
        if(Attack.IsPressed() && weapon.CanAttack()){
            weapon.GetComponent<Weapon>().Attacks();
            Debug.Log("Attacking");
            animator.SetTrigger("Attacks");
            //swordSound.Play();
        }
    }
    public bool  JumpAttackIfPossible()
    {
        if (!bIsOnGround && bCanJumpAttack)
        {
            bCanJumpAttack = false;
            animator.applyRootMotion = true;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            GameObject ps = Instantiate(JumpVFX, transform.position, Quaternion.identity);
            ps.GetComponentInChildren<ParticleSystem>().Play();
            return true;
        }
        return false;
    }
    public void ResetPositionAfterAnimation()
    {
        characterController.enabled = false;
        Transform model = PlayerInstance.Instance.PlayerFbx.transform;
        Vector3 vector3 = model.position;
        model.localPosition = Vector3.zero;
        model.localRotation = Quaternion.identity;
       
        transform.position = vector3;

        characterController.enabled = true;
        bCanJumpAttack = true;

    }

    #endregion Attacks


    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PowerUp")){///change when otherup comes
            Destroy(other.gameObject);
            
            weapon.gameObject.SetActive(true);
            weapon.ResetDurability();
            //activeCoroutine=StartCoroutine(RemovePowerUp());
        }
        else if (other.gameObject.CompareTag("Chest")){
            GameManager gameManager= GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.ChestLooted();
            Destroy(other.gameObject);
            if(activeCoroutine!=null){
                StopCoroutine(activeCoroutine);
            }
            
            bHasPowerUp=false;
        }
    }

    IEnumerator RemovePowerUp(){////not used yet
        yield return new WaitForSeconds(100);
        bHasPowerUp=false;
    }

    public void GameOver(){
        Move.Disable();
        Jump.Disable();
        Attack.Disable();
    }
    public void GettingPushed(Vector3 direction, float speed)
    {
        characterController.SimpleMove(direction * speed);
    }

}
