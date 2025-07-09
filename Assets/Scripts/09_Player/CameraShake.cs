using Unity.Cinemachine;
using UnityEngine;
using System.Collections;


public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCam;
    [SerializeField] private float shakeIntensity = 2f;
    [SerializeField] private float shakeTime = 0.5f;

    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        StartCoroutine(FindNoiseComponentDelayed());
    }

    private IEnumerator FindNoiseComponentDelayed()
    {
        yield return new WaitForSeconds(6f);
        
        if (cinemachineCam != null)
        {
            noise = cinemachineCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise != null)
                Debug.Log("Noise component found after 6 seconds");
            else
                Debug.LogWarning("Noise component not found!");
        }
    }

    public void ShakeCamera()
    {
        if (noise != null)
        {
            StartCoroutine(DoShake());
        }
    }

    private IEnumerator DoShake()
    {
        noise.AmplitudeGain = shakeIntensity;
        yield return new WaitForSeconds(shakeTime);
        noise.AmplitudeGain = 0f;
    }
}
