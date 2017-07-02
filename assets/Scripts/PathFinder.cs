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
        var path = new List<T> { current };

        while (parents.ContainsKey(current)) {
          current = parents[current];
          path.Add(current);
        }

        path.Reverse();

        return path;
      }

      open.Remove(current);
      closed.Add(current);

      var neighbors = map.Neighbors(current);

      movementCosts[neighbors[1]] = weight(neighbors[1]);
      if (movementCosts[neighbors[1]] < 0) {
        neighbors[0] = default(T);
        neighbors[2] = default(T);
      }

      movementCosts[neighbors[4]] = weight(neighbors[4]);
      if (movementCosts[neighbors[4]] < 0) {
        neighbors[2] = default(T);
        neighbors[7] = default(T);
      }

      movementCosts[neighbors[6]] = weight(neighbors[6]);
      if (movementCosts[neighbors[6]] < 0) {
        neighbors[7] = default(T);
        neighbors[5] = default(T);
      }

      movementCosts[neighbors[3]] = weight(neighbors[3]);
      if (movementCosts[neighbors[3]] < 0) {
        neighbors[5] = default(T);
        neighbors[0] = default(T);
      }

      foreach (var neighbor in neighbors) {
        if (neighbor == null) {
          continue;
        }

        if (!movementCosts.ContainsKey(neighbor)) {
          movementCosts[neighbor] = weight(neighbor);
        }

        var movementCost = movementCosts[neighbor];

        if (movementCost < 0) {
          closed.Add(neighbor);
          continue;
        }

        if (closed.Contains(neighbor) || open.Contains(neighbor)) {
          continue;
        }

        parents.Remove(neighbor);
        parents.Add(neighbor, current);
        estimatedCosts.Enqueue(neighbor, movementCost + Heuristic(neighbor, goal));
        open.Add(neighbor);
      }
    }

    return null;
  }

  private float Heuristic(T node, T goal) {
    var dX = (node.X - goal.X);
    var dY = (node.Y - goal.Y);
    return dX * dX + dY * dY;
  }
}
