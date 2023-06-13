using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichProjectileSpawner : MonoBehaviour
{
    [SerializeField] private float interval = 5f;

    [SerializeField] private GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (byte i = 0; i < 3; i++) {
            StartCoroutine(ExecuteWithDelay(i));
        }
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
    }
}
