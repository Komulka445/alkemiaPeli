using UnityEngine;

public class ClockTimer : MonoBehaviour
{
    // Set the total time for the timer (in seconds)
    public float totalTime = 240f; // 4 minutes (240 seconds)

    // Store the initial and target rotation angles
    private float initialZRotation = 0f;
    private float targetZRotation = -180f; // 6 o'clock position

    private float timer;

    void Start()
    {
        // Initialize the timer
        timer = totalTime;
    }

    void Update()
    {
        if (timer > 0)
        {
            // Decrease the timer over time
            timer -= Time.deltaTime;

            // Calculate the new Z rotation based on the remaining time
            float progress = 1 - (timer / totalTime);
            float currentZRotation = Mathf.Lerp(initialZRotation, targetZRotation, progress);

            // Apply the rotation to the clock hand
            transform.localEulerAngles = new Vector3(0, 0, currentZRotation);
        }
    }
}
