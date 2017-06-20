using UnityEngine;

public class QuadTree<T> where T : class {
  private QuadTree<T>[] children;
  private bool solid = false;
  private Rect bounds;
  private T cell;
  private CellRenderer<T> renderer;

  private QuadTree(CellRenderer<T> renderer, Rect bounds, bool root) {
    this.bounds = bounds;
    this.renderer = renderer;
    children = new QuadTree<T>[4];
  }

  public QuadTree(CellRenderer<T> renderer, Rect bounds) {
    this.renderer = renderer;
    this.bounds = bounds;
    bounds.width = ContainingPowerOf2(bounds.width);
    bounds.height = ContainingPowerOf2(bounds.height);
    children = new QuadTree<T>[4];
  }

  private bool Filled() {
    if (isLeaf()) {
      return solid;
    }
    return (children[0].Filled() && children[1].Filled() && children[2].Filled() && children[3].Filled());
  }

  public void Insert(Vector2 position) {
    if (bounds.width < 1.1) {
      solid = true;
      return;
    }

    if (isLeaf()) {
      var subWidth = (bounds.width / 2f);
      var subHeight = (bounds.height / 2f);
      var x = bounds.x;
      var y = bounds.y;

      children[0] = new QuadTree<T>(renderer, new Rect(x + subWidth, y, subWidth, subHeight), false);
      children[1] = new QuadTree<T>(renderer, new Rect(x, y, subWidth, subHeight), false);
      children[2] = new QuadTree<T>(renderer, new Rect(x, y + subHeight, subWidth, subHeight), false);
      children[3] = new QuadTree<T>(renderer, new Rect(x + subWidth, y + subHeight, subWidth, subHeight), false);
    }

    var child = GetChildContaining(position);
    if (child != null) {
      child.Insert(position);
    }
  }

  public void Remove(Vector2 position) {
    if (bounds.Contains(position)) {
      if (!isLeaf()) {
        for (var i = 0; i < children.Length; i++) {
          children[i].Remove(position);
        }
      } else {
        solid = false;
      }
    }
  }

  private QuadTree<T> GetChildContaining(Vector2 position) {
    for (var i = 0; i < children.Length; i++) {
      if (children[i].bounds.Contains(position)) {
        return children[i];
      }
    }
    return null;
  }

  private float ContainingPowerOf2(float n) {
    var res = 2f;
    while (res < n) {
      res = Mathf.Pow(res, 2);
    }
    return res;
  }

  public void DrawDebug() {
    if (Filled()) {
      Gizmos.DrawLine(new Vector3(bounds.x, 0, bounds.y), new Vector3(bounds.x, 0, bounds.y + bounds.height));
      Gizmos.DrawLine(new Vector3(bounds.x, 0, bounds.y), new Vector3(bounds.x + bounds.width, 0, bounds.y));
      Gizmos.DrawLine(new Vector3(bounds.x + bounds.width, 0, bounds.y), new Vector3(bounds.x + bounds.width, 0, bounds.y + bounds.height));
      Gizmos.DrawLine(new Vector3(bounds.x, 0, bounds.y + bounds.height), new Vector3(bounds.x + bounds.width, 0, bounds.y + bounds.height));
      return;
    }

    if (!isLeaf()) {
      for (var i = 0; i < children.Length; i++) {
        if (!children[i].isLeaf()) {
          children[i].DrawDebug();
        }
      }
    }
  }

  public void Visualize() {
    if (Filled()) {
      if (cell == null) {
        cell = renderer.Render(bounds);
      }
    } else {
      if (cell != null) {
        cell = null;
      }

      if (!isLeaf()) {
        for (var i = 0; i < children.Length; i++) {
          if (!children[i].isLeaf()) {
            children[i].Visualize();
          }
        }
      }
    }
  }

  private bool isLeaf() {
    return children[0] == null;
  }
}
