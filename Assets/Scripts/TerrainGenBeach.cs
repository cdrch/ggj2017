﻿using UnityEngine;
using System.Collections;
using SimplexNoise;

public class TerrainGenBeach {

    // TO-DO: Expose all of this externally? Perhaps with sliders?
    /*
    float stoneBaseHeight = -24;
    float stoneBaseNoise = 0.05f;
    float stoneBaseNoiseHeight = 4;
    float stoneMountainHeight = 48;
    float stoneMountainFrequency = 0.008f;
    float stoneMinHeight = -12;
    float dirtBaseHeight = 1;
    float dirtNoise = 0.04f;
    float dirtNoiseHeight = 3;
    float caveFrequency = 0.025f;
    int caveSize = 7;
    float treeFrequency = 0.2f;
    int treeDensity = 3;*/

    float sandBaseHeight = -24;
    float sandBaseNoise = 0.05f;
    float sandBaseNoiseHeight = 4;
    float sandDuneHeight = 48;
    float sandDuneFrequency = 0.008f;
    float sandMinHeight = -12;

    public Chunk ChunkGen(Chunk chunk)
    {        
        for (int x = chunk.pos.x - 3; x < chunk.pos.x + Chunk.chunkSize + 3; x++)
        {
            for (int z = chunk.pos.z - 3; z < chunk.pos.z + Chunk.chunkSize + 3; z++)
            {
                chunk = ChunkColumnGen(chunk, x, z);
            }
        }
        return chunk;
    }

    public static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }

    public Chunk ChunkColumnGen(Chunk chunk, int x, int z)
    {
        int sandHeight = Mathf.FloorToInt(sandBaseHeight);
        sandHeight += GetNoise(x, 0, z, sandDuneFrequency, Mathf.FloorToInt(sandDuneHeight));
        if (sandHeight < sandMinHeight)
            sandHeight = Mathf.FloorToInt(sandMinHeight);
        sandHeight += GetNoise(x, 0, z, sandBaseNoise, Mathf.FloorToInt(sandBaseNoiseHeight));
        for (int y = chunk.pos.y - 8; y < chunk.pos.y + Chunk.chunkSize; y++)
        {
            if (y <= sandHeight)
            {
                SetBlock(x, y, z, new BlockSand(), chunk);
                // Place rocks or other objects here
                /*
                if (y == dirtHeight && GetNoise(x, 0, z, treeFrequency, 100) < treeDensity)
                    CreateTree(x, y + 1, z, chunk);
                */
            }
            else
            {
                SetBlock(x, y, z, new BlockAir(), chunk);
            }
        }
        return chunk;
    }

    public static void SetBlock(int x, int y, int z, Block block, Chunk chunk, bool replaceBlocks = false)
    {
        x -= chunk.pos.x;
        y -= chunk.pos.y;
        z -= chunk.pos.z;
        if (Chunk.InRange(x) && Chunk.InRange(y) && Chunk.InRange(z))
        {
            if (replaceBlocks || chunk.blocks[x, y, z] == null)
                chunk.SetBlock(x, y, z, block);
        }
    }

    void CreateTree(int x, int y, int z, Chunk chunk)
    {
        //create leaves
        for (int xi = -2; xi <= 2; xi++)
        {
            for (int yi = 4; yi <= 8; yi++)
            {
                for (int zi = -2; zi <= 2; zi++)
                {
                    SetBlock(x + xi, y + yi, z + zi, new BlockLeaves(), chunk, true);
                }
            }
        }
        //create trunk
        for (int yt = 0; yt < 6; yt++)
        {
            SetBlock(x, y + yt, z, new BlockWood(), chunk, true);
        }
    }
}
