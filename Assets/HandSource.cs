using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSource : MonoBehaviour {
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject handA, handB;
    private Hand handScriptA, handScriptB;
    private GameObject player;
    private bool canReplay;

    private Rigidbody2D ownRb;
    // Start is called before the first frame update
    void Start() {
        ownRb = GetComponent<Rigidbody2D>();
        handScriptA = handA.GetComponent<Hand>();
        handScriptB = handB.GetComponent<Hand>();

    }

    public void Replay() {
        canReplay = true;
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            var target = Physics2D.OverlapCircle(transform.position, 100, playerMask);
            if (target != null && target.gameObject != null) player = target.gameObject;
        }
        if (canReplay) {
            if (player != null) {
                handA.SetActive(true);
                handB.SetActive(true);
            }

            canReplay = false;
        }

        if (player) {
            Vector2 directionToPlayer = (Vector2)player.transform.position - ownRb.position;
            float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            angleToPlayer += 90f;
            ownRb.rotation = angleToPlayer;
        }
    }
}
