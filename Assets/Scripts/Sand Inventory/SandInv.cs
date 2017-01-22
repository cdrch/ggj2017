using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandInv : MonoBehaviour {

	public int looseSand = 0;
	public int maxSand = 100;
	public int sandPerBlock = 2;
	public float blockSidelen = 1;

	public List<GameObject> blocks = new List<GameObject>();

	public GameObject prefab;

	public List<int> levelSizes = new List<int>();	// Side length of each square level of the sand pile
	private bool updatePile = false;

	// Use this for initialization
	void Start () {
		CreateBlocks();
		UpdateBlocks();
	}
	
	// Update is called once per frame
	void Update () {
		if(updatePile)
		{
			UpdateBlocks();
			updatePile = false;
		}
	}

	public void CreateBlocks()
	{
		int level = 0;
		while(level < levelSizes.Count)
		{
			for(int x=0; x < levelSizes[level]; x++)
			{
				for(int z=0; z < levelSizes[level];z++)
				{
					Vector3 newPos = transform.position + (new Vector3(x + 0.4f,level + 1f,z))*blockSidelen - (new Vector3(levelSizes[level],0,levelSizes[level]))*blockSidelen/2;
					GameObject newSand = Instantiate(prefab, newPos, Quaternion.identity);
					blocks.Add(newSand);
                    newSand.transform.parent = this.transform;
                }
			}
			level++;
		}
	}

	public void UpdateBlocks()
	{
		/*
		int sandLeft = looseSand;
		int level = 0;
		while(sandLeft > 0)
		{
			for(int x=0; x < levelSizes[level];x++)
			{
				for(int z=0; z < levelSizes[level];z++)
				{
					Vector3 newPos = transform.position + (new Vector3(x + 0.4f,level + 1f,z))*blockSidelen - (new Vector3(levelSizes[level],0,levelSizes[level]))*blockSidelen/2;
					GameObject newSand = Instantiate(prefab, newPos, Quaternion.identity);

                    newSand.transform.parent = this.transform;

                    sandLeft -= Mathf.Clamp(sandPerBlock,1,sandLeft);
					if(sandLeft <= 0)
						break;
                }
				if(sandLeft <= 0)
					break;
			}
			level++;
		}
		*/

		for(int i = 0; i < blocks.Count;i++)
		{
			if(i<looseSand/(float)sandPerBlock)
			{
				blocks[i].SetActive(true);
			}
			else
				blocks[i].SetActive(false);
		}

	}

	public int LooseSand
	{
		get{return looseSand;}
		set
		{
			if(value <= maxSand)
			{
				looseSand = value;
				if(looseSand < 0)
					looseSand = 0;
				updatePile = true;
			}
		}
	}

	// Get number of blocks currently in pile, partially filled blocks are counted
	public int SandBlocks
	{
		get{return (int)Mathf.Floor(looseSand/sandPerBlock+1);}
	}

}
