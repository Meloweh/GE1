using System.Collections;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private GameObject[] frames;
    [SerializeField] private float frameInterval = 0.5f;  // Time between frames

    private int currentFrameIndex = 0;
    private int lastFrameIndex = 0;

    private void Start()
    {
        // Get all the child game objects
        frames = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            frames[i] = transform.GetChild(i).gameObject;
        }

        // Start the animation coroutine
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            // Deactivate all frames
            foreach (var frame in frames)
            {
                frame.SetActive(false);
            }

            // Generate a random index, excluding the last one
            do
            {
                currentFrameIndex = Random.Range(0, frames.Length);
            } while (currentFrameIndex == lastFrameIndex);

            // Activate the current frame
            frames[currentFrameIndex].SetActive(true);

            // Remember the current frame index for next time
            lastFrameIndex = currentFrameIndex;

            // Wait for the next frame
            yield return new WaitForSeconds(frameInterval);
        }
    }
}