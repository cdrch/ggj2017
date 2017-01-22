using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionChecker : MonoBehaviour {

    public World world;
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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(CheckAgainstWinConditions())
            {
                Debug.Log("WIN");
            }
            else
            {
                Debug.Log("NOPE");
            }
        }
	}

    public bool CheckAgainstWinConditions()
    {
        // Let's assume, as a test case, a house of 3x3x1 interior space with walls wins

        for (int x = -16; x < 16; x++)
        {
            for (int y = -16; y < 16; y++)
            {
                for (int z = -16; z < 16; z++)
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
                        Debug.Log("yep");
                        if (LookForRoomsFixed(new WorldPos(x, y, z), testBlueprintHeight, testBlueprintWidth, testBlueprintLength, testBlueprint, false, false))
                        {
                            Debug.Log("SOMETHING WAS TRUE 1");
                            return true;
                        }
                        else if (LookForRoomsFixed(new WorldPos(x, y, z), testBlueprintHeight, testBlueprintWidth, testBlueprintLength, testBlueprint, true, false))
                        {
                            Debug.Log("SOMETHING WAS TRUE 2");
                            return true;
                        }
                        else if (LookForRoomsFixed(new WorldPos(x, y, z), testBlueprintHeight, testBlueprintWidth, testBlueprintLength, testBlueprint, false, true))
                        {
                            Debug.Log("SOMETHING WAS TRUE 3");
                            return true;
                        }
                        else if (LookForRoomsFixed(new WorldPos(x, y, z), testBlueprintHeight, testBlueprintWidth, testBlueprintLength, testBlueprint, true, true))
                        {
                            Debug.Log("SOMETHING WAS TRUE 4");
                            return true;
                        }
                    }

                }
            }
        }
        return false;
    }

    bool LookForRoomsFixed(WorldPos startingBlock, int blueprintHeight, int blueprintWidth, int blueprintLength, int[] blueprint, bool flipped, bool backwards)
    {
        for (int y = 0; y < blueprintHeight; y++)
        {
            for (int x = 0; x < blueprintWidth; x++)
            {
                for (int z = 0; z < blueprintLength; z++)
                {
                    Block temp = world.GetBlock(startingBlock.x + x, startingBlock.y + y, startingBlock.z + z);
                    //Debug.Log(z + x * blueprintWidth);
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
}
