using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSource : Entity {
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
        player = null;
        canReplay = true;
    }

    public bool IsPlaying() {
        return handA.activeSelf || handB.activeSelf || canReplay;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            var target = Physics2D.OverlapCircle(transform.position, 100, playerMask);
            if (target != null && target.gameObject != null) {
                player = target.gameObject;
            }
            else {
                handA.SetActive(false);
                handB.SetActive(false);
                canReplay = false;
            }
        }
        if (canReplay) {
            if (player != null) {
                //Debug.Log("hand set active");
                handA.SetActive(true);
                handB.SetActive(true);
            }

            canReplay = false;
        }

        if (player) {
            Vector2 directionToPlayer = (Vector2)player.transform.position - ownRb.position;
            SetDirection(directionToPlayer.normalized);
            handScriptA.SetDirection(directionToPlayer.normalized);
            handScriptB.SetDirection(directionToPlayer.normalized);
            float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            angleToPlayer += 90f;
            ownRb.rotation = angleToPlayer;
        }
    }
}
