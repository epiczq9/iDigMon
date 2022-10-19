using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineSubmeshes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.SetTriangles(mesh.triangles, 0);
    }
}
