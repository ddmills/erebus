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
    var path = new Dictionary<T, T>();
    var gScores = new Dictionary<T, float> {{ start, 0f }};
    var fScores = new PriorityQueue<T, float>();

    fScores.Enqueue(start, Heuristic(start, goal));

    while (open.Count > 0) {
      var current = fScores.Dequeue();

      if (current.Equals(goal)) {
        var finalPath = new List<T>(path.Values);
        finalPath.Reverse();
        return finalPath;
      }

      open.Remove(current);
      closed.Add(current);

      foreach (var neighbour in map.Neighbours(current)) {
        // var w = weight(neighbour);

        if (closed.Contains(neighbour)) {
          continue;
        }

        if (!open.Contains(neighbour)) {
          open.Add(neighbour);
        }

        var gScore = gScores[current] + Distance(current, neighbour);
        if (gScore >= gScores[neighbour]) {
          continue;
        }

        path.Add(current, neighbour);
        gScores[neighbour] = gScore;
        fScores.Enqueue(neighbour, gScore + Heuristic(neighbour, goal));
      }
    }

    return null;
  }

  private float Heuristic(T start, T goal) {
    return 1f;
  }

  private float Distance(T from, T to) {
    var dX = (to.X - from.X);
    var dY = (to.Y - from.Y);
    return (float) Math.Sqrt(dX * dX + dY * dY);
  }
}
