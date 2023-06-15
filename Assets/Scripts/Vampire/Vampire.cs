using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D;
using UnityEngine;

public class Vampire : EntityHostile
{
 
    public GameObject batPrefab;  
    public Vector3 spawnPosition;

    protected override void OnDie()
    {
        Debug.Log("Hallo");
            GetAnimator().SetTrigger("isDying");
            Invoke(nameof(DestroySelf), 0);
            Instantiate(batPrefab, transform.position, Quaternion.identity);
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
