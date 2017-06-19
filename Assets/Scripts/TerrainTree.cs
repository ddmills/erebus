using UnityEngine;

public class TerrainTree {
  public TerrainTree[] cells;
  public bool solid = false;
  public Rect bounds;
  public GameObject visual;

  public TerrainTree(Rect bounds, bool root) {
    cells = new TerrainTree[4];
    this.bounds = bounds;
  }

  public TerrainTree(Rect bounds) {
    bounds.width = ContainingPowerOf2(bounds.width);
    bounds.height = ContainingPowerOf2(bounds.height);
    cells = new TerrainTree[4];
    this.bounds = bounds;
  }

  public bool Filled() {
    if (cells[0] == null) {
      return solid;
    }
    return (cells[0].Filled() && cells[1].Filled() && cells[2].Filled() && cells[3].Filled());
  }

  public void Insert(Vector2 pos) {
    if (bounds.width < 1.1) {
      solid = true;
      return;
    }

    if (cells[0] == null) {
      var subWidth = (bounds.width / 2f);
      var subHeight = (bounds.height / 2f);
      var x = bounds.x;
      var y = bounds.y;

      cells[0] = new TerrainTree(new Rect(x + subWidth, y, subWidth, subHeight), false);
      cells[1] = new TerrainTree(new Rect(x, y, subWidth, subHeight), false);
      cells[2] = new TerrainTree(new Rect(x, y + subHeight, subWidth, subHeight), false);
      cells[3] = new TerrainTree(new Rect(x + subWidth, y + subHeight, subWidth, subHeight), false);
    }

    var iCell = GetCellToInsertObject(pos);
    if (iCell > -1) {
      cells[iCell].Insert(pos);
    }
  }

  public void Remove(Vector2 pos) {
    if (bounds.Contains(pos)) {
      if (cells[0] != null) {
        for (var i = 0; i < 4; i++) {
          cells[i].Remove(pos);
        }
      } else {
        solid = false;
      }
    }
  }

  private int GetCellToInsertObject(Vector2 location) {
    for (var i = 0; i < 4; i++) {
      if (cells[i].bounds.Contains(location)) {
        return i;
      }
    }
    return -1;
  }

  public float ContainingPowerOf2(float n) {
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

    if (cells[0] != null) {
      for (var i = 0; i < cells.Length; i++) {
        if (cells[i] != null) {
          cells[i].DrawDebug();
        }
      }
    }
  }

  public void Visualize(GameObject template, GameObject holder) {
    if (Filled()) {
      if (visual == null) {
        var loc = new Vector3(bounds.x + bounds.width / 2f, 0f, bounds.y + bounds.height / 2f);
        var scl = new Vector3(bounds.width, 1, bounds.height);
        var instance = GameObject.Instantiate(template);
        instance.transform.position = loc;
        instance.transform.localScale = scl;
        instance.transform.parent = holder.transform;
        visual = instance;
      }
    } else {
      if (visual != null) {
        GameObject.Destroy(visual);
        visual = null;
      }

      if (cells[0] != null) {
        for (var i = 0; i < cells.Length; i++) {
          if (cells[i] != null) {
            cells[i].Visualize(template, holder);
          }
        }
      }
    }
  }
}
