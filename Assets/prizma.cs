using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CreatePrism : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();

        // 1. Define the 6 vertices of a triangular prism
        mesh.vertices = new Vector3[] {
            new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0), // Front face
            new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(0.5f, 1, 1)  // Back face
        };

        // 2. Define the triangles (order matters for faces to show correctly!)
        mesh.triangles = new int[] {
            0, 2, 1, // Front
            3, 4, 5, // Back
            0, 3, 2, 2, 3, 5, // Left side
            1, 2, 4, 2, 5, 4, // Right side
            0, 1, 3, 1, 4, 3  // Bottom
        };

        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}