
using System;
using UnityEngine;

public sealed class Map : CellRenderer<GameEntity> {
  public TileMap<Tile> Tiles { get; }
  public QuadTree<GameEntity> mountains;
  public int Size { get; }

  public Map(Config config) {
    Tiles = new TileMap<Tile>(config.mapSize);
    Size = config.mapSize;
    mountains = new QuadTree<GameEntity>(this, new Rect(0, 0, config.mapSize, config.mapSize));
  }

  public Tile GetTile(int x, int y) {
    return Tiles.Get(x, y);
  }

  public void SetTile(int x, int y, Tile tile) {
    Tiles.Set(x, y, tile);
  }

  public void AddMountain(int x, int y) {
    Tiles.Get(x, y).hasMountain = true;
    mountains.Insert(x, y);
  }

  public void RemoveMountain(int x, int y) {
    Tiles.Get(x, y).hasMountain = false;
    mountains.Remove(x, y);
    mountains.Render();
  }

  public GameEntity RenderCell(Rect bounds) {
    return MountainBlueprint.Create(bounds);
  }

  public void RemoveCell(GameEntity cell) {
    cell.isDestroyed = true;
  }
}
