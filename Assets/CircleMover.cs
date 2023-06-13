using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMover : MonoBehaviour
{
    public GameObject target;  // Object to circle around
    public float speed = 5f, speed2 = 8f;   // Speed of rotation
    public float amplitude = 0.1f; // Amplitude of the sine wave
    public float radiusA = 0.2f, radiusB = 0.4f; // Distance between objects
    public float magnitude = 0.1f;
    
    // The original position of the object
    private Vector3 originalPosition;
    private void Start()
    {
        // Calculate the initial distance between objects
        //radiusA = Vector2.Distance(transform.position, target.transform.position);
    }

    private void Update()
    {
        CircleAroundTarget();
    }

    private void CircleAroundTarget()
    {
        // Calculate the new position of the moving object
        float x = target.transform.position.x + radiusA * Mathf.Cos(Time.time * speed) + Random.Range(-1f, 1f) * magnitude;
        float y = target.transform.position.y + radiusB * Mathf.Sin(Time.time * speed2) + amplitude * Mathf.Sin(Time.time);
        transform.position = new Vector2(x, y);
        
    }
}