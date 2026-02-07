using UnityEngine;

/// <summary>
/// Darkens ambient lighting for a dim indoor atmosphere.
/// Attach to any persistent scene object (e.g. GameManager).
/// </summary>
public class AmbientDarkness : MonoBehaviour
{
    [Header("Ambient Settings")]
    [SerializeField] private Color ambientColor = new Color(0.15f, 0.15f, 0.18f);
    [SerializeField] private float ambientIntensity = 0.5f;

    private void Awake()
    {
        RenderSettings.ambientLight = ambientColor;
        RenderSettings.ambientIntensity = ambientIntensity;
    }
}
