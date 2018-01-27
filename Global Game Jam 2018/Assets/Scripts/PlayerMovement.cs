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


	// Use this for initialization
	void Start () {
        playerRigidBody = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        
        //Player shouldn't be able to freely move vertically, so the vertical axis is set to 0
        Vector3 movement = new Vector3(moveHorizontal, 0);
        Vector3 temp = playerRigidBody.velocity;
        temp.x = moveHorizontal * speed;
        playerRigidBody.velocity = temp;
        velocityOutput = playerRigidBody.velocity;
        //playerRigidBody.MovePosition(transform.position +  * Time.deltaTime);
        movementOutput = movement;
	}
}
