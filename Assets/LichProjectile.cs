using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichProjectile : Entity
{
    [SerializeField] private GameObject targetObj;  // Object to circle around
    [SerializeField] private float speed = 5f, speed2 = 8f;   // Speed of rotation
    [SerializeField] private float amplitude = 0.1f; // Amplitude of the sine wave
    [SerializeField] private float radiusA = 0.2f, radiusB = 0.4f; // Distance between objects
    [SerializeField] private float magnitude = 0.1f;
    [SerializeField] private float tracerStart = 2.5f;
    [SerializeField] private float despawnTime = 8f;
    [SerializeField] private LayerMask playerMask;
    private Vector3 target;
    private float tracerStartTimer;
    private Rigidbody2D ownRb;
    private GameObject player;

    private void Remove() {
        Destroy(gameObject);
    }

    private void Start() {
        tracerStartTimer = 0f;
        target = transform.position;//targetObj.transform.position;
        Invoke(nameof(Remove), despawnTime);
        ownRb = GetComponent<Rigidbody2D>();
        var t = Physics2D.OverlapCircle(transform.position, 100, playerMask);
        if (t != null && t.gameObject != null) {
            player = t.gameObject;
        }
        else {
            Debug.Log("No player nearby");
            player = targetObj;
        }
    }

    private void Update() {
        tracerStartTimer += Time.deltaTime;
        CircleAroundTarget();
        if (target == null) return;
        var playerPos = player != null ? player.transform.position : target;
        var dir = GetDirectionTo(target, playerPos);
        if (tracerStartTimer > tracerStart) {
            if (Vector2.Distance(target, playerPos) > 0.5f) {
                target += dir * Time.deltaTime * 3.5f;
            }
        }
        SetDirection(dir);
    }

    private void CircleAroundTarget() {
        float x = target.x + radiusA * Mathf.Cos(Time.time * speed) + Random.Range(-1f, 1f) * magnitude;
        float y = target.y + radiusB * Mathf.Sin(Time.time * speed2) + amplitude * Mathf.Sin(Time.time);
        transform.position = new Vector2(x, y);
        
    }
}