using UnityEngine;

public class RandomAnimationController : MonoBehaviour
{
    private Animator animator;
    public float minSwitchInterval = 5f; // Minimum time before switching animations
    public float maxSwitchInterval = 10f; // Maximum time before switching animations
    private float timer;
    private bool isAnimation1Playing = true;

    private void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Start the default animation (animation1)
        animator.SetBool("Animation1", true);

        // Set the initial random switch interval
        timer = Random.Range(minSwitchInterval, maxSwitchInterval);
    }

    private void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // Check if it's time to switch animations
        if (timer <= 0f)
        {
            // Toggle between animations
            isAnimation1Playing = !isAnimation1Playing;

            // Set the appropriate bool parameter in the Animator controller
            animator.SetBool("Animation1", isAnimation1Playing);
            animator.SetBool("Animation2", !isAnimation1Playing);

            // Set a new random switch interval
            timer = Random.Range(minSwitchInterval, maxSwitchInterval);
        }
    }
}