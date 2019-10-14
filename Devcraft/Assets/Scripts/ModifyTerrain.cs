
using UnityEngine;

public class ModifyTerrain : Singleton<ModifyTerrain>
{
    private World world;
    private GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        world = gameObject.GetComponent("World") as World;
        character = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        LoadChunks(character.transform.position, 50,60);
    }

    public void DestroyBlock(float range, byte block) {
        var ray = new Ray(character.transform.position, character.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.distance < range)
            {
                DestroyBlockAt(hit, block);
            }
        }
    }

    public void AddBlock(float range, byte block)
    {
        var ray = new Ray(character.transform.position, character.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.distance < range)
            {
                AddBlockAt(hit, block);
            }
        }
    }

    public void DestroyBlockAt(RaycastHit hit, byte block)
    {
        var position = hit.point;
        position += (hit.normal * (-0.5f));
        SetBlockAt(position, block);
    }

    public void AddBlockAt(RaycastHit hit, byte block) {
        var position = hit.point;
        position += (hit.normal * 0.5f);
        SetBlockAt(position, block);
    }

    public void SetBlockAt(Vector3 position, byte block)
    {
        var x = Mathf.RoundToInt(position.x);
        var y = Mathf.RoundToInt(position.y);
        var z = Mathf.RoundToInt(position.z);

        world.WorldData[x, y, z] = block;
        UpdateChunkAt(x,y,z);
    }

    public void UpdateChunkAt(int x, int y, int z)
    {
        var updateX = Mathf.FloorToInt(x / world.ChunkSize);
        var updateY = Mathf.FloorToInt(y / world.ChunkSize);
        var updateZ = Mathf.FloorToInt(z / world.ChunkSize);

        world.Chunks[updateX, updateY, updateZ].IsUpdate = true;
    }

    public void LoadChunks(Vector3 playerPosition, float distanceToLoad, float distanceToDestroy)
    {
        for (var x = 0; x < world.Chunks.GetLength(0); x++)
        {
            for (var z = 0; z < world.Chunks.GetLength(2); z++)
            {
                var distance = Vector2.Distance(new Vector2(x * world.ChunkSize, z * world.ChunkSize), new Vector2(playerPosition.x, playerPosition.z));

                if (distance < distanceToLoad)
                {
                    if (world.Chunks[x, 0, z] == null)
                    {
                        world.GenerateChunk(x, z);
                    }
                }
                else if (distance > distanceToDestroy)
                {
                    if (world.Chunks[x, 0, z] != null)
                    {
                        world.DestroyChunk(x, z);
                    }
                }
            }
        }
    }
}
