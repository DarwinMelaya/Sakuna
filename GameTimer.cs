using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    private bool gameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
        UpdateTimerUI();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // hide cursor
    }

    void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GameOver();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining);
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // stop game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; // show cursor
    }
}
