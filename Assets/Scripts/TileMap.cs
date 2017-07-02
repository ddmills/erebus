using System;

public class TileMap<T> where T : ICoordinate, new() {
  public int Width { get; }
  public int Height { get; }
  private T[,] tiles;

  public TileMap(int size) : this(size, size) {
  }

  public TileMap(int width, int height) {
    Width = width;
    Height = height;
    tiles = new T[Height, Width];
    Map(tile => new T());
  }

  public T Get(int x, int y) {
    return OutOfBounds(x, y) ? default(T) : tiles[y, x];
  }

  public void Set(int x, int y, T tile) {
    tile.X = x;
    tile.Y = y;
    tiles[y, x] = tile;
  }

  public bool OutOfBounds(int x, int y) {
    return x > Width || y > Height || x < 0 || y < 0;
  }

  public T[] Neighbors(T tile) {
    var neighbors = new T[8];

    var x = tile.X;
    var y = tile.Y;

    neighbors[0] = Get(x - 1, y + 1);
    neighbors[1] = Get(x, y + 1);
    neighbors[2] = Get(x + 1, y + 1);
    neighbors[3] = Get(x - 1, y);
    neighbors[4] = Get(x + 1, y);
    neighbors[5] = Get(x - 1, y - 1);
    neighbors[6] = Get(x, y - 1);
    neighbors[7] = Get(x + 1, y - 1);

    return neighbors;
  }

  public TileMap<T> ForEach(Action<T> callback) {
    for (var i = 0; i < Height; i++) {
      for (var j = 0; j < Width; j++) {
        callback(tiles[i, j]);
      }
    }

    return this;
  }

  public TileMap<T> Map(Func<T, T> callback) {
    for (var i = 0; i < Height; i++) {
      for (var j = 0; j < Width; j++) {
        Set(j, i, callback(tiles[i, j]));
      }
    }

    return this;
  }
}
