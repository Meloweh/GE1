using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public Light2D sunPrefab;
    public ParticleSystem firefliesPrefab;

    public static GameManager Instance { get; private set; }

    private CinemachineVirtualCamera virtualCamera;
    private GameObject player;
    private Light2D sunLight;
    private ParticleSystem fireflies;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SpawnPlayerAndCamera();
    }

    private void SpawnPlayerAndCamera()
    {
        sunLight = Instantiate(sunPrefab);
        fireflies = Instantiate(firefliesPrefab);

        player = Instantiate(playerPrefab);
        var camera = Instantiate(cameraPrefab);
        virtualCamera = camera.GetComponent<CinemachineVirtualCamera>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;

            var confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
            {
                GameObject worldBorders = GameObject.Find("WorldBorders");
                if (worldBorders != null)
                {
                    var collider = worldBorders.GetComponent<Collider2D>();
                    if (collider != null)
                    {
                        confiner.m_BoundingShape2D = collider;
                    }
                    else
                    {
                        Debug.LogWarning("No Collider2D found in WorldBorders");
                    }
                }
                else
                {
                    Debug.LogWarning("No WorldBorders found in scene");
                }
            }
        }

        MovePlayerToStartPoint();
    }

    public void RespawnCamera(Collider2D worldBordersCollider)
    {
        // Destroy the existing camera
        if (virtualCamera != null)
        {
            Destroy(virtualCamera.gameObject);
        }

        // Spawn a new camera
        var camera = Instantiate(cameraPrefab);
        virtualCamera = camera.GetComponent<CinemachineVirtualCamera>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;

            // Setup Cinemachine Confiner
            var confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner != null && worldBordersCollider != null)
            {
                confiner.m_BoundingShape2D = worldBordersCollider;
            }
            else
            {
                Debug.LogWarning("No Collider2D found in WorldBorders or Confiner not found on Virtual Camera");
            }
        }
    }



    private void MovePlayerToStartPoint()
    {
        GameObject startPoint = GameObject.Find("StartPoint (Game Start)");
        if (startPoint != null)
        {
            player.transform.position = startPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("No StartPoint found in scene " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}

