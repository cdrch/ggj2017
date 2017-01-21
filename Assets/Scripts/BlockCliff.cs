using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCliff : Block {

    public BlockCliff() : base()
    {

    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 2;
        tile.y = 0;
        return tile;
    }
}
