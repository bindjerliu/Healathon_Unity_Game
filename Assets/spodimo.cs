using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class spodimo : MonoBehaviour
{
    [Header("Settings")]
    public Transform[] snapTargets;      
    public float snapThreshold = 1.0f;
    public bool isCorrect = false;

    [Header("Audio")]
    public AudioClip snapSound; // NEW: Slot for your sound file

    private Vector3 offset;
    private float zCoord;
    private bool isSnapped = false;

    void Start()
    {
        GeneratePyramidMesh();

        if (snapTargets != null && snapTargets.Length >= 2)
        {
            SnapTo(snapTargets[1], 1);
        }
    }

    void GeneratePyramidMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "GeneratedPyramid";
        mesh.vertices = new Vector3[] {
            new Vector3(-1, 0, -1), new Vector3(1, 0, -1),
            new Vector3(1, 0, 1), new Vector3(-1, 0, 1),
            new Vector3(0, 2, 0)
        };
        mesh.triangles = new int[] { 0, 4, 1, 1, 4, 2, 2, 4, 3, 3, 4, 0, 0, 1, 2, 0, 2, 3 };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        GetComponent<MeshFilter>().mesh = mesh;
        MeshCollider mc = GetComponent<MeshCollider>();
        mc.sharedMesh = mesh;
        mc.convex = true; 
    }

    void OnMouseDown()
    {
        isSnapped = false;
        isCorrect = false; 
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        if (!isSnapped)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        CheckForSnapping();
    }

    private void CheckForSnapping()
    {
        if (snapTargets == null || snapTargets.Length == 0) return;

        Transform bestTarget = null;
        float closestDistance = float.MaxValue;
        int bestIndex = -1;

        for (int i = 0; i < snapTargets.Length; i++)
        {
            if (snapTargets[i] == null) continue;
            float distance = Vector3.Distance(transform.position, snapTargets[i].position);

            if (distance <= snapThreshold && distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = snapTargets[i];
                bestIndex = i;
            }
        }

        if (bestTarget != null)
        {
            SnapTo(bestTarget, bestIndex);
        }
    }

    private void SnapTo(Transform target, int index)
    {
        if (target == null) return;
        transform.position = target.position;
        transform.rotation = target.rotation;
        
        // NEW: Play the sound if we just snapped AND it wasn't already snapped
        if (!isSnapped && snapSound != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(snapSound, Camera.main.transform.position);
        }

        isSnapped = true;
        isCorrect = (index == 0);

        if (ummmm.Instance != null) ummmm.Instance.CheckWinCondition();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mousePoint = new Vector3(mousePosition.x, mousePosition.y, zCoord);
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}