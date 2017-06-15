using UnityEngine;

public class TerrainGenerator {
  int seed;
  int mapSize;
  float scale;
  float tileSize;
  SplatPrototype[] splats;

  public TerrainGenerator(int seed, int mapSize, float tileSize) {
    this.seed = seed;
    this.mapSize = mapSize;
    this.tileSize = tileSize;

    var sand = Resources.Load<Texture2D>("Textures/Terrain/sand3");
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
    var perlinX = seed + 1000 + x / 15f;
    var perlinY = seed + 1000 + y / 15f;
    return Mathf.PerlinNoise(perlinX, perlinY);
  }

  public TerrainData Generate() {
    var terrain = new TerrainData();
    terrain.size = new Vector3(mapSize * tileSize, 0, mapSize * tileSize);
    terrain.splatPrototypes = splats;

    var splatmap = new float[
      terrain.alphamapWidth,
      terrain.alphamapHeight,
      terrain.alphamapLayers
    ];

    for (var y = 0; y < terrain.alphamapHeight; y++) {
      for (var x = 0; x < terrain.alphamapWidth; x++) {
        float realX = ((float) x / terrain.alphamapWidth) * mapSize * tileSize;
        float realY = ((float) y / terrain.alphamapHeight) * mapSize * tileSize;

        var height = Height(realX, realY);

        if (height <= .3f) {
          splatmap[y, x, 0] = 1;
          splatmap[y, x, 1] = 0;
        } else if (height <= .5f) {
          var normalized = (height - .3f) * 5;
          splatmap[y, x, 0] = 1 - normalized;
          splatmap[y, x, 1] = normalized;
        } else {
          splatmap[y, x, 0] = 0;
          splatmap[y, x, 1] = 1;
        }
      }
    }

    terrain.SetAlphamaps(0, 0, splatmap);

    return terrain;
  }
}
