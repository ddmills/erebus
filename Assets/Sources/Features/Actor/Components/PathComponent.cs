using Entitas;
using System.Collections.Generic;

[Game]
public sealed class PathComponent : IComponent {
  public List<Tile> tiles;
  public int currentNodeIndex;
}
