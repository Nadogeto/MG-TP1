using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public Material material;

    [Range(1, 100)]
    public int radius = 2;
    [Range(1, 100)]
    public int height = 5;
    [Range(5, 50)]
    public int meridians = 5;


    private int old_radius = 0;
    private int old_height = 0;
    private int old_meridians = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (old_radius !=  radius || old_height != height || old_meridians != meridians)
        {
            //sommets
            Vector3[] vertices = new Vector3[(meridians * 2) + 2];
            //triangles (qui créent les cercles (-h/2, h/2))
            int[] triangles = new int[6 * meridians * 2]; 

                for (int i = 0; i < meridians; i++)
                {
                    // calcule la valeur de θi= 2π i/m
                    float theta = 2 * Mathf.PI * i / meridians;

                    //crée les sommets des méridien 
                    vertices[i * 2] = new Vector3(radius * Mathf.Cos(theta), -height / 2, radius * Mathf.Sin(theta));
                    vertices[i * 2 + 1] = new Vector3(radius * Mathf.Cos(theta), height / 2, radius * Mathf.Sin(theta));

                    //calcule l'index du prochain mer
                    int nexti = (i < meridians - 1) ? i + 1 : 0;

                    //assigne des sommets 
                    triangles[i * 6] = i * 2;
                    triangles[i * 6 + 1] = i * 2 + 1;
                    triangles[i * 6 + 2] = nexti * 2;

                    triangles[i * 6 + 3] = i * 2 + 1;
                    triangles[i * 6 + 4] = nexti * 2 + 1;
                    triangles[i * 6 + 5] = nexti * 2;

                }

            vertices[meridians * 2] = new Vector3(0, -height / 2, 0);
            vertices[meridians * 2 + 1] = new Vector3(0, height / 2, 0);

            int offset = meridians * 6;

                for (int i = 0; i < meridians; ++i)
                {
                    //calcule l'index du prochain mer
                    int ni = (i < meridians - 1) ? i + 1 : 0; 

                    triangles[offset + i * 6] = meridians * 2;
                    triangles[offset + i * 6 + 1] = i * 2;
                    triangles[offset + i * 6 + 2] = ni * 2;

                    triangles[offset + i * 6 + 3] = meridians * 2 + 1;
                    triangles[offset + i * 6 + 4] = ni * 2 + 1;
                    triangles[offset + i * 6 + 5] = i * 2 + 1;
                }

                Mesh mesh = new Mesh();

                mesh.vertices = vertices;
                mesh.triangles = triangles;

                gameObject.GetComponent<MeshFilter>().mesh = mesh;
                gameObject.GetComponent<MeshRenderer>().material = material;
        }
    }
}
