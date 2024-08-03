using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemacineShake : MonoBehaviour
{
    public static CinemacineShake Instance { get; private set; }

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;

    private float timer;
    private float timerMax;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        m_MultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if(timer < timerMax)
        {
            timer += Time.deltaTime;
            float ampitude = Mathf.Lerp(startingIntensity, 0, timer / timerMax);
            m_MultiChannelPerlin.m_AmplitudeGain = ampitude;
        }
    }

    public void ShakeCamera(float intensity, float timerMax)
    {
        this.timerMax = timerMax;
        timer = 0;

        this.startingIntensity = intensity;
        m_MultiChannelPerlin.m_AmplitudeGain = intensity;

    }
}
