using UnityEngine;

/// <summary>
/// Pinapanatili ang crosshair sa gitna ng screen. Gamitin ang crosshair para mag-aim at mag-collect ng items, hindi ang cursor.
/// Attach to the Crosshair UI element.
/// </summary>
public class CrosshairFollowCursor : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas rootCanvas;
    private RectTransform rootRect;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rootCanvas = GetComponentInParent<Canvas>()?.rootCanvas;
        rootRect = rootCanvas != null ? rootCanvas.transform as RectTransform : null;
    }

    void Update()
    {
        if (rectTransform == null || rootRect == null) return;

        // Crosshair sa gitna ng screen - hindi sumusunod sa cursor
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rootRect,
            screenCenter,
            rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera,
            out localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }
}
