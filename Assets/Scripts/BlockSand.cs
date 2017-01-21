using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockSand : Block
{
    public BlockSand() : base()
    {

    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 3;
        tile.y = 1;
        return tile;
    }
}