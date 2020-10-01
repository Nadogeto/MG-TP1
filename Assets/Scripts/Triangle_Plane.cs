using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle_Plane : MonoBehaviour
{
    public Material material;

    [Range(1, 10)]
    public int planeHeight = 1;
    [Range(1, 10)]
    public int planeWidth = 1;

    private int old_planeHeight = 0;
    private int old_planeWidth = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(old_planeHeight != planeHeight || old_planeWidth != planeWidth)
        {
            //position des sommets
            Vector3[] vertices = new Vector3[4 * planeHeight * planeWidth];
            //tableau du n° de triangles
            int[] triangles = new int[6 * planeWidth * planeHeight];

            for(int x = 0; x < planeHeight; x++)
            {
                for(int y = 0; y < planeWidth; y++)
                {
                    int i = (x + y * planeHeight) * 4;
                    vertices[i] = new Vector3(x, y, 0);
                    vertices[i + 1] = new Vector3(x + 1, y, 0);
                    vertices[i + 2] = new Vector3(x, y + 1, 0);
                    vertices[i + 3] = new Vector3(x + 1, y + 1, 0);

                    int j = (x + y * planeHeight) * 6;
                    triangles[j] = i;
                    triangles[j + 1] = i + 1;
                    triangles[j + 2] = i + 2;
                    triangles[j + 3] = i + 1;
                    triangles[j + 4] = i + 3;
                    triangles[j + 5] = i + 2;
                }
            }

            //vertices[0] = new Vector3(0,0,0); 
            //vertices[1] = new Vector3(1,0,0); 
            //vertices[2] = new Vector3(0,1,0); 

            //triangles[0] = 0;
            //triangles[1] = 1;
            //triangles[2] = 2;

            Mesh mesh = new Mesh();

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.GetComponent<MeshRenderer>().material = material;

        }

    }
}
