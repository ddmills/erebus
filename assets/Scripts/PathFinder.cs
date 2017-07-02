using System;
using System.Collections.Generic;

public sealed class PathFinder<T> where T : ICoordinate, new() {
  private readonly TileMap<T> map;

  public PathFinder(TileMap<T> map) {
    this.map = map;
  }

  public List<T> Find(T start, T goal, Func<T, float> weight) {
    var open = new List<T> { start };
    var closed = new List<T>();
    var parents = new Dictionary<T, T>();
    var movementCosts = new Dictionary<T, float> {{ start, 0f }};
    var estimatedCosts = new PriorityQueue<T, float>();

    estimatedCosts.Enqueue(start, Heuristic(start, goal));

    while (open.Count > 0) {
      var current = estimatedCosts.Dequeue();

      if (current.Equals(goal)) {
        var path = new List<T>(parents.Values);
        path.Add(goal);
        return path;
      }

      open.Remove(current);
      closed.Add(current);

      foreach (var neighbour in map.Neighbours(current)) {
        if (neighbour == null) {
          continue;
        }

        var movementCost = weight(neighbour);

        if (movementCost < 0 || closed.Contains(neighbour) || open.Contains(neighbour)) {
          continue;
        }

        parents.Remove(current);
        parents.Add(current, neighbour);
        movementCosts[neighbour] = movementCost;
        estimatedCosts.Enqueue(neighbour, movementCost + Heuristic(neighbour, goal));
        open.Add(neighbour);
      }
    }

    return null;
  }

  private float Heuristic(T node, T goal) {
    return Distance(node, goal);
  }

  private float Distance(T from, T to) {
    var dX = (to.X - from.X);
    var dY = (to.Y - from.Y);
    return (float) Math.Sqrt(dX * dX + dY * dY);
  }
}
