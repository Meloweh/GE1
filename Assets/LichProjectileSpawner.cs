using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichProjectileSpawner : MonoBehaviour
{
    [SerializeField] private float interval = 5f;

    [SerializeField] private GameObject projectilePrefab;

    private byte spawnedAmount;

    private readonly byte spawnTargetAmount = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Replay() {
        spawnedAmount = 0;
        for (byte i = 0; i < spawnTargetAmount; i++) {
            StartCoroutine(ExecuteWithDelay(i));
        }
    }

    public bool IsPlaying() {
        return spawnedAmount >= spawnTargetAmount;
    }

    // Update is called once per frame
    void Update()
    {
        /*for (byte i = 0; i < count; i++) {
            StartCoroutine(ExecuteWithDelay(i));
        }*/
    }
    
    private IEnumerator ExecuteWithDelay(byte delay)
    {
        yield return new WaitForSeconds(delay * interval);
        Instantiate(projectilePrefab, transform.position + new Vector3(-1 + delay, 1, 0), Quaternion.identity);
        spawnedAmount++;
    }
}
