using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public int frameRange = 60;

    private float[] frameTimes;
    private int frameTimeIndex = 0;

    void Start()
    {
        frameTimes = new float[frameRange];
    }

    void Update()
    {
        float frameTime = Time.unscaledDeltaTime;

        frameTimes[frameTimeIndex] = frameTime;
        frameTimeIndex = (frameTimeIndex + 1) % frameRange;

        float averageFrameTime = 0f;
        foreach (float time in frameTimes)
        {
            averageFrameTime += time;
        }
        averageFrameTime /= frameRange;

        float fps = 1.0f / averageFrameTime;
        fpsText.text = "FPS: " + Mathf.Round(fps);
    }
}