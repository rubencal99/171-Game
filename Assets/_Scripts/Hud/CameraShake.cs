using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance {get; private set;}
    private CinemachineVirtualCamera Camera;
    CinemachineBasicMultiChannelPerlin CameraPerlin;
    private float startingIntensity;
    private float startingFrequency;
    private float shakeTimer;
    private float shakeTimerTotal;

    private Vector3 originalCameraPos;

    
    private void Awake()
    {
        Instance = this;
        Camera = GetComponent<CinemachineVirtualCamera>();
        CameraPerlin = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            CameraPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
        }
        else
        {
            this.transform.localPosition = originalCameraPos;

        }
    }

    public void ShakeCamera(float intensity, float frequency, float time)
    {
        // CameraPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        // CameraPerlin.m_FrequencyGain = frequency;
        startingFrequency = frequency;
        shakeTimer = time;
        shakeTimerTotal = time;
        originalCameraPos = this.transform.localPosition;
    }
}
 