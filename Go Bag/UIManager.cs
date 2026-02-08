using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI feedbackText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button quitButton;

    private float feedbackTimer = 0f;

    void Start()
    {
        gameOverPanel.SetActive(false);
        feedbackText.text = "";

        // Connect buttons
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        quitButton.onClick.AddListener(() => GameManager.Instance.QuitGame());
    }

    void Update()
    {
        // Auto-hide feedback after 2 seconds
        if (feedbackTimer > 0)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0)
            {
                feedbackText.text = "";
            }
        }
    }

    public void UpdateTimer(float timeRemaining)
    {
        if (timerText == null) return;
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

        // Warning color when time is low
        if (timeRemaining < 10)
        {
            timerText.color = Color.red;
        }
    }

    public void UpdateScore(int current, int total)
    {
        scoreText.text = string.Format("Items: {0}/{1}", current, total);
    }

    public void ShowFeedback(string message, bool isCorrect)
    {
        feedbackText.text = message;
        feedbackText.color = isCorrect ? Color.green : Color.red;
        feedbackTimer = 2f;
    }

    public void ShowGameOver(string message, bool won)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = message;
        gameOverText.color = won ? Color.green : Color.red;

        // Ipakita ang cursor para ma-click ang Restart/Quit buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}