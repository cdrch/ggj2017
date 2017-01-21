using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAir : Block {

	public BlockAir() : base()
    {

    }

    public override MeshData BlockData(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        //meshData.useRenderDataForCol = true; // TO-DO: Check if this is actually needed?
        return meshData;
    }

    public override bool IsSolid(Direction direction)
    {
        return false;
    }
}
