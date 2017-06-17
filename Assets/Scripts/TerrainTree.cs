using UnityEngine;
using System.Collections.Generic;


public class TerrainTree
{
    public TerrainTree[] cells;
    public bool solid = false;
    public Rect bounds;
    public GameObject visual;

    public TerrainTree(Rect _bounds, bool root)
    {
        cells = new TerrainTree[4];
        bounds = _bounds;
    }
    public TerrainTree(Rect _bounds)
    {
        _bounds.width = ContainingPowerOf2(_bounds.width);
        _bounds.height = ContainingPowerOf2(_bounds.height);
        cells = new TerrainTree[4];
        bounds = _bounds;
    }

    public bool Filled()
    {
        if (cells[0] == null)
        {
            return solid;
        } else
        {
            return (cells[0].Filled() && cells[1].Filled() && cells[2].Filled() && cells[3].Filled());
        }
    }

    public void Insert(Vector2 pos)
    {   
        if (bounds.width < 1.1)
        {
            solid = true;
            return;
        }
        if (cells[0] == null)
        {
            float subWidth = (bounds.width / 2f);
            float subHeight = (bounds.height / 2f);
            float x = bounds.x;
            float y = bounds.y;
            cells[0] = new TerrainTree(new Rect(x + subWidth, y, subWidth, subHeight), false);
            cells[1] = new TerrainTree(new Rect(x, y, subWidth, subHeight), false);
            cells[2] = new TerrainTree(new Rect(x, y + subHeight, subWidth, subHeight), false);
            cells[3] = new TerrainTree(new Rect(x + subWidth, y + subHeight, subWidth, subHeight), false);
        }
        int iCell = GetCellToInsertObject(pos);
        if (iCell > -1)
        {
            cells[iCell].Insert(pos);
        }
    }

    public void Remove(Vector2 pos)
    {
        if (bounds.Contains(pos))
        {
            if (cells[0] != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    cells[i].Remove(pos);
                }
            } else {
                solid = false;
            }
        }
    }

    private int GetCellToInsertObject(Vector2 location)
    {
        for (int i = 0; i < 4; i++)
        {
            if (cells[i].bounds.Contains(location))
                return i;
        }
        return -1;
    }

    public float ContainingPowerOf2(float n)
    {
        float res = 2;
        while (res < n)
            res = Mathf.Pow(res, 2);
        return res;
    }

    public void DrawDebug()
    {
        if (this.Filled())
        {
            Gizmos.DrawLine(new Vector3(bounds.x, 0, bounds.y), new Vector3(bounds.x, 0, bounds.y + bounds.height));
            Gizmos.DrawLine(new Vector3(bounds.x, 0, bounds.y), new Vector3(bounds.x + bounds.width, 0, bounds.y));
            Gizmos.DrawLine(new Vector3(bounds.x + bounds.width, 0, bounds.y), new Vector3(bounds.x + bounds.width, 0, bounds.y + bounds.height));
            Gizmos.DrawLine(new Vector3(bounds.x, 0, bounds.y + bounds.height), new Vector3(bounds.x + bounds.width, 0, bounds.y + bounds.height));
            return;
        }
        if (cells[0] != null)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] != null)
                {
                    cells[i].DrawDebug();
                }
            }
        }
    }

    public void Visualize(GameObject g, GameObject holder)
    {
        if (Filled()) {
            if (visual == null) {
                Vector3 loc = new Vector3(bounds.x + bounds.width / 2f, 0f, bounds.y + bounds.height / 2f);
                Vector3 scl = new Vector3(bounds.width, 1, bounds.height);
                GameObject o = GameObject.Instantiate(g);
                o.transform.position = loc;
                o.transform.localScale = scl;
                o.transform.parent = holder.transform;
                visual = o;
            }
        } else {
            if (visual != null) {
                GameObject.Destroy(visual);
                visual = null;
            }
            if (cells[0] != null)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] != null)
                    {
                        cells[i].Visualize(g, holder);
                    }
                }
            }
        }
    }
}
