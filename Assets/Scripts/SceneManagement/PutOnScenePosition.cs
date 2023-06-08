using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PutOnScenePosition : MonoBehaviour
{
    public string previousScene;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        GameObject startPoint;
        if (previousScene == "Village")
        {
            startPoint = GameObject.Find("StartPoint (Village)");
        }
        else if (previousScene == "Dragon Pit")
        {
            startPoint = GameObject.Find("StartPoint (Dragon Pit)");
        }
        else if (previousScene == "Monster Lair")
        {
            startPoint = GameObject.Find("StartPoint (Monster Lair)");
        }
        else
        {
            startPoint = null;
        }

        if (startPoint != null)
        {
            transform.position = startPoint.transform.position;
        }

        GameObject worldBorders = GameObject.Find("WorldBorders");
        Collider2D worldBordersCollider = null;
        if (worldBorders != null)
        {
            worldBordersCollider = worldBorders.GetComponent<Collider2D>();
            if (worldBordersCollider == null)
            {
                Debug.LogWarning("No Collider2D found in WorldBorders");
            }
        }
        else
        {
            Debug.LogWarning("No WorldBorders found in scene");
        }

        previousScene = SceneManager.GetActiveScene().name;
        GameManager.Instance?.RespawnCamera(worldBordersCollider);
    }
}

