using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistOnSceneLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
