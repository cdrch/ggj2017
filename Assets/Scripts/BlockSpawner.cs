using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

	public GameObject prefab;
	public float spawnTime = .25f;
	public float clock = 0;

	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		clock += Time.deltaTime;
		if(clock >= spawnTime)
		{
			Spawn();
			clock = 0;
		}
	}

	public void Spawn()
	{
		Instantiate(prefab,transform.position,Quaternion.identity);
	}

}
