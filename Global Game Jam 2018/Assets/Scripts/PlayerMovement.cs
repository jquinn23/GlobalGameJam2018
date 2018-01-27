/**
 * @Author Jonathan R. Quinn
 * <summary>
 * This script will allow for basic player platforming movement
 * </summary>
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float speed;
    public Vector3 movementOutput;
    public Vector3 velocityOutput;
    private Rigidbody2D playerRigidBody;
    public float jumpHeight;
    private bool isGrounded;
    public bool c1Grounded;
    public bool c2Grounded;
    private Collider2D playerCollider;

    //These two variables are related to making a dynamic jump
    public float fallMultiplyer;
    public float lowJumpMultiplyer;

    private float distanceToGround;


	// Use this for initialization
	void Start () {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
        
        float moveHorizontal = Input.GetAxis ("Horizontal");
        isGrounded = CheckGrounded();

        //Player shouldn't be able to freely move vertically, so the vertical axis is set to 0
        Vector3 movement = new Vector3(moveHorizontal, 0);
        Vector3 temp = playerRigidBody.velocity;
        temp.x = moveHorizontal * speed;
        playerRigidBody.velocity = temp;

        //Makes velocity and movement monitorable from the editor
        velocityOutput = playerRigidBody.velocity;
        movementOutput = movement;


        //This implements the basic jump
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            playerRigidBody.velocity = Vector2.up * jumpHeight;
        }

        /*
         * This makes the jump more dynamic
         * The first if statement will make the player fall faster over time
         * The second if statement will make it so longer button presses result in higher jumps
         */
        if(playerRigidBody.velocity.y < 0)
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplyer - 1) * Time.deltaTime;
        }
        else if(playerRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplyer - 1) * Time.deltaTime;
        }
	}


    //Performs a Raycast from the corners of the player to see if the player is grounded
    private bool CheckGrounded()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 5;
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        float sizeX = playerCollider.bounds.size.x;
        float sizeY = playerCollider.bounds.size.y;

        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, ((-sizeY / 2) + .01f));
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, (-sizeY / 2 + .01f));

        RaycastHit2D hit = Physics2D.Raycast(corner1, new Vector3(0, -1, 0), 0.1f, layerMask);
        if(hit.collider != null)
        {
            float rayLength = Vector2.Distance(transform.position, hit.point);
            Debug.DrawRay(corner1, new Vector3(0, -1, 0), Color.yellow, rayLength);
        }

        bool grounded1 = Physics2D.Raycast(corner1, new Vector3(0, -1, 0), 0.1f);

        bool grounded2 = Physics2D.Raycast(corner2, new Vector3(0, -1, 0), 0.1f);
        c1Grounded = grounded1;
        c2Grounded = grounded2;

        //If any corner is grounded, the object is grounded
        return (grounded1 || grounded2);
    }

    
}
