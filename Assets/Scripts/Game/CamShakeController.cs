using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShakeController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera)
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    public IEnumerator Shake(float shakeDuration, float shakeAmplitude, float shakeFrequency)
    {
        if (virtualCamera != null || virtualCameraNoise != null)
        {
            virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
            virtualCameraNoise.m_FrequencyGain = shakeFrequency;
            yield return new WaitForSeconds(shakeDuration);
        }
        virtualCameraNoise.m_AmplitudeGain = 0f;
    }

    public void ShakeAtController(float shakeDuration, float shakeAmplitude, float shakeFrequency)
    {
        StartCoroutine(Shake(shakeDuration, shakeAmplitude, shakeFrequency));
    }

}
