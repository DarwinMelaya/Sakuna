using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Applies professional game UI styling at runtime.
/// Add to your main Canvas to enhance Timer, Score, Game Over panel, and buttons.
/// </summary>
[RequireComponent(typeof(Canvas))]
public class GameUIEnhancer : MonoBehaviour
{
    [Header("Text Styling")]
    public Color timerColor = new Color(0.95f, 0.95f, 1f, 1f);
    public Color scoreColor = new Color(0.4f, 0.9f, 0.5f, 1f);
    public int timerFontSize = 28;
    public int scoreFontSize = 24;
    public bool addTextOutline = true;
    public Color outlineColor = new Color(0.05f, 0.05f, 0.1f, 1f);
    public float outlineWidth = 0.2f;

    [Header("Game Over Panel")]
    public Color overlayColor = new Color(0.02f, 0.02f, 0.05f, 0.92f);
    public Color gameOverTextColor = new Color(1f, 0.35f, 0.35f, 1f);
    public Color winTextColor = new Color(0.3f, 0.95f, 0.5f, 1f);

    [Header("Button Styling")]
    public Color buttonNormalColor = new Color(0.2f, 0.55f, 0.9f, 1f);
    public Color buttonHighlightColor = new Color(0.3f, 0.65f, 1f, 1f);
    public Color buttonTextColor = Color.white;

    [Header("References (Auto-find if empty)")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button[] buttons;

    void Start()
    {
        ApplyStyling();
    }

    [ContextMenu("Apply UI Styling")]
    public void ApplyStyling()
    {
        // Find timer text if not assigned
        if (timerText == null)
        {
            var allTmp = FindObjectsOfType<TextMeshProUGUI>();
            foreach (var t in allTmp)
                if (t.name.Contains("Timer")) { timerText = t; break; }
        }
        if (timerText != null)
        {
            timerText.color = timerColor;
            timerText.fontSize = timerFontSize;
            if (addTextOutline)
            {
                timerText.outlineColor = outlineColor;
                timerText.outlineWidth = outlineWidth;
            }
        }

        // Find score/item counter if not assigned
        if (scoreText == null)
        {
            var itemUI = FindObjectOfType<ItemUI>();
            if (itemUI != null && itemUI.itemText != null)
                scoreText = itemUI.itemText;
        }
        if (scoreText != null)
        {
            scoreText.color = scoreColor;
            scoreText.fontSize = scoreFontSize;
            if (addTextOutline)
            {
                scoreText.outlineColor = outlineColor;
                scoreText.outlineWidth = outlineWidth;
            }
        }

        // Style game over panel overlay
        if (gameOverPanel == null)
            gameOverPanel = GameObject.Find("GameOverPanel");
        if (gameOverPanel != null)
        {
            var overlayImage = gameOverPanel.GetComponent<Image>();
            if (overlayImage != null)
            {
                overlayImage.color = overlayColor;
                overlayImage.raycastTarget = true;
            }

            // Style game over text
            if (gameOverText == null)
            {
                var tmp = gameOverPanel.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null && tmp.text.ToUpper().Contains("GAME"))
                    gameOverText = tmp;
            }
            if (gameOverText != null && addTextOutline)
            {
                gameOverText.outlineColor = outlineColor;
                gameOverText.outlineWidth = 0.25f;
            }
        }

        // Style buttons
        if (buttons == null || buttons.Length == 0)
            buttons = FindObjectsOfType<Button>();
        foreach (var btn in buttons)
        {
            if (btn == null) continue;
            var colors = btn.colors;
            colors.normalColor = buttonNormalColor;
            colors.highlightedColor = buttonHighlightColor;
            colors.pressedColor = Color.Lerp(buttonNormalColor, Color.black, 0.2f);
            btn.colors = colors;

            var btnImage = btn.GetComponent<Image>();
            if (btnImage != null)
                btnImage.color = buttonNormalColor;

            var btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null)
            {
                btnText.color = buttonTextColor;
                if (addTextOutline)
                {
                    btnText.outlineColor = outlineColor;
                    btnText.outlineWidth = outlineWidth;
                }
            }
        }
    }

}
