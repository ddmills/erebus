using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public sealed class TileMapComponent : IComponent {
  public TileMap<Tile> tiles;
}
