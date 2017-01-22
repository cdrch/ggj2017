using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour {

    // Public Attributes
    public float moveSpeed = 5.0f;
    public float jumpForce = 350.0f;
    public float distanceFromPointToPlace = 1f;
    public LayerMask whatIsGround;
    public bool update = false;
    public World world;
    public WorldPos pos;

    // Private Attributes
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lastDirection = Vector3.forward;
    private Vector3 gravity = Vector3.zero;
    private Rigidbody rigidbod;
    

    /*
     * Store Gravity value
     * Cache Rigidbody component
     */
    private void Awake () {
        gravity = Physics.gravity;
        rigidbod = GetComponent<Rigidbody>();
	}
	
	
    /*
     * Read input for movement and jumping
     */
	private void Update () {
        if (update) {
            // Set moveDirection based on input
            moveDirection = new Vector3(moveSpeed * Input.GetAxis("Horizontal"),
                                        0.0f, 
                                        moveSpeed * Input.GetAxis("Vertical"));

            // Face the direction of movement
            UpdateDirection();

            // Sand Interaction
            if (Input.GetKeyDown(KeyCode.Space)) {
                SandAction();
            }

            // Detect "Jump" input
            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                Jump();
            }
            
        }
    }


    /*
     * Set Rigidbody Velocity
     * Apply Gravity Force
     * Update World Position
     */
    private void FixedUpdate() {
        if (update) {
            // Set Rigidbody Velocity
            rigidbod.velocity = new Vector3(moveDirection.x, rigidbod.velocity.y, moveDirection.z);

            // Apply Gravity Force
            rigidbod.AddForce(Vector3.up * gravity.y);

            // Update World Position
            UpdateWorldPos();
        }
    }


    /*
     * Apply Jump Force if grounded
     */
    private void Jump() {
        if (IsGrounded()) {
            rigidbod.AddForce(Vector3.up * jumpForce);
        }
    }


    /*
     * Return true if a Chunk Collider is detected below the player
     */
    private bool IsGrounded() {
        // Check for ground at a position that is slightly below the player
        Vector3 spherePos = new Vector3(transform.position.x, transform.position.y - 0.04f, transform.position.z);

        // Store the array of Colliders that the sphere overlaps
        Collider[] sphereHits = Physics.OverlapSphere(spherePos, 0.5f, whatIsGround, QueryTriggerInteraction.Ignore);

        // Return true if the Collider array is greater than zero (which means there is ground below us)
        return (sphereHits.Length > 0);
    }


    private void UpdateWorldPos() {
        // Update World Position
        pos.x = (int)Mathf.Round(transform.position.x);
        pos.y = (int)Mathf.Round(transform.position.y);
        pos.z = (int)Mathf.Round(transform.position.z);
    }

    private void UpdateDirection() {
        // Face the direction of movement
        if (moveDirection.sqrMagnitude > 0.1f) {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            targetRotation.x = transform.rotation.x;
            targetRotation.z = transform.rotation.z;
            transform.rotation = targetRotation;
            lastDirection = moveDirection;
        }
    }

    private void SandAction() {
        // Store the RaycastHit
        RaycastHit hit;

        // If a sand block is detected, remove it
        if (Physics.Raycast(transform.position, lastDirection, out hit, distanceFromPointToPlace)) {
            EditTerrain.SetBlock(hit, new BlockAir());
        }
        // If no sand block is detected, place one down
        else {
            EditTerrain.SetBlock(transform.position + Vector3.Normalize(lastDirection) * distanceFromPointToPlace, new BlockSand(), world);
        }
    }
}
