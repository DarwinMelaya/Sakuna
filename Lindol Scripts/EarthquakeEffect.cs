using UnityEngine;

public class EarthquakeEffect : MonoBehaviour
{
    public float shakeMagnitude = 0.1f;

    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
        InvokeRepeating("Shake", 0f, 0.02f);
    }

    void Shake()
    {
        transform.position = originalPos + Random.insideUnitSphere * shakeMagnitude;
    }

    public void StopShake()
    {
        CancelInvoke();
        transform.position = originalPos;
    }
}
