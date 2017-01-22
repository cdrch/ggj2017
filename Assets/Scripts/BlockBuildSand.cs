using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBuildSand : Block {

    public BlockBuildSand() : base()
    {

    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 3;
        tile.y = 0;
        return tile;
    }
}
