using System.Collections;
using UnityEngine;

public class Lightning : Entity
{
    private GameObject[] frames;
    [SerializeField] private float frameInterval = 0.5f;  // Time between frames
    [SerializeField] private LayerMask playerMask;

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
        Destroy(gameObject, 0.7f);
        
        var target = Physics2D.OverlapCircle(transform.position, 10, playerMask);
        if (target != null) {
            var dir = GetDirectionTo(transform.position, target.gameObject.transform.position);
            SetDirection(dir);
        }
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