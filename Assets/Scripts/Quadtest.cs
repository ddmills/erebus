using UnityEngine;

public class Quadtest : MonoBehaviour {
  public GameObject terrainCube;
  private GameObject holder;
  public TerrainTree quad;
  float seed = 0;
  private Vector3 mousepos;

  public float Height(float x, float y)
  {
    var perlinX = seed + 1000f + x / 15f;
    var perlinY = seed + 1000f + y / 15f;
    return Mathf.PerlinNoise(perlinX, perlinY);
  }

  void Start()
  {
    holder = new GameObject("holder");
    quad = new TerrainTree(new Rect(0, 0, 120 , 120));
    for (int y = 0; y < 120; y++)
    {
      for (int x = 0; x < 120; x++)
      {
        if (Height(x,y) > 0.5)
        {
          Vector2 v = new Vector2(x, y);
          quad.Insert(v);
        }
      }
    }

  }

  private void Update()
  {
    quad.Visualize(terrainCube, holder);
  }

  private void OnMouseOver()
  {
    RaycastHit hit = new RaycastHit();
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit))
    {
      mousepos = hit.point;
      Vector2 v = new Vector2(hit.point.x, hit.point.z);
      if (Input.GetMouseButton(0)) {
        quad.Insert(v);
      } else if (Input.GetMouseButton(1)) {
        quad.Remove(v);
      }

    }
  }

  void OnDrawGizmos()
  {
    if (mousepos != null)
    {
      Gizmos.color = new Color(1, 0, 1);
      Gizmos.DrawRay(mousepos,new Vector3(1,0,0));
      Gizmos.DrawRay(mousepos, new Vector3(-1, 0, 0));
      Gizmos.DrawRay(mousepos, new Vector3(0, 0, 1));
      Gizmos.DrawRay(mousepos, new Vector3(0, 0, -1));
      Gizmos.color = new Color(1, 1, 1);
    }
    if (quad != null)
    {
      quad.DrawDebug();
    }
  }
}
