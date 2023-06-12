using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ParticleLight2D : MonoBehaviour {
    public GameObject lightPrefab;
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private GameObject[] lights;

    void Start() {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        lights = new GameObject[particleSystem.main.maxParticles];

        for (int i = 0; i < lights.Length; i++) {
            lights[i] = Instantiate(lightPrefab, Vector3.zero, Quaternion.identity);
            lights[i].SetActive(false);
        }
    }

    void Update() {
        int particleCount = particleSystem.GetParticles(particles);

        for (int i = 0; i < lights.Length; i++) {
            if (i < particleCount) {
                lights[i].SetActive(true);
                lights[i].transform.position = particles[i].position;
                Light2D light2D = lights[i].GetComponent<Light2D>();
                light2D.pointLightOuterRadius = particles[i].GetCurrentSize(particleSystem) * 2.5f;
            } else {
                lights[i].SetActive(false);
            }
        }
    }
}