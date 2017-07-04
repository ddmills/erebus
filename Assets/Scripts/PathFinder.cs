using System;
using System.Collections.Generic;

public sealed class PathFinder<T> where T : ICoordinate, new() {
  private readonly TileMap<T> map;

  public PathFinder(TileMap<T> map) {
    this.map = map;
  }

  public List<T> Find(T start, T goal, Func<T, T, float> weight) {
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

      if (neighbors[1] == null || weight(current, neighbors[1]) < 0) {
        closed.Add(neighbors[1]);
        neighbors[0] = default(T);
        neighbors[2] = default(T);
      }

      if (neighbors[4] == null || weight(current, neighbors[4]) < 0) {
        closed.Add(neighbors[4]);
        neighbors[2] = default(T);
        neighbors[7] = default(T);
      }

      if (neighbors[6] == null || weight(current, neighbors[6]) < 0) {
        closed.Add(neighbors[6]);
        neighbors[7] = default(T);
        neighbors[5] = default(T);
      }

      if (neighbors[3] == null || weight(current, neighbors[3]) < 0) {
        closed.Add(neighbors[3]);
        neighbors[5] = default(T);
        neighbors[0] = default(T);
      }

      foreach (var neighbor in neighbors) {
        if (neighbor == null || closed.Contains(neighbor)) {
          continue;
        }

        if (!open.Contains(neighbor)) {
          open.Add(neighbor);
        }

        var tentativeCost = weight(current, neighbor);

        if (tentativeCost < 0) {
          open.Remove(neighbor);
          closed.Add(neighbor);
          continue;
        }

        var movementCost = movementCosts[current] + tentativeCost;

        if (!movementCosts.ContainsKey(neighbor) || movementCost < movementCosts[neighbor]) {
          movementCosts[neighbor] = movementCost;
        } else {
          continue;
        }

        parents.Remove(neighbor);
        parents.Add(neighbor, current);
        estimatedCosts.Enqueue(neighbor, movementCost + Heuristic(neighbor, goal));
      }
    }

    return null;
  }

  private float Heuristic(T node, T goal) {
    var dX = (node.X - goal.X);
    var dY = (node.Y - goal.Y);
    return (float) Math.Sqrt(dX * dX + dY * dY);
  }
}
