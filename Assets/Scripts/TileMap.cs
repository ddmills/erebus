using System;

public class TileMap<T> where T : new() {
  public int Width { get; }
  public int Height { get; }
  private T[,] tiles;

  public TileMap(int width, int height) {
    Width = width;
    Height = height;
    tiles = new T[Height, Width];

    for (var i = 0; i < Height; i++) {
      for (var j = 0; j < Width; j++) {
        tiles[i, j] = new T();
      }
    }
  }

  public T Get(int x, int y) {
    return tiles[y, x];
  }

  public void Set(int x, int y, T tile) {
    tiles[y, x] = tile;
  }
}
