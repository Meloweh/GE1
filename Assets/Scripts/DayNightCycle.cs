using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    private Light2D playerLight;
    private Light2D globalLight;
    public Color dawnColor;
    public Color dayColor;
    public Color duskColor;
    public Color nightColor;
    public float maxIntensity = 1f;
    public float minIntensity = 0.1f;
    public float transitionSpeed = 0.01f;

    public float timeOfDay; // Zwischen 0 und 1, repr�sentiert den Fortschritt des Tages.
    public int dayCount; // Z�hlt die vergangenen Tage.

    private ParticleSystem rain;
    private ParticleSystem ripples;
    private ParticleSystem fireflySystem;
    private GameObject player;


    void Start()
    {
        timeOfDay = 0;
        dayCount = 0;
        player = GameObject.FindWithTag("Player");
        globalLight = GameObject.FindWithTag("Sun").GetComponent<Light2D>();
        fireflySystem = GameObject.FindWithTag("Fireflies").GetComponent<ParticleSystem>();


        if (player != null)
        {
            rain = player.transform.Find("Raindrops").GetComponent<ParticleSystem>();
            ripples = player.transform.Find("Rainripples").GetComponent<ParticleSystem>();
            playerLight = player.transform.Find("PlayerLight").GetComponent<Light2D>();
        }
    }

    void Update()
    {
        // Umrechnung von Realzeit zu Spielzeit. 2 Sekunden entsprechen 0.5 Stunden, also sind 24 Stunden (ein voller Tag) 96 Sekunden.
        timeOfDay += Time.deltaTime / 96f;

        if (globalLight == null)
        {
            globalLight = GameObject.FindWithTag("Sun").GetComponent<Light2D>();
            fireflySystem = GameObject.FindWithTag("Fireflies").GetComponent<ParticleSystem>();
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (player != null) {
            rain = player.transform.Find("Raindrops").GetComponent<ParticleSystem>();
            ripples = player.transform.Find("Rainripples").GetComponent<ParticleSystem>();
            playerLight = player.transform.Find("PlayerLight").GetComponent<Light2D>();
        }

        if (timeOfDay >= 1) // Wenn ein voller Tag vergangen ist.
        {
            dayCount++;
            timeOfDay -= 1;

            if (Random.Range(0f, 1f) < 0.2f)
            {
                if (!rain.isPlaying) rain.Play();
                if (!ripples.isPlaying) ripples.Play();
            }
            else
            {
                if (rain.isPlaying) rain.Stop();
                if (ripples.isPlaying) ripples.Stop();
            }
        }

        // �ndert das globale Licht abh�ngig von der Tageszeit
        if (timeOfDay < 5f / 24f || timeOfDay > 20f / 24f)
        {
            // Nacht
            if (!fireflySystem.isPlaying) fireflySystem.Play();

            globalLight.color = Color.Lerp(globalLight.color, nightColor, transitionSpeed * Time.deltaTime);
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, minIntensity, transitionSpeed * Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, maxIntensity, transitionSpeed * Time.deltaTime);
        }
        else if (timeOfDay < 6f / 24f)
        {
            // Fr�he D�mmerung
            if (fireflySystem.isPlaying) fireflySystem.Stop();
            globalLight.color = Color.Lerp(globalLight.color, dawnColor, transitionSpeed * Time.deltaTime);
        }
        else if (timeOfDay < 8f / 24f)
        {
            // Sp�te D�mmerung
            globalLight.color = Color.Lerp(globalLight.color, dawnColor, transitionSpeed * Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, minIntensity, transitionSpeed * Time.deltaTime);
        }
        else if (timeOfDay < 18f / 24f)
        {
            // Tag
            globalLight.color = Color.Lerp(globalLight.color, dayColor, transitionSpeed * Time.deltaTime);
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, maxIntensity, transitionSpeed * Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 0, transitionSpeed * Time.deltaTime);
        }
        else if (timeOfDay < 19f / 24f)
        {
            // Fr�he D�mmerung
            globalLight.color = Color.Lerp(globalLight.color, duskColor, transitionSpeed * Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, minIntensity, transitionSpeed * Time.deltaTime);
        }
        else if (timeOfDay < 20f / 24f)
        {
            // Sp�te D�mmerung
            globalLight.color = Color.Lerp(globalLight.color, duskColor, transitionSpeed * Time.deltaTime);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, minIntensity, transitionSpeed * Time.deltaTime);
        }
    }

    // Gibt die aktuelle Uhrzeit als String zur�ck, im Format HH:MM.
    public string GetTime()
    {
        return string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timeOfDay * 24), Mathf.FloorToInt((timeOfDay * 24 * 60) % 60));
    }

    // Gibt den aktuellen Tag als Integer zur�ck.
    public int GetDay()
    {
        return dayCount;
    }
}
