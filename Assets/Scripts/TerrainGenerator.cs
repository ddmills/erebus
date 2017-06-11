using UnityEngine;

public class TerrainGenerator {
  int seed;
  int mapSize;
  float tileSize;
  SplatPrototype[] splats;

  public TerrainGenerator(int seed, int mapSize, float tileSize) {
    this.seed = seed;
    this.mapSize = mapSize;
    this.tileSize = tileSize;

    var sand = Resources.Load<Texture2D>("Textures/Terrain/sand");
    var dust = Resources.Load<Texture2D>("Textures/Terrain/sand2");

    splats = new SplatPrototype[2];

    splats[0] = new SplatPrototype();
    splats[0].texture = sand;
    splats[0].smoothness = 1;

    splats[1] = new SplatPrototype();
    splats[1].texture = dust;
    splats[1].smoothness = 1;
  }

  private float Height(float x, float y) {
    var perlinX = seed + 1000 + x * mapSize;
    var perlinY = seed + 1000 + y * mapSize;

    return Mathf.PerlinNoise(perlinX, perlinY);
  }

  public TerrainData Generate() {
    var terrain = new TerrainData();
    terrain.size = new Vector3(mapSize * 10 * tileSize, 0, mapSize * 10 * tileSize);
    terrain.splatPrototypes = splats;

    var splatmap = new float[
      terrain.alphamapWidth,
      terrain.alphamapHeight,
      terrain.alphamapLayers
    ];

    for (var y = 0; y < terrain.alphamapHeight; y++) {
      for (var x = 0; x < terrain.alphamapWidth; x++) {
        var height = Height((float) x / terrain.alphamapWidth, (float) y / terrain.alphamapHeight);

        splatmap[x, y, 0] = height;
        splatmap[x, y, 1] = 1 - height;
      }
    }

    terrain.SetAlphamaps(0, 0, splatmap);

    return terrain;
  }
}
