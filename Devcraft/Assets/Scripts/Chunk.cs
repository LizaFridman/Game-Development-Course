using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TextureType {
    Air, Grass, Rock
}

public class Chunk : MonoBehaviour
{
    private List<Vector3> newVertices = new List<Vector3>();
    private List<int> newTriangles = new List<int>();
    private List<Vector2> newUV = new List<Vector2>();

    private Mesh mesh;
    private MeshCollider chunkCollider;
    private float textureWidth = 0.083f;
    private int faceCount;
    private World world;
    private int chunkSize;
    private int chunkX;
    private int chunkY;
    private int chunkZ;
    private GameObject worldGO;

    private Vector2 grassTop = new Vector2(1,11);
    private Vector2 grassSide = new Vector2(0,10);
    private Vector2 rock = new Vector2(7, 8);

    public int ChunkSize { get => chunkSize; set => chunkSize = value; }
    public int ChunkX { get => chunkX; set => chunkX = value; }
    public int ChunkY { get => chunkY; set => chunkY = value; }
    public int ChunkZ { get => chunkZ; set => chunkZ = value; }
    public GameObject WorldGO { get => worldGO; set => worldGO = value; }


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        chunkCollider = GetComponent<MeshCollider>();
        world = worldGO.GetComponent("World") as World;

        GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMesh()
    {
        for (var x = 0; x < chunkSize; x++)
        {
            for (var y = 0; y < chunkSize; y++)
            {
                for (var z = 0; z < chunkSize; z++)
                {
                    if (Block(x, y, z) != (byte)TextureType.Air.GetHashCode()) {
                        // Block above is air
                        if (Block(x, y + 1, z) == (byte)TextureType.Air.GetHashCode()) {
                            CubeTop(x, y, z, Block(x, y, z));
                        }
                        // Block below is air
                        if (Block(x, y - 1, z) == (byte)TextureType.Air.GetHashCode())
                        {
                            CubeBottom(x, y, z, Block(x, y, z));
                        }
                        // Block to the east is air
                        if (Block(x + 1, y, z) == (byte)TextureType.Air.GetHashCode())
                        {
                            CubeEast(x, y, z, Block(x, y, z));
                        }
                        // Block to the west is air
                        if (Block(x - 1, y, z) == (byte)TextureType.Air.GetHashCode())
                        {
                            CubeWest(x, y, z, Block(x, y, z));
                        }
                        // Block to the north is air
                        if (Block(x, y, z + 1) == (byte)TextureType.Air.GetHashCode())
                        {
                            CubeNorth(x, y, z, Block(x, y, z));
                        }
                        // Block to the south is air
                        if (Block(x, y, z - 1) == (byte)TextureType.Air.GetHashCode())
                        {
                            CubeSouth(x, y, z, Block(x, y, z));
                        }
                    }
                        
                }
            }
        }
        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.triangles = newTriangles.ToArray();

        mesh.Optimize();
        mesh.RecalculateNormals();

        chunkCollider.sharedMesh = null;
        chunkCollider.sharedMesh = mesh;

        newVertices.Clear();
        newUV.Clear();
        newTriangles.Clear();
        faceCount = 0;
    }

    void CubeTop(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x, y, z));

        Vector2 texturePosition = new Vector2(0, 0);

        if (block == (byte)TextureType.Rock.GetHashCode())
        {
            texturePosition = rock;
        }
        else if (block == (byte)TextureType.Grass.GetHashCode())
        {
            texturePosition = grassTop;
        }

        Cube(texturePosition);
    }

    void CubeNorth(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

        Vector2 texturePosition = GetSideTexture(x,y,z, block);

        Cube(texturePosition);
    }

    void CubeEast(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x + 1, y - 1 , z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));

        Vector2 texturePosition = GetSideTexture(x, y, z, block);

        Cube(texturePosition);
    }

    void CubeSouth(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));

        Vector2 texturePosition = GetSideTexture(x, y, z, block);

        Cube(texturePosition);
    }

    void CubeWest(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x, y - 1, z));

        Vector2 texturePosition = GetSideTexture(x, y, z, block);

        Cube(texturePosition);
    }

    void CubeBottom(int x, int y, int z, byte block)
    {
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

        Vector2 texturePosition = GetSideTexture(x, y, z, block);

        Cube(texturePosition);
    }

    public Vector2 GetSideTexture(int x, int y, int z, byte block)
    {
        var texturePos = new Vector2(0, 0);

        if (block == (byte)TextureType.Rock.GetHashCode())
        {
            texturePos = rock;
        }
        else if (block == (byte)TextureType.Grass.GetHashCode())
        {
            texturePos = grassSide;
        }

        return texturePos;
    }

    void Cube(Vector2 texturePosition) {
        newTriangles.Add(faceCount * 4);// 1
        newTriangles.Add(faceCount * 4 + 1);// 2
        newTriangles.Add(faceCount * 4 + 2);// 3
        newTriangles.Add(faceCount * 4);// 1
        newTriangles.Add(faceCount * 4 + 2);// 3
        newTriangles.Add(faceCount * 4 + 3);// 4

        newUV.Add(new Vector2(textureWidth * texturePosition.x + textureWidth, textureWidth * texturePosition.y));
        newUV.Add(new Vector2(textureWidth * texturePosition.x + textureWidth, textureWidth * texturePosition.y + textureWidth));
        newUV.Add(new Vector2(textureWidth * texturePosition.x, textureWidth * texturePosition.y + textureWidth));
        newUV.Add(new Vector2(textureWidth * texturePosition.x, textureWidth * texturePosition.y));

        faceCount++;
    }

    byte Block(int x, int y, int z) {
        return world.Block(x + chunkX, y + ChunkY, z + ChunkZ);
    }
}
