using System;
using System.Collections.Generic;

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
    return tiles[y, x];
  }

  public void Set(int x, int y, T tile) {
    tile.X = x;
    tile.Y = y;
    tiles[y, x] = tile;
  }

  public bool OutOfBounds(int x, int y) {
    return x >= Width || y >= Height || x < 0 || y < 0;
  }

  public T[] Neighbors(int x, int y) {
    var neighbors = new T[8];

    neighbors[0] = OutOfBounds(x - 1, y + 1) ? default(T) : Get(x - 1, y + 1);
    neighbors[1] = OutOfBounds(x, y + 1) ? default(T) : Get(x, y + 1);
    neighbors[2] = OutOfBounds(x + 1, y + 1) ? default(T) : Get(x + 1, y + 1);
    neighbors[3] = OutOfBounds(x - 1, y) ? default(T) : Get(x - 1, y);
    neighbors[4] = OutOfBounds(x + 1, y) ? default(T) : Get(x + 1, y);
    neighbors[5] = OutOfBounds(x - 1, y - 1) ? default(T) : Get(x - 1, y - 1);
    neighbors[6] = OutOfBounds(x, y - 1) ? default(T) : Get(x, y - 1);
    neighbors[7] = OutOfBounds(x + 1, y - 1) ? default(T) : Get(x + 1, y - 1);

    return neighbors;
  }

  public T[] Neighbors(T tile) {
    return Neighbors(tile.X, tile.Y);
  }

  public T[] ImmediateNeighbors(T tile) {
    var neighbors = new T[3];

    var x = tile.X;
    var y = tile.Y;

    neighbors[0] = OutOfBounds(x, y + 1) ? default(T) : Get(x, y + 1);
    neighbors[1] = OutOfBounds(x - 1, y) ? default(T) : Get(x - 1, y);
    neighbors[2] = OutOfBounds(x + 1, y) ? default(T) : Get(x + 1, y);
    neighbors[3] = OutOfBounds(x, y - 1) ? default(T) : Get(x, y - 1);

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

  public List<T> InRadius(int x, int y, int radius, Func<T, bool> test) {
    var nodes = new List<T>();
    var radiusSquared = radius * radius;

    for (var relX = -radius; relX <= radius; relX++) {
      for (var relY = -radius; relY <= radius; relY++) {
        var tileX = x + relX;
        var tileY = y + relY;

        if (OutOfBounds(tileX, tileY)) {
          continue;
        }

        var node = Get(tileX, tileY);
        var dX = node.X - x;
        var dY = node.Y - y;
        var distanceSquared = dX * dX + dY * dY;

        if (distanceSquared <= radiusSquared && test(node)) {
          nodes.Add(node);
        }
      }
    }

    return nodes;
  }
}
