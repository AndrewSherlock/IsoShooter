using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player{

    Rigidbody rb;
    Vector3 currentPosition;
   
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        GetPlayerMovement();
        RotatePlayer();
    }

    void FixedUpdate()
    {
        Vector3 timedMovement = transform.TransformDirection(currentPosition) * Time.fixedDeltaTime;
        Vector3 totalMove = rb.position + timedMovement;
        rb.MovePosition(totalMove);
    }

    void GetPlayerMovement()
    {
        float xDir = -ZeroOutAxis(Input.GetAxisRaw("Horizontal")); // inverts the X axis to correct our button input
        float zDir = ZeroOutAxis(Input.GetAxisRaw("Vertical")); // inverts the Y axis to correct our button input
        
        Vector3 moveDirection = new Vector3(zDir,0, xDir).normalized;
        Vector3 movementSpeed = moveDirection * (Input.GetButton("Run") ? runSpeed : speed);
        currentPosition = movementSpeed;
    }

    void RotatePlayer()
    {
        float zDir = ZeroOutAxis(Input.GetAxisRaw("RotateAxisVertical"));

        Quaternion angles = Quaternion.Euler(Vector3.up * zDir * speed);
        transform.rotation = transform.rotation * angles;
    }

    float ZeroOutAxis(float input)
    {
        if (input < 0.2f && input > -0.2f)
        {
            input = 0;
        }
        else if (input > -0.2f && input < 0.2f)
        {
            input = 0;
        }

        return input;
    }
}
