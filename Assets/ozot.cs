using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CreatePyramid : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();

        // 1. Define the 5 vertices (Base 4 + Top 1)
        mesh.vertices = new Vector3[] {
            new Vector3(-1, 0, -1), // 0: Bottom Front Left
            new Vector3(1, 0, -1),  // 1: Bottom Front Right
            new Vector3(1, 0, 1),   // 2: Bottom Back Right
            new Vector3(-1, 0, 1),  // 3: Bottom Back Left
            new Vector3(0, 2, 0)    // 4: The Top Point
        };

        // 2. Define the triangles (3 points per face)
        mesh.triangles = new int[] {
            0, 4, 1, // Front Face
            1, 4, 2, // Right Face
            2, 4, 3, // Back Face
            3, 4, 0, // Left Face
            0, 1, 2, 0, 2, 3 // Bottom Base (two triangles)
        };

        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}