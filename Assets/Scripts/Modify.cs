using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modify : MonoBehaviour
{
    public World world;
    Vector2 rot;
    public float distanceFromPointToPlace = 1f;

    void Update()
    {
        /*
        RaycastHit h;
        if(Physics.Raycast(transform.position, transform.forward, out h, 100f))
        {
            Debug.Log(EditTerrain.GetBlock(h) + " " + h.transform.position);
        }
        */
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distanceFromPointToPlace))
            {
                EditTerrain.SetBlock(hit, new BlockAir());
            }
            else
            {
                
                WorldPos blockPosToEdit = EditTerrain.GetBlockPos(transform.position + Vector3.forward); // Get the block one unit away from the transform of whatever this script is attached to
                EditTerrain.SetBlock(transform.position + Vector3.forward * distanceFromPointToPlace, new BlockSand(), world);
                
            }
        }

        rot = new Vector2(
            rot.x + Input.GetAxis("Mouse X") * 3,
            rot.y + Input.GetAxis("Mouse Y") * 3
            );

        transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

        transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
        transform.position += transform.right * 3 * Input.GetAxis("Horizontal");
    }
}
