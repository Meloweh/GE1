using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ParticleLight2D : MonoBehaviour {
    public GameObject lightPrefab;
    private ParticleSystem ownParticleSystem;
    private ParticleSystem.Particle[] particles;
    private GameObject[] lights;

    void Start() {
        ownParticleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ownParticleSystem.main.maxParticles];
        lights = new GameObject[ownParticleSystem.main.maxParticles];

        for (int i = 0; i < lights.Length; i++) {
            lights[i] = Instantiate(lightPrefab, Vector3.zero, Quaternion.identity);
            lights[i].SetActive(false);
        }
    }

    void Update() {
        if (particles == null) return;
        var particleCount = ownParticleSystem.GetParticles(particles);
        for (int i = 0; i < lights.Length; i++) {
            if (lights[i] == null) lights[i] = Instantiate(lightPrefab, Vector3.zero, Quaternion.identity);
            if (i < particleCount) {
                lights[i].SetActive(true);
                lights[i].transform.position = particles[i].position;
                Light2D light2D = lights[i].GetComponent<Light2D>();
                light2D.pointLightOuterRadius = particles[i].GetCurrentSize(ownParticleSystem) * 2.5f;
            } else {
                lights[i].SetActive(false);
            }
        }
    }
}