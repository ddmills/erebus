using Entitas;
using Entitas.CodeGeneration.Attributes;

[World, Unique]
public sealed class TileMapComponent : IComponent {
  public TileMap<Tile> tiles;
}
