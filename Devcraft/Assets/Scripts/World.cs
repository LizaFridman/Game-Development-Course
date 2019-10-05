using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] byte[,,] worldData;
    [SerializeField] int worldX = 16;
    [SerializeField] int worldY = 16;
    [SerializeField] int worldZ = 16;

    // Start is called before the first frame update
    void Start()
    {
        worldData = new byte[worldX, worldY, worldZ];

        for (var x = 0; x < worldX; x++) {
            for (var y = 0; y < worldY; y++) {
                for (var z = 0; z < worldZ; z++) {
                    if (y <= 8) {
                        worldData[x, y, z] = (byte)TextureType.Rock.GetHashCode();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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
