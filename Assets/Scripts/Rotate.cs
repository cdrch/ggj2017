using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float rotationSpeed = 100.0f;
    public Vector3 rotationAxis = new Vector3(0, 1, 0);
	
	void Update () {
        transform.Rotate(new Vector3(   rotationAxis.x * rotationSpeed * Time.deltaTime,
                                        rotationAxis.y * rotationSpeed * Time.deltaTime,
                                        rotationAxis.z * rotationSpeed * Time.deltaTime   ));
	}
}
