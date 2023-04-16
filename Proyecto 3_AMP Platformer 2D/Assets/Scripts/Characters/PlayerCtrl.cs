using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerCtrl : MonoBehaviour
{
    //Player Initial definitions
    private GameObject player, playerSpawn;
    private Rigidbody2D playerRB;
    
    //Player Horizontal Movement parameters
    public float movSpeed,runSpeed;
    private float localScaleX, localScaleY, localScaleZ;
    private bool playerLeft,isRunning, isWalking, canRun;

    //Player Jump parameters
    public float jumpForce;
    private bool isGrounded;

    //Animation parameters
    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerRB.gravityScale = 2f;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerSpawn = GameObject.FindGameObjectWithTag("SpawnStart");

        localScaleX = player.transform.localScale.x;
        localScaleY = player.transform.localScale.y;
        localScaleZ = player.transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal Movement
        MovementX();
        
        //Jump Movement
        Jump();

        playerAnimator.SetBool("AnimationWalk", isWalking);
        playerAnimator.SetBool("AnimationRun", isRunning);
        if (!isGrounded && playerRB.velocity.y > 0)
        { playerAnimator.SetBool("AnimationJumpUp", true); playerAnimator.SetBool("AnimationJumpDown", false); }
        if (!isGrounded && playerRB.velocity.y < 0)
        { playerAnimator.SetBool("AnimationJumpUp", false); playerAnimator.SetBool("AnimationJumpDown", true); }
        if (isGrounded && Mathf.Abs(playerRB.velocity.y) < 0.01)
        { playerAnimator.SetBool("AnimationJumpUp", false); playerAnimator.SetBool("AnimationJumpDown", false); }
    }

    private void Jump()
    {
        //Regular Jump
        if (isGrounded && Input.GetButtonDown("Jump")) //Mathf.Abs(playerRB.velocity.y) < 3f &&  //Prevent from jumping when touching wall, but it also prevents from jumpin on slide
        {
            playerRB.AddForce(new Vector2(playerRB.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Destroyer"))
        {
            player.transform.position = playerSpawn.transform.position;
            // Add falling animation / sound
            // Removes 1 heart in Health script. 
        } //Falling respawning.
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void MovementX()
    {

        //Player horizontal movement - walk
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * movSpeed, playerRB.velocity.y); 
        if (Input.GetAxis("Horizontal") !=0)
        {
            isWalking = true; //isWalking is kept true even when isRunning = true
        }
        else
        {
            isWalking= false;
        }

        //Player horizontal movement - run
        if (Input.GetKeyDown(KeyCode.RightShift)) //Cant trigger run speed if not on the ground
        {
            if (isGrounded)
            {
                canRun = true;
            }
            if (!isGrounded)
            {
                canRun = false;
            }
        }
        if (Input.GetKey(KeyCode.RightShift) && canRun) //If 
        {
            playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, playerRB.velocity.y); 
            if(Input.GetAxis("Horizontal")!=0)
            {
                isRunning = true;
            }
            if (Input.GetAxis("Horizontal") == 0)
            {
                isRunning = false;
            }
        }

        //Running Jump +15%
        if(Input.GetKeyDown(KeyCode.RightShift) && playerRB.velocity.x != 0)
        {
            jumpForce *= 1.15f;
            //isRunning = true; //Not needed.
        }
        if (Input.GetKeyUp(KeyCode.RightShift) && playerRB.velocity.x != 0)
        {
            jumpForce /= 1.15f;
            isRunning = false;
        }
        
        //Player orientation
        if (playerRB.velocity.x < 0)
        {
            playerLeft = true;
        }
        if (playerRB.velocity.x > 0)
        {
            playerLeft = false;
        }
        if (playerLeft)
        {
            player.transform.localScale = new Vector3(-localScaleX, localScaleY, localScaleZ);
        }
        if (!playerLeft)
        {
            player.transform.localScale = new Vector3(localScaleX, localScaleY, localScaleZ);
        }
    }
}
