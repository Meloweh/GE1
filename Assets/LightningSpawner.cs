using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour {
    [SerializeField] private GameObject lightningPrefab;
    [SerializeField] private byte interval = 1;
    [SerializeField] private LayerMask playerMask;
    
    private byte spawnedAmount;

    private readonly byte spawnTargetAmount = 5;
    
    public void Replay() {
        spawnedAmount = 0;
        for (byte i = 0; i < spawnTargetAmount; i++) {
            StartCoroutine(ExecuteWithDelay(i));
        }
    }

    public bool IsPlaying() {
        return spawnedAmount >= spawnTargetAmount;
    }

    private IEnumerator SpawnWithDelay(float delay) {
        var target = Physics2D.OverlapCircle(transform.position, 100, playerMask);
        if (target != null) {
            Vector3 oldPlayerPos = target.gameObject.transform.position;
            yield return new WaitForSeconds(delay * interval);
            Instantiate(lightningPrefab, oldPlayerPos, Quaternion.identity);
            spawnedAmount++;
        }
        else {
            Debug.LogWarning("Player missing");
        }
    }
    private IEnumerator ExecuteWithDelay(float delay) {
        yield return new WaitForSeconds(delay * interval);
        StartCoroutine(SpawnWithDelay(0.5f));
    }

    void Start() {
        for (byte i = 0; i < spawnTargetAmount; i++) {
            StartCoroutine(ExecuteWithDelay(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
