using UnityEngine;

public class QuadTree<T> where T : class {
  private QuadTree<T>[] children;
  private bool solid = false;
  private Rect bounds;
  private T visual;
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

  public void Insert(int x, int y) {
    if (bounds.width < 1.1f) {
      solid = true;
      return;
    }

    if (isLeaf()) {
      var subWidth = (bounds.width / 2f);
      var subHeight = (bounds.height / 2f);
      var subX = bounds.x;
      var subY = bounds.y;

      children[0] = new QuadTree<T>(renderer, new Rect(subX + subWidth, subY, subWidth, subHeight), false);
      children[1] = new QuadTree<T>(renderer, new Rect(subX, subY, subWidth, subHeight), false);
      children[2] = new QuadTree<T>(renderer, new Rect(subX, subY + subHeight, subWidth, subHeight), false);
      children[3] = new QuadTree<T>(renderer, new Rect(subX + subWidth, subY + subHeight, subWidth, subHeight), false);
    }

    var child = GetChildContaining(x, y);
    if (child != null) {
      child.Insert(x, y);
    }
  }

  public void Remove(int x, int y) {
    var position = new Vector2(x, y);
    if (bounds.Contains(position)) {
      if (!isLeaf()) {
        for (var i = 0; i < children.Length; i++) {
          children[i].Remove(x, y);
        }
      } else {
        solid = false;
      }
    }
  }

  private QuadTree<T> GetChildContaining(int x, int y) {
    var position = new Vector2(x, y);
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

  public void Render() {
    if (Filled()) {
      if (visual == null) {
        visual = renderer.RenderCell(bounds);
      }
    } else {
      if (visual != null) {
        renderer.RemoveCell(visual);
        visual = null;
      }
      for (var i = 0; i < children.Length; i++) {
        if (children[i] != null) {
          children[i].Render();
        }
      }
    }
  }

  private bool isLeaf() {
    return children[0] == null;
  }
}
