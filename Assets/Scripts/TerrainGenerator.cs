using UnityEngine;

public class TerrainGenerator {
  int mapSize;
  float tileSize;

  public TerrainGenerator(int mapSize, float tileSize) {
    this.mapSize = mapSize;
    this.tileSize = tileSize;
  }

  public TerrainData Generate(int seed) {
    var data = new TerrainData();
    data.size = new Vector3(mapSize * 10, mapSize, mapSize * 10);

    return data;
  }
}
