using UnityEngine;

/// <summary>
/// Makes a Light component flicker/blink to simulate unstable lighting (e.g. during earthquake).
/// </summary>
[RequireComponent(typeof(Light))]
public class BlinkingLight : MonoBehaviour
{
    [Header("Blink Settings")]
    [SerializeField] private float minIntensity = 0.1f;
    [SerializeField] private float maxIntensity = 1f;
    [SerializeField] private float blinkSpeedMin = 2f;
    [SerializeField] private float blinkSpeedMax = 8f;
    [SerializeField] private bool randomizePhase = true;

    private Light _light;
    private float _baseIntensity;
    private float _timer;
    private float _blinkSpeed;

    private void Awake()
    {
        _light = GetComponent<Light>();
        _baseIntensity = _light.intensity;
        if (randomizePhase) _timer = Random.Range(0f, 10f);
        _blinkSpeed = Random.Range(blinkSpeedMin, blinkSpeedMax);
    }

    private void Update()
    {
        _timer += Time.deltaTime * _blinkSpeed;
        float t = (Mathf.Sin(_timer) + 1f) * 0.5f;
        t = t * t; // Smoother curve
        _light.intensity = Mathf.Lerp(minIntensity * _baseIntensity, maxIntensity * _baseIntensity, t);
    }
}
