using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichProjectile : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;  // Object to circle around
    [SerializeField] private float speed = 5f, speed2 = 8f;   // Speed of rotation
    [SerializeField] private float amplitude = 0.1f; // Amplitude of the sine wave
    [SerializeField] private float radiusA = 0.2f, radiusB = 0.4f; // Distance between objects
    [SerializeField] private float magnitude = 0.1f;
    [SerializeField] private float tracerStart = 2.5f;
    [SerializeField] private float despawnTime = 8f;
    [SerializeField] private float traceSpeed = 1f;
    [SerializeField] private LayerMask playerMask;
    private Vector3 target;
    private float tracerStartTimer;
    private Rigidbody2D ownRb;
    private GameObject player;

    private void Remove() {
        Destroy(gameObject);
    }

    // The original position of the object
    private void Start() {
        tracerStartTimer = 0f;
        target = targetObj.transform.position;
        Invoke(nameof(Remove), despawnTime);
        ownRb = GetComponent<Rigidbody2D>();
        var t = Physics2D.OverlapCircle(transform.position, 100, playerMask);
        if (t != null && t.gameObject != null) {
            player = t.gameObject;
        }
        else {
            player = targetObj;
        }
    }
    
    private Vector3 GetDirectionTo(Vector2 source, Vector2 target) {
        Vector2 position1 = source;
        Vector2 position2 = target;
        Vector2 difference = position2 - position1;
        Vector2 direction = difference.normalized;
        return direction;
    }

    private void Update() {
        tracerStartTimer += Time.deltaTime;
        CircleAroundTarget();
        if (tracerStartTimer > tracerStart) {
            //transform.position += GetDirectionTo(player.transform.position) * 4;
            //amplitude = 0.5f;
            //radiusA = 0.5f;
            //radiusB = 0.8f;
            if (Vector2.Distance(target, player.transform.position) > 0.5f) {
                target += GetDirectionTo(target, player.transform.position) * 0.01f;
            }
        }
    }

    private void CircleAroundTarget()
    {
        // Calculate the new position of the moving object
        float x = target.x + radiusA * Mathf.Cos(Time.time * speed) + Random.Range(-1f, 1f) * magnitude;
        float y = target.y + radiusB * Mathf.Sin(Time.time * speed2) + amplitude * Mathf.Sin(Time.time);
        transform.position = new Vector2(x, y);
        
    }
}