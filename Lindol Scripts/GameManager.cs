using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalItemsToCollect = 3;
    public Text uiText;
    public GameObject winPanel;
    public GameObject failPanel;
    
    [Header("Counter Panel")]
    public GameObject counterPanel;
    public TextMeshProUGUI counterText;
    
    [Header("Instruction Text")]
    public GameObject instructionPanel;
    public TextMeshProUGUI instructionText;
    
    [Header("Timer Panel")]
    public GameObject timerPanel;
    public TextMeshProUGUI timerText;
    public float timeLimit = 60f; // Time limit in seconds
    
    private int itemsCollected = 0;
    private bool gameEnded = false;
    private float timeRemaining;

    void Start()
    {
        // Initialize timer
        timeRemaining = timeLimit;
        
        UpdateUI();
        UpdateTimerUI();
        
        if (winPanel != null) winPanel.SetActive(false);
        if (failPanel != null) failPanel.SetActive(false);
        
        // Show counter panel at start
        if (counterPanel != null) counterPanel.SetActive(true);
        
        // Show timer panel at start
        if (timerPanel != null) timerPanel.SetActive(true);
        
        // Hide instruction text at start
        if (instructionPanel != null) instructionPanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        // Update timer
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            OnTimerExpired();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Format time as MM:SS
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void OnTimerExpired()
    {
        if (gameEnded) return;
        
        gameEnded = true;
        Debug.Log("Time's up! Game Over!");
        
        // Show fail panel when time expires
        if (failPanel != null) failPanel.SetActive(true);
        
        // Hide timer panel on game over
        if (timerPanel != null) timerPanel.SetActive(false);
        
        // Hide counter panel on game over
        if (counterPanel != null) counterPanel.SetActive(false);
        
        // Hide instruction text on game over
        if (instructionPanel != null) instructionPanel.SetActive(false);
        
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Disable player movement
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.enabled = false;
        }
    }

    public void CollectItem(string itemName)
    {
        if (gameEnded) return;
        
        itemsCollected++;
        Debug.Log("Collected: " + itemName + " | Total: " + itemsCollected + "/" + totalItemsToCollect);
        UpdateUI();
    }

    public void PlayerWentUnderTable()
    {
        if (gameEnded) return;
        
        if (itemsCollected >= totalItemsToCollect)
        {
            WinGame();
        }
        else
        {
            FailGame();
        }
    }

    void UpdateUI()
    {
        if (uiText != null)
        {
            uiText.text = "Objects Collected: " + itemsCollected + "/" + totalItemsToCollect + "\nFind all objects and go under the table!";
        }
        
        // Update counter panel text
        if (counterText != null)
        {
            counterText.text = itemsCollected + " / " + totalItemsToCollect;
        }
        
        // Show instruction text when all items are collected
        if (itemsCollected >= totalItemsToCollect)
        {
            if (instructionPanel != null) instructionPanel.SetActive(true);
            if (instructionText != null) instructionText.text = "Go ka sa Ilalim ng table";
        }
        else
        {
            if (instructionPanel != null) instructionPanel.SetActive(false);
        }
    }

    void WinGame()
    {
        gameEnded = true;
        Debug.Log("You Win! All items collected!");
        
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        
        // Hide counter panel on win
        if (counterPanel != null) counterPanel.SetActive(false);
        
        // Hide timer panel on win
        if (timerPanel != null) timerPanel.SetActive(false);
        
        // Hide instruction text on win
        if (instructionPanel != null) instructionPanel.SetActive(false);
        
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Disable player movement
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.enabled = false;
        }
    }

    void FailGame()
    {
        gameEnded = true;
        Debug.Log("You need to collect all objects first!");
        
        if (failPanel != null)
        {
            failPanel.SetActive(true);
        }
        
        // Hide counter panel on fail
        if (counterPanel != null) counterPanel.SetActive(false);
        
        // Hide timer panel on fail
        if (timerPanel != null) timerPanel.SetActive(false);
        
        // Hide instruction text on fail
        if (instructionPanel != null) instructionPanel.SetActive(false);
        
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}