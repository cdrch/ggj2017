using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour {

    // Public Attributes
    public float moveSpeed = 5.0f;
    public float jumpForce = 20.0f;

    // Private Attributes
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 gravity = Vector3.zero;
    private Rigidbody rigidbod;

	
	private void Awake () {
        gravity = Physics.gravity;
        rigidbod = GetComponent<Rigidbody>();
	}
	
	
	private void Update () {
        // Set moveDirection
        moveDirection = new Vector3(moveSpeed * Input.GetAxis("Horizontal"), gravity.y, moveSpeed * Input.GetAxis("Vertical"));

        // Transform the vector3 to local space
        moveDirection = transform.TransformDirection(moveDirection);

        // Detect Jump
        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }


    private void FixedUpdate() {
        // Set velocity
        rigidbod.velocity = new Vector3(moveDirection.x, rigidbod.velocity.y, moveDirection.z);

        // Gravity Force
        rigidbod.AddForce(Vector3.up * moveDirection.y);
    }

    private void Jump() {

    }
}
