using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SolutionChecker : MonoBehaviour {

    public World world;
    public GameObject endGamePanel;
    public static int[] testBlueprint = new int[] 
    {
        1, 1, 1, 1, 1, 1,
        1, 0, 0, 0, 0, 1,
        1, 0, 0, 0, 0, 1,
        1, 0, 0, 0, 0, 1,
        1, 0, 0, 0, 0, 1,
        1, 1, 1, 1, 1, 1
    };
    int testBlueprintWidth = 6;
    int testBlueprintLength = 6;
    int testBlueprintHeight = 1;
    bool testDoesHaveRoof = false;

    public Blueprint[] blueprints;

    int baseOffset = -3;
    int Y_BASE_LEVEL = -8;
    bool thing = true;

	// Use this for initialization
	void Start () {
        endGamePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if (thing)
        {
            Debug.Log(blueprints[0].width + "_" + blueprints[0].length);
            for (int x = 0; x < blueprints[0].width; x++)
            {
                for (int z = 0; z < blueprints[0].length; z++)
                {
                    EditTerrain.SetBlock(new Vector3(x, Y_BASE_LEVEL, z), new BlockBuildSand(), world);
                }
            }
            thing = false;
        }
        */
        /*
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (CheckAgainstAllWinConditions(blueprints[0], -16, 16, -16, 16)) {
                Debug.Log("WIN");
            }
            else
            {
                Debug.Log("NOPE");
            }
        }
        */
	}

    IEnumerator CheckForWin() {
        bool notWinning = true;
        while (notWinning) {
            if (CheckAgainstAllWinConditions(blueprints[0], -16, 16, -16, 16)) {
                notWinning = false;
                StartCoroutine(EndGame());
            }

            yield return new WaitForSeconds(1f);
        }        
    }

    public bool CheckAgainstAllWinConditions(Blueprint bp, int xLimitLow, int xLimitHigh, int zLimitLow, int zLimitHigh)
    {
        WorldPos wpStart;
        WorldPos wpEnd;
        if (CheckAgainstWinCondition(bp.height, bp.width, bp.length, bp.blueprint, bp.hasRoof, out wpStart, out wpEnd, xLimitLow, xLimitHigh, zLimitLow, zLimitHigh))
        {
            foreach (Blueprint b in bp.subBlueprints)
            {
                CheckAgainstAllWinConditions(b, wpStart.x, wpEnd.x, wpStart.z, wpEnd.z);
            }
            return true;
        }
        return false;
    }

    public bool CheckAgainstWinCondition(int blueprintHeight, int blueprintWidth, int blueprintLength, int[] blueprint, bool hasRoof, out WorldPos start, out WorldPos end, int xLimitLow, int xLimitHigh, int zLimitLow, int zLimitHigh)
    {
        for (int x = xLimitLow; x < xLimitHigh + 1; x++)
        {
            for (int y = -16; y < 16; y++)
            {
                for (int z = zLimitLow; z < zLimitHigh + 1; z++)
                {
                    //Debug.Log(x + " " + y + " " + z);
                    /*
                    if (!(world.GetBlock(x, y, z) is BlockSand))
                    {
                        world.SetBlock(x, y, z, new BlockSand());
                    }
                    */
                    if (world.GetBlock(x, y, z) is BlockSand)
                    {
                        //Debug.Log("yep");
                        if (LookForRoomsFixed(new WorldPos(x, y, z), blueprintHeight, blueprintWidth, blueprintLength, blueprint, hasRoof, false, false, out end))
                        {
                            start = new WorldPos(x, y, z);
                            Debug.Log("SOMETHING WAS TRUE 1");
                            return true;
                        }
                        else if (LookForRoomsFixed(new WorldPos(x, y, z), blueprintHeight, blueprintWidth, blueprintLength, blueprint, hasRoof, true, false, out end))
                        {
                            start = new WorldPos(x, y, z);
                            Debug.Log("SOMETHING WAS TRUE 2");
                            return true;
                        }
                        else if (LookForRoomsFixed(new WorldPos(x, y, z), blueprintHeight, blueprintWidth, blueprintLength, blueprint, hasRoof, false, true, out end))
                        {
                            start = new WorldPos(x, y, z);
                            Debug.Log("SOMETHING WAS TRUE 3");
                            return true;
                        }
                        else if (LookForRoomsFixed(new WorldPos(x, y, z), blueprintHeight, blueprintWidth, blueprintLength, blueprint, hasRoof, true, true, out end))
                        {
                            start = new WorldPos(x, y, z);
                            Debug.Log("SOMETHING WAS TRUE 4");
                            return true;
                        }
                    }

                }
            }
        }
        start = new WorldPos(0, 0, 0);
        end = start;
        return false;
    }

    bool LookForRoomsFixed(WorldPos startingBlock, int blueprintHeight, int blueprintWidth, int blueprintLength, int[] blueprint, bool roof, bool flipped, bool backwards, out WorldPos lastBlock) // flipped and backwards not implemented
    {
        lastBlock = new WorldPos(0, 0, 0);
        for (int y = 0; y < blueprintHeight + 1; y++)
        {
            for (int x = 0; x < blueprintWidth; x++)
            {
                for (int z = 0; z < blueprintLength; z++)
                {
                    Block temp = world.GetBlock(startingBlock.x + x, startingBlock.y + y, startingBlock.z + z);
                    //Debug.Log(z + x * blueprintWidth);
                    if (y < blueprintHeight)
                    {
                        switch (blueprint[z + x * blueprintWidth])
                        {
                            case 0:
                                if (!(temp is BlockAir))
                                    return false;
                                break;
                            case 1:
                                if (!(temp is BlockSand))
                                    return false;
                                break;
                            case 2:
                                if (!(temp is BlockLeaves))
                                    return false;
                                break;
                            default:
                                break;
                        }
                    }
                    else // roof
                    {
                        if (roof)
                        {
                            if (!(temp is BlockSand))
                                return false;
                        }                        
                    }
                    lastBlock = new WorldPos(x, y, z);
                }
            }
        }
        return true;
    }
    #region Junk Code
    /*
    bool LookForRooms(WorldPos startingBlock, int blueprintHeight, int blueprintWidth, int blueprintLength, int[] blueprint)
    {
        for (int y = 0; y < blueprintHeight; y++)
        {
            for (int x = 0; x < blueprintWidth; x++)
            {
                for (int z = 0; z < blueprintLength; z++)
                {
                    Block temp = world.GetBlock(startingBlock.x + x, startingBlock.y + y, startingBlock.z + z);
                    Debug.Log(z + x * blueprintWidth);
                    switch (blueprint[z + x * blueprintWidth])
                    {
                        case 0:
                            if (!(temp is BlockAir))
                                return false;
                            break;
                        case 1:
                            if (!(temp is BlockSand))
                                return false;
                            break;
                        case 2:
                            if (!(temp is BlockLeaves))
                                return false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return true;
    }
    
    bool LookForRoomsFlipped(WorldPos startingBlock, int blueprintHeight, int blueprintWidth, int blueprintLength, int[] blueprint)
    {
        for (int y = 0; y < blueprintHeight; y++)
        {
            for (int x = 0; x < blueprintWidth; x++)
            {
                for (int z = 0; z < blueprintLength; z++)
                {
                    Block temp = world.GetBlock(startingBlock.x + x, startingBlock.y + y, startingBlock.z + z);
                    Debug.Log(z + x * blueprintWidth);
                    switch (blueprint[z + x * blueprintWidth])
                    {
                        case 0:
                            if (!(temp is BlockAir))
                                return false;
                            break;
                        case 1:
                            if (!(temp is BlockSand))
                                return false;
                            break;
                        case 2:
                            if (!(temp is BlockLeaves))
                                return false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return true;
    }

    bool LookForRoomsBackwards(WorldPos startingBlock, int blueprintHeight, int blueprintWidth, int blueprintLength, int[] blueprint)
    {
        for (int y = blueprintHeight; y > 0; y--)
        {
            for (int x = blueprintWidth - 1; x >= 0; x--)
            {
                for (int z = blueprintLength - 1; z >= 0; z--)
                {
                    Block temp = world.GetBlock(startingBlock.x + x, startingBlock.y + y, startingBlock.z + z);
                    Debug.Log(z + x * blueprintWidth);
                    switch (blueprint[z + x * blueprintLength])
                    {
                        case 0:
                            if (!(temp is BlockAir))
                                return false;
                            break;
                        case 1:
                            if (!(temp is BlockSand))
                                return false;
                            break;
                        case 2:
                            if (!(temp is BlockLeaves))
                                return false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return true;
    }
    
    bool LookForRoomsBackwardsFlipped(WorldPos startingBlock, int blueprintHeight, int blueprintWidth, int blueprintLength, int[] blueprint)
    {
        for (int y = blueprintHeight; y > 0; y--)
        {
            for (int x = blueprintLength - 1; x >= 0; x--)
            {
                for (int z = blueprintWidth - 1; z >= 0; z--)
                {
                    Block temp = world.GetBlock(startingBlock.x + x, startingBlock.y + y, startingBlock.z + z);
                    Debug.Log(z + x * blueprintWidth);
                    switch (blueprint[z + x * blueprintWidth])
                    {
                        case 0:
                            if (!(temp is BlockAir))
                                return false;
                            break;
                        case 1:
                            if (!(temp is BlockSand))
                                return false;
                            break;
                        case 2:
                            if (!(temp is BlockLeaves))
                                return false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return true;
    }
    */
    #endregion

    IEnumerator EndGame() {

        endGamePanel.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
