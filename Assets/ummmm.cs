using UnityEngine;

public class ummmm : MonoBehaviour
{
    public static ummmm Instance; 

    [Header("UI Settings")]
    public GameObject winScreenUI; 

    [Header("Level Progress")]
    public GameObject[] levelObjects; 

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (winScreenUI != null) winScreenUI.SetActive(false);
    }

    public void CheckWinCondition()
    {
        if (levelObjects == null || levelObjects.Length == 0) return;

        int correctCount = 0;

        foreach (GameObject obj in levelObjects)
        {
            if (obj == null) continue;

            CubeSnapper cube = obj.GetComponent<CubeSnapper>();
            spodimo pyramid = obj.GetComponent<spodimo>();

            // NEW: If the manager can't find EITHER script, it will tell you!
            if (cube == null && pyramid == null)
            {
                Debug.LogWarning("WARNING: " + obj.name + " does not have a CubeSnapper or spodimo script! Is this the wrong object, or is your script named DraggableCube?");
            }

            if (cube != null && cube.isCorrect) correctCount++;
            if (pyramid != null && pyramid.isCorrect) correctCount++;
        }

        Debug.Log("Progress: " + correctCount + " / " + levelObjects.Length);

        if (correctCount >= levelObjects.Length && levelObjects.Length > 0)
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        if (winScreenUI != null)
        {
            winScreenUI.SetActive(true);
            Debug.Log("LEVEL COMPLETE!");
        }
    }
}