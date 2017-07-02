using Entitas;
using System.Collections.Generic;

[Game]
public sealed class PathComponent : IComponent {
  public List<Tile> path;
  public int currentNodeIndex;
}
