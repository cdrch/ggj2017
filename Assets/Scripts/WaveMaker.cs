using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaker : MonoBehaviour
{
    public World world;

    public bool active = false;

    int maxDistanceInland = 1;
    int furthestDistance = 60;
    int currentDistance = 60;
    int minCurrentDistance = 31;
    int deltaMinCurrentDistance = 5;
    int waveMinHeight = -9;

    int waveMinZ = -64;
    int waveMaxZ = 64;

    bool incoming;
    public bool emergencyStop = false;

    public float sandMoveChance = .25f;
    public float sandDropChance = .001f;
    public int sandCounter = 0;

    public float waveTotalTime = 5f;
    public float timeUntilStart = 3f;

    List<int> waterColumnsToAvoid;

	// Use this for initialization
	void Start () {
        waterColumnsToAvoid = new List<int>();
        StartCoroutine(StartTimer());
	}
	
	// Update is called once per frame
	void Update () {
		if (active)
        {
            active = false;
            StartCoroutine(WaveIn());
        }
	}

    IEnumerator StartTimer ()
    {
        yield return new WaitForSeconds(timeUntilStart);
        active = true;
    }

    IEnumerator WaveIn ()
    {
        while (!emergencyStop)
        {
            for (int z = waveMinZ; z < waveMaxZ; z++)
            {
                if (incoming && !waterColumnsToAvoid.Contains(z))
                {
                    if (world.GetBlock(currentDistance, waveMinHeight, z) is BlockSand)
                    {
                        if (Random.Range(0f, 1f) < sandMoveChance)
                        {
                            sandCounter++;
                            EditTerrain.SetBlock(new Vector3(currentDistance, waveMinHeight, z), new BlockWater(), world);
                        }
                        else
                        {
                            waterColumnsToAvoid.Add(z);
                        }
                    }
                    else
                    {
                        EditTerrain.SetBlock(new Vector3(currentDistance, waveMinHeight, z), new BlockWater(), world);
                    }
                }
                else // if outgoing
                {
                    if (world.GetBlock(currentDistance, waveMinHeight, z) is BlockWater)
                    {
                        if (sandCounter > 0 && currentDistance > 19 && Random.Range(0f, 1f) < .2)
                        {
                            sandCounter--;
                            EditTerrain.SetBlock(new Vector3(currentDistance, waveMinHeight, z), new BlockSand(), world);
                        }
                        else if (sandCounter > 0 && Random.Range(0f, 1f) < sandDropChance)
                        {
                            sandCounter--;
                            EditTerrain.SetBlock(new Vector3(currentDistance, waveMinHeight, z), new BlockSand(), world);
                        }
                        else
                        {
                            EditTerrain.SetBlock(new Vector3(currentDistance, waveMinHeight, z), new BlockAir(), world);
                        }
                    }
                }                              
            }

            if ((incoming && currentDistance <= maxDistanceInland) || (!incoming && currentDistance >= furthestDistance))
            {
                if (incoming)
                {
                    waterColumnsToAvoid.Clear();
                }
                else
                {
                    maxDistanceInland = Random.Range(1, minCurrentDistance);
                    minCurrentDistance -= deltaMinCurrentDistance;
                    if (minCurrentDistance <= 1)
                    {
                        minCurrentDistance = 1;
                        waveTotalTime -= 1f;
                        if (waveTotalTime < 1f)
                        {
                            waveTotalTime = 1f;
                        }
                    }
                }
                incoming = !incoming;
            }
            else if (incoming)
                currentDistance--;
            else
                currentDistance++;

            yield return new WaitForSeconds(waveTotalTime / (furthestDistance - maxDistanceInland));
        }        
    }
}
