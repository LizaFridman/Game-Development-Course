using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noise;

public class World : MonoBehaviour
{
    [SerializeField] GameObject chunk;
    [SerializeField] int worldX = 16;
    [SerializeField] int worldY = 16;
    [SerializeField] int worldZ = 16;
    [SerializeField] int chunkSize = 16;

    private byte[,,] worldData;
    private Chunk[,,] chunks;

    // Start is called before the first frame update
    void Start()
    {
        worldData = new byte[worldX, worldY, worldZ];

        for (var x = 0; x < worldX; x++)
        {
            for (var z = 0; z < worldZ; z++)
            {
                var rock = PerlinNoise(x, 0, z, 10f, 3f, 1.2f);
                rock += PerlinNoise(x, 200, z, 20, 8f, 0f) + 10;

                var grass = PerlinNoise(x, 100, z, 50, 30f, 0f) + 1;
                for (var y = 0; y < worldY; y++)
                {
                    if (y <= rock)
                    {
                        worldData[x, y, z] = (byte)TextureType.Grass.GetHashCode();
                    }
                    else if (y <= grass)
                    {
                        worldData[x, y, z] = (byte)TextureType.Rock.GetHashCode();
                    }
                }
            }
        }

        chunks = new Chunk[Mathf.FloorToInt(worldX / chunkSize), Mathf.FloorToInt(worldY / chunkSize), Mathf.FloorToInt(worldZ / chunkSize)];
        for (var x = 0; x < chunks.GetLength(0); x++)
        {
            for (var y = 0; y < chunks.GetLength(1); y++)
            {
                for (var z = 0; z < chunks.GetLength(2); z++)
                {
                    var newChunk = Instantiate(chunk, new Vector3(x * chunkSize, y * chunkSize, z * chunkSize), new Quaternion(0, 0, 0, 0)) as GameObject;
                    chunks[x, y, z] = newChunk.GetComponent("Chunk") as Chunk;
                    chunks[x, y, z].WorldGO = gameObject;
                    chunks[x, y, z].ChunkSize = chunkSize;
                    chunks[x, y, z].ChunkX = x * chunkSize;
                    chunks[x, y, z].ChunkY = y * chunkSize;
                    chunks[x, y, z].ChunkZ = z * chunkSize;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int PerlinNoise(int x, int y, int z, float scale, float height, float power)
    {
        var perlinValue = Noise.Noise.GetNoise((double)x / scale, (double)y / scale, (double)z / scale);
        perlinValue *= height;

        if (power != 0)
        {
            perlinValue = Mathf.Pow(perlinValue, power);
        }

        return (int)perlinValue;
    }

    public byte Block(int x, int y, int z) {
        if (x >= worldX || x < 0 ||
            y >= worldY || y < 0 ||
            z >= worldZ || z < 0) {

            return (byte)TextureType.Rock.GetHashCode();
        }

        return worldData[x, y, z];
    }
}
