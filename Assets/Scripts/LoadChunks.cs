using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadChunks : MonoBehaviour
{
    #region Variables
    public World world;
    public GameObject titleScreen;

    List<WorldPos> updateList = new List<WorldPos>();
    List<WorldPos> buildList = new List<WorldPos>();

    // Equals 'true' when no chunks were added
    private bool doneAddingChunks = false;  

    // Equals 'true' once the player is created
    private bool createdPlayer = false;

    int timer = 0;

    static WorldPos[] chunkPositions = {   new WorldPos( 0, 0,  0), new WorldPos(-1, 0,  0), new WorldPos( 0, 0, -1), new WorldPos( 0, 0,  1), new WorldPos( 1, 0,  0),
                             new WorldPos(-1, 0, -1), new WorldPos(-1, 0,  1), new WorldPos( 1, 0, -1), new WorldPos( 1, 0,  1), new WorldPos(-2, 0,  0),
                             new WorldPos( 0, 0, -2), new WorldPos( 0, 0,  2), new WorldPos( 2, 0,  0), new WorldPos(-2, 0, -1), new WorldPos(-2, 0,  1),
                             new WorldPos(-1, 0, -2), new WorldPos(-1, 0,  2), new WorldPos( 1, 0, -2), new WorldPos( 1, 0,  2), new WorldPos( 2, 0, -1),
                             new WorldPos( 2, 0,  1), new WorldPos(-2, 0, -2), new WorldPos(-2, 0,  2), new WorldPos( 2, 0, -2), new WorldPos( 2, 0,  2),
                             new WorldPos(-3, 0,  0), new WorldPos( 0, 0, -3), new WorldPos( 0, 0,  3), new WorldPos( 3, 0,  0), new WorldPos(-3, 0, -1),
                             new WorldPos(-3, 0,  1), new WorldPos(-1, 0, -3), new WorldPos(-1, 0,  3), new WorldPos( 1, 0, -3), new WorldPos( 1, 0,  3),
                             new WorldPos( 3, 0, -1), new WorldPos( 3, 0,  1), new WorldPos(-3, 0, -2), new WorldPos(-3, 0,  2), new WorldPos(-2, 0, -3),
                             new WorldPos(-2, 0,  3), new WorldPos( 2, 0, -3), new WorldPos( 2, 0,  3), new WorldPos( 3, 0, -2), new WorldPos( 3, 0,  2),
                             new WorldPos(-4, 0,  0), new WorldPos( 0, 0, -4), new WorldPos( 0, 0,  4), new WorldPos( 4, 0,  0), new WorldPos(-4, 0, -1),
                             new WorldPos(-4, 0,  1), new WorldPos(-1, 0, -4), new WorldPos(-1, 0,  4), new WorldPos( 1, 0, -4), new WorldPos( 1, 0,  4),
                             new WorldPos( 4, 0, -1), new WorldPos( 4, 0,  1), new WorldPos(-3, 0, -3), new WorldPos(-3, 0,  3), new WorldPos( 3, 0, -3),
                             new WorldPos( 3, 0,  3), new WorldPos(-4, 0, -2), new WorldPos(-4, 0,  2), new WorldPos(-2, 0, -4), new WorldPos(-2, 0,  4),
                             new WorldPos( 2, 0, -4), new WorldPos( 2, 0,  4), new WorldPos( 4, 0, -2), new WorldPos( 4, 0,  2), new WorldPos(-5, 0,  0),
                             new WorldPos(-4, 0, -3), new WorldPos(-4, 0,  3), new WorldPos(-3, 0, -4), new WorldPos(-3, 0,  4), new WorldPos( 0, 0, -5),
                             new WorldPos( 0, 0,  5), new WorldPos( 3, 0, -4), new WorldPos( 3, 0,  4), new WorldPos( 4, 0, -3), new WorldPos( 4, 0,  3),
                             new WorldPos( 5, 0,  0), new WorldPos(-5, 0, -1), new WorldPos(-5, 0,  1), new WorldPos(-1, 0, -5), new WorldPos(-1, 0,  5),
                             new WorldPos( 1, 0, -5), new WorldPos( 1, 0,  5), new WorldPos( 5, 0, -1), new WorldPos( 5, 0,  1), new WorldPos(-5, 0, -2),
                             new WorldPos(-5, 0,  2), new WorldPos(-2, 0, -5), new WorldPos(-2, 0,  5), new WorldPos( 2, 0, -5), new WorldPos( 2, 0,  5),
                             new WorldPos( 5, 0, -2), new WorldPos( 5, 0,  2), new WorldPos(-4, 0, -4), new WorldPos(-4, 0,  4), new WorldPos( 4, 0, -4),
                             new WorldPos( 4, 0,  4), new WorldPos(-5, 0, -3), new WorldPos(-5, 0,  3), new WorldPos(-3, 0, -5), new WorldPos(-3, 0,  5),
                             new WorldPos( 3, 0, -5), new WorldPos( 3, 0,  5), new WorldPos( 5, 0, -3), new WorldPos( 5, 0,  3), new WorldPos(-6, 0,  0),
                             new WorldPos( 0, 0, -6), new WorldPos( 0, 0,  6), new WorldPos( 6, 0,  0), new WorldPos(-6, 0, -1), new WorldPos(-6, 0,  1),
                             new WorldPos(-1, 0, -6), new WorldPos(-1, 0,  6), new WorldPos( 1, 0, -6), new WorldPos( 1, 0,  6), new WorldPos( 6, 0, -1),
                             new WorldPos( 6, 0,  1), new WorldPos(-6, 0, -2), new WorldPos(-6, 0,  2), new WorldPos(-2, 0, -6), new WorldPos(-2, 0,  6),
                             new WorldPos( 2, 0, -6), new WorldPos( 2, 0,  6), new WorldPos( 6, 0, -2), new WorldPos( 6, 0,  2), new WorldPos(-5, 0, -4),
                             new WorldPos(-5, 0,  4), new WorldPos(-4, 0, -5), new WorldPos(-4, 0,  5), new WorldPos( 4, 0, -5), new WorldPos( 4, 0,  5),
                             new WorldPos( 5, 0, -4), new WorldPos( 5, 0,  4), new WorldPos(-6, 0, -3), new WorldPos(-6, 0,  3), new WorldPos(-3, 0, -6),
                             new WorldPos(-3, 0,  6), new WorldPos( 3, 0, -6), new WorldPos( 3, 0,  6), new WorldPos( 6, 0, -3), new WorldPos( 6, 0,  3),
                             new WorldPos(-7, 0,  0), new WorldPos( 0, 0, -7), new WorldPos( 0, 0,  7), new WorldPos( 7, 0,  0), new WorldPos(-7, 0, -1),
                             new WorldPos(-7, 0,  1), new WorldPos(-5, 0, -5), new WorldPos(-5, 0,  5), new WorldPos(-1, 0, -7), new WorldPos(-1, 0,  7),
                             new WorldPos( 1, 0, -7), new WorldPos( 1, 0,  7), new WorldPos( 5, 0, -5), new WorldPos( 5, 0,  5), new WorldPos( 7, 0, -1),
                             new WorldPos( 7, 0,  1), new WorldPos(-6, 0, -4), new WorldPos(-6, 0,  4), new WorldPos(-4, 0, -6), new WorldPos(-4, 0,  6),
                             new WorldPos( 4, 0, -6), new WorldPos( 4, 0,  6), new WorldPos( 6, 0, -4), new WorldPos( 6, 0,  4), new WorldPos(-7, 0, -2),
                             new WorldPos(-7, 0,  2), new WorldPos(-2, 0, -7), new WorldPos(-2, 0,  7), new WorldPos( 2, 0, -7), new WorldPos( 2, 0,  7),
                             new WorldPos( 7, 0, -2), new WorldPos( 7, 0,  2), new WorldPos(-7, 0, -3), new WorldPos(-7, 0,  3), new WorldPos(-3, 0, -7),
                             new WorldPos(-3, 0,  7), new WorldPos( 3, 0, -7), new WorldPos( 3, 0,  7), new WorldPos( 7, 0, -3), new WorldPos( 7, 0,  3),
                             new WorldPos(-6, 0, -5), new WorldPos(-6, 0,  5), new WorldPos(-5, 0, -6), new WorldPos(-5, 0,  6), new WorldPos( 5, 0, -6),
                             new WorldPos( 5, 0,  6), new WorldPos( 6, 0, -5), new WorldPos( 6, 0,  5) };
    #endregion

    private void Start() {
        StartCoroutine(ShowTitleScreen());
    }

    void Update()
    {
        if (DeleteChunks()) // If a delete happened, return early
            return;
        FindChunksToLoad();
        LoadAndRenderChunks();
    }

    void FindChunksToLoad()
    {
        // Get the position of this gameobject to generate around
        WorldPos playerPos = new WorldPos(
            Mathf.FloorToInt(transform.position.x / Chunk.chunkSize) * Chunk.chunkSize,
            Mathf.FloorToInt(transform.position.y / Chunk.chunkSize) * Chunk.chunkSize,
            Mathf.FloorToInt(transform.position.z / Chunk.chunkSize) * Chunk.chunkSize
            );
        // If there are not currently any chunks to generate
        if (updateList.Count == 0)
        {
            // Used to track whether a chunk was added or not
            bool addedNewChunk = false;

            // Cycle through the array of positions
            for (int i = 0; i < chunkPositions.Length; i++)
            {
                // Translate the player position and array position into chunk position
                WorldPos newChunkPos = new WorldPos(
                    chunkPositions[i].x * Chunk.chunkSize + playerPos.x,
                    0,
                    chunkPositions[i].z * Chunk.chunkSize + playerPos.z
                    );
                // Get the chunk in the defined position
                Chunk newChunk = world.GetChunk(
                    newChunkPos.x, newChunkPos.y, newChunkPos.z);
                // If the chunk already exists and it's already rendered or in queue to be rendered continue
                if (newChunk != null
                    && (newChunk.rendered || updateList.Contains(newChunkPos)))
                    continue;

                // Set to true, because we're adding a new chunk
                addedNewChunk = true;


                // Load a column of chunks in this position
                for (int y = -4; y < 4; y++)
                {
                    for (int x = newChunkPos.x - Chunk.chunkSize; x <= newChunkPos.x + Chunk.chunkSize; x += Chunk.chunkSize)
                    {
                        for (int z = newChunkPos.z - Chunk.chunkSize; z <= newChunkPos.z + Chunk.chunkSize; z += Chunk.chunkSize)
                        {
                            buildList.Add(new WorldPos(
                                x, y * Chunk.chunkSize, z));
                        }
                    }
                    updateList.Add(new WorldPos(
                                newChunkPos.x, y * Chunk.chunkSize, newChunkPos.z));
                }
                return;
            }

            // If no chunk was added, set doneAddingChunks to 'true'
            doneAddingChunks = !addedNewChunk;
            /*
            if (doneAddingChunks) {
                StartGame();
            }
            */
        }
    }

    void BuildChunk(WorldPos pos)
    {
        if (world.GetChunk(pos.x, pos.y, pos.z) == null)
            world.CreateChunk(pos.x, pos.y, pos.z);
    }

    void LoadAndRenderChunks()
    {
        if (buildList.Count != 0)
        {
            for (int i = 0; i < buildList.Count && i < 8; i++)
            {
                BuildChunk(buildList[0]);
                buildList.RemoveAt(0);
            }
            // If chunks were built, then return early
            return;
        }
        if (updateList.Count != 0)
        {
            Chunk chunk = world.GetChunk(updateList[0].x, updateList[0].y, updateList[0].z);
            if (chunk != null) {
                chunk.update = true;
                /*
                // Check if the player has been created yet
                if (!createdPlayer) {
                    // If the current chunk position equals the following hard-coded position, create the player
                    if (chunk.pos.Equals(new WorldPos(0, -16, -16))) {
                        // Create the crab player at a hard-coded position
                        world.CreateCrab(10, -8, -5);

                        // Set to 'true' so we know the player has been created
                        createdPlayer = true;
                    }
                }
                */
            }
            updateList.RemoveAt(0);
        }
    }

    bool DeleteChunks()
    {
        if (timer == 10)
        {
            var chunksToDelete = new List<WorldPos>();
            foreach (var chunk in world.chunks)
            {
                float distance = Vector3.Distance(
                    new Vector3(chunk.Value.pos.x, 0, chunk.Value.pos.z),
                    new Vector3(transform.position.x, 0, transform.position.z));
                if (distance > 256) // TO-DO: Deal with magic numbers?
                    chunksToDelete.Add(chunk.Key);
            }
            foreach (var chunk in chunksToDelete)
                world.DestroyChunk(chunk.x, chunk.y, chunk.z);
            timer = 0;
            return true;
        }
        timer++;
        return false;
    }

    IEnumerator ShowTitleScreen() {
        yield return new WaitForSeconds(15);
        StartGame();
    }

    private void StartGame() {
        // Check if the player has been created yet
        if (!createdPlayer) {
            // Create the crab player at a hard-coded position
            world.CreateCrab(10, -8, -5);

            // Set to 'true' so we know the player has been created
            createdPlayer = true;
        }

        // Disable start screen
        titleScreen.SetActive(false);
    }
}