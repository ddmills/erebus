using UnityEngine;
using System.Collections;

public class TestObject : IQuadTreeObject
{
    private Vector3 m_vPosition;
    public TestObject(Vector3 position)
    {
        m_vPosition = position;
    }
    public Vector2 GetPosition()
    {
        //Ignore the Y position, Quad-trees operate on a 2D plane.
        return new Vector2(m_vPosition.x, m_vPosition.z);
    }
}

public class Quadtest : MonoBehaviour
{
    public GameObject terrainCube;
    private GameObject holder;
    public QuadTree<TestObject> quad;
    float seed = 0;

    public float Height(float x, float y)
    {
        var perlinX = seed + 1000 + x / 15f;
        var perlinY = seed + 1000 + y / 15f;
        return Mathf.PerlinNoise(perlinX, perlinY);
    }

    void Start()
    {
        holder = new GameObject("holder");
        quad = new QuadTree<TestObject>(1, new Rect(0, 0, 128 , 128));
        for (int y = 0; y < 128; y++)
        {
            for (int x = 0; x < 128; x++)
            {   
                if (Height(x,y) > 0.5)
                {
                    Vector3 v = new Vector3(x, 0, y);
                    TestObject newObject = new TestObject(v);
                    quad.Insert(newObject);
                    //GameObject o = Instantiate(terrainCube);
                    //o.transform.position = v+new Vector3(0.5f,0,0.5f);
                    //o.transform.parent = holder.transform;
                }

            }
        }
        quad.PlaceObject(terrainCube, holder);
    }


    void Update()
    {

    }

    void OnDrawGizmos()
    {
        if (quad != null)
        {
            quad.DrawDebug();
        }
    }
}
