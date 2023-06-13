using System.Collections;
using UnityEngine;

public class LichProjectile : MonoBehaviour
{
    // Define the magnitude of vibration
    public float magnitude = 0.1f;
    
    // The original position of the object
    private Vector3 originalPosition;

    void Start()
    {
        // Save the original position
        originalPosition = transform.position;
        TriggerVibration();
    }

    public void TriggerVibration()
    {
        // Start the vibration coroutine
        StartCoroutine(Vibrate());
    }

    IEnumerator Vibrate() {
        while (true) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            yield return null;
        }
    }
}