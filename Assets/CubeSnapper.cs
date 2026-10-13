using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSnapper : MonoBehaviour
{
    [Header("Settings")]
    public Transform[] snapTargets;      
    public float snapThreshold = 1.0f;
    public bool isCorrect = false;

    [Header("Fake 3D Effect")]
    public GameObject fake3DChild; 
    public int flatTargetIndex = 0; 

    [Header("Audio")]
    public AudioClip snapSound; // NEW: Slot for your sound file

    private Vector3 offset;
    private float zCoord;
    private bool isSnapped = false;

    void Start()
    {
        if (snapTargets != null && snapTargets.Length >= 2)
        {
            SnapTo(snapTargets[1], 1);
        }
    }

    void OnMouseDown()
    {
        isSnapped = false;
        isCorrect = false; 
        if (fake3DChild != null) fake3DChild.SetActive(true);

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

        if (fake3DChild != null)
        {
            fake3DChild.SetActive(index != flatTargetIndex);
        }

        if (ummmm.Instance != null) ummmm.Instance.CheckWinCondition();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mousePoint = new Vector3(mousePosition.x, mousePosition.y, zCoord);
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}