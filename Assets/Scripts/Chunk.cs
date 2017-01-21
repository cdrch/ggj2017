﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Chunk : MonoBehaviour {

    public Block[,,] blocks = new Block[chunkSize, chunkSize, chunkSize]; // TO-DO: Should this be made private again and an accessor or something set up?
    public static int chunkSize = 16;
    public bool update = false;
    public bool rendered;

    public World world;
    public WorldPos pos;

    MeshFilter filter;
    MeshCollider col;

	void Start ()
    {
        filter = gameObject.GetComponent<MeshFilter>();
        col = gameObject.GetComponent<MeshCollider>();
    }	

	void Update ()
    {
        if (update)
        {
            update = false;
            UpdateChunk();
        }
    }

    // Returns a block based on x, y, and z coordinates
    public Block GetBlock(int x, int y, int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
            return blocks[x, y, z];
        return world.GetBlock(pos.x + x, pos.y + y, pos.z + z);
    }

    // Updates the chunk based on its contents
    void UpdateChunk()
    {
        rendered = true;
        MeshData meshData = new MeshData();
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    meshData = blocks[x, y, z].BlockData(this, x, y, z, meshData);
                }
            }
        }
        RenderMesh(meshData);
    }

    // Sends the calculated mesh information to the mesh and collision components
    void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();
        filter.mesh.uv = meshData.uv.ToArray();
        filter.mesh.RecalculateNormals();

        col.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();
        mesh.RecalculateNormals();

        col.sharedMesh = mesh;
    }

    public static bool InRange(int index)
    {
        if (index < 0 || index >= chunkSize)
            return false;

        return true;
    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            blocks[x, y, z] = block;
        }
        else
        {
            world.SetBlock(pos.x + x, pos.y + y, pos.z + z, block);
        }
    }

    public void SetBlocksUnmodified()
    {
        foreach (Block block in blocks)
        {
            block.changed = false;
        }
    }
}
