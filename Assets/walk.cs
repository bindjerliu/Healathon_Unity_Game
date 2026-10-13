using UnityEngine;
using System.Collections;

public class NPCIntroSequence : MonoBehaviour
{
    [Header("Settings")]
    public Transform playerTransform;   // Drag your Player object here in the Inspector
    public Vector3 targetPosition;      // The spot 1/3 into the scene
    public float moveSpeed = 2.5f;      // How fast the NPC walks
    public float waveDuration = 5.0f;   // How long the NPC waves

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        // Safety check to make sure everything is assigned
        if (playerTransform == null)
        {
            Debug.LogError("NPCIntro: Please drag your Player object into the 'Player Transform' slot in the Inspector!");
            return;
        }

        // Start the intro logic
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        Debug.Log("Intro Sequence: Starting Walk...");

        // 1. START WALKING
        // Make sure "isWalking" matches your Animator Parameter name exactly
        anim.SetBool("isWalking", true);

        // Loop until we are close to the target
        while (Vector3.Distance(transform.position, targetPosition) > 0.2f)
        {
            // Move the NPC position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Make the NPC look at where they are going
            Vector3 lookDir = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            transform.LookAt(lookDir);

            yield return null; // Wait for the next frame
        }

        // 2. STOP WALKING & TURN TO PLAYER
        Debug.Log("Intro Sequence: Target Reached. Turning to face user...");
        anim.SetBool("isWalking", false);

        // Turn to face the player smoothly
        Vector3 playerPos = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(playerPos);

        // 3. START WAVING
        Debug.Log("Intro Sequence: Waving...");
        anim.SetBool("isWaving", true);

        // Wait for the duration you set (5 seconds)
        yield return new WaitForSeconds(waveDuration);

        // 4. STOP WAVING & START GAME
        anim.SetBool("isWaving", false);
        Debug.Log("Intro Sequence: Finished! The player can now begin.");
        
        StartGame();
    }

    void StartGame()
    {
        // This is where you would enable your Player Controller script 
        // to let the user start moving.
    }
    
    // This helps you see the target position in the Scene View
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(targetPosition, 0.5f);
    }
}