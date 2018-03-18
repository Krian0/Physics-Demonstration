using System;
using UnityEngine;


public class PlayerController : Controller
{
    public float moveSpeed = 1f;
    public float jumpSpeed = 6f;
    private Vector3 movement;

	void Start ()
    {
	}

    void Update()
    {
        applyMovement();
    }

    //TODO: Add condition to check we are grounded, add it to jump condition
    protected override void applyMovement()
    {
        float jump = 0;

        movement = Vector3.zero;

        movement += Vector3.forward * Input.GetAxis("Vertical") * moveSpeed;
        movement += Vector3.right * Input.GetAxis("Horizontal") * moveSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
            jump = jumpSpeed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y + jump, movement.z);
    }
}