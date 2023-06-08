using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Color dayColor = Color.white;
    public Color nightColor = Color.black;
    public float secondsInFullDay = 120f;
    public Light playerLight;
    public Light sunLight;  // Das "Sonnen"-Licht in deiner Szene

    void Update()
    {
        float t = Mathf.PingPong(Time.time / secondsInFullDay, 1);
        sunLight.intensity = Mathf.Lerp(1f, 0f, t);
        sunLight.color = Color.Lerp(dayColor, nightColor, t);

        if (t < 0.5f)
        {
            // Es ist Tag
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 0f, 0.1f);
        }
        else
        {
            // Es ist Nacht
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 1f, 0.1f);
        }
    }
}


