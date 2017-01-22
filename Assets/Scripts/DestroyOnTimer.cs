using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour {

    public float timeToDestroy = 15f;
    float timeLeft;

	// Use this for initialization
	void Start () {
        timeLeft = timeToDestroy;
	}

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
