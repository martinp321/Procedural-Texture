using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{

    public int sizeX, sizeY;
    private Vector3[] vertices;

    private void Awake()
    {
        StartCoroutine(Generate());
    }


    private void OnDrawGizmos()
    {
        if (vertices == null) return;

        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

    private Mesh mesh;

    IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(.05f);
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "procedural Grid";
        vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];

        for (int i = 0, y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                yield return wait;
            }
        }

        mesh.vertices = vertices;
        int[] triangles = new int[sizeX * 6];
        for (int ti = 0, vi = 0, x = 0;
            x < sizeX;
            x++, ti += 6, vi++)
        {
            triangles[0] = 0;
            triangles[2] = 1;
            triangles[1] = sizeX + 1;

            triangles[3] = 1;
            triangles[4] = sizeX + 1;
            triangles[5] = sizeX + 2;
        }
        mesh.triangles = triangles;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
