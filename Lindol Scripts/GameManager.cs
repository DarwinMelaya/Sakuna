using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Scene Mode")]
    [Tooltip("When active scene name matches this, Go Bag game logic runs. Otherwise Lindol logic runs.")]
    public string goBagSceneName = "Go Bag";

    [Header("Lindol Game Settings")]
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

    [Header("Go Bag Game Settings")]
    public float gameTime = 60f;
    public int totalItemsNeeded = 5;
    public UIManager uiManager;

    // Lindol state
    private int itemsCollected = 0;
    private bool gameEnded = false;
    private float timeRemaining;

    // Go Bag state (only used when in Go Bag scene)
    private int goBagItemsCollected = 0;
    private int correctItemsCollected = 0;
    private bool goBagGameActive = true;
    private List<string> collectedItemNames = new List<string>();
    private float goBagTimeRemaining;

    private bool IsGoBagScene => SceneManager.GetActiveScene().name == goBagSceneName;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (IsGoBagScene)
        {
            goBagTimeRemaining = gameTime;
            goBagGameActive = true;
            goBagItemsCollected = 0;
            correctItemsCollected = 0;
            collectedItemNames.Clear();
            UpdateGoBagUI();
            return;
        }

        // Lindol: Initialize timer
        timeRemaining = timeLimit;

        UpdateUI();
        UpdateTimerUI();

        if (winPanel != null) winPanel.SetActive(false);
        if (failPanel != null) failPanel.SetActive(false);

        if (counterPanel != null) counterPanel.SetActive(true);
        if (timerPanel != null) timerPanel.SetActive(true);
        if (instructionPanel != null) instructionPanel.SetActive(false);
    }

    void Update()
    {
        if (IsGoBagScene)
        {
            if (!goBagGameActive) return;
            goBagTimeRemaining -= Time.deltaTime;
            if (goBagTimeRemaining <= 0)
            {
                goBagTimeRemaining = 0;
                GoBagGameOver(false);
            }
            UpdateGoBagUI();
            return;
        }

        if (gameEnded) return;

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
        if (IsGoBagScene) return;
        if (gameEnded) return;

        itemsCollected++;
        Debug.Log("Collected: " + itemName + " | Total: " + itemsCollected + "/" + totalItemsToCollect);
        UpdateUI();
    }

    /// <summary>Used by Go Bag scene only. Call from CollectibleItem when using ItemData.</summary>
    public void CollectItem(ItemData item)
    {
        if (!IsGoBagScene) return;
        if (!goBagGameActive) return;

        goBagItemsCollected++;
        collectedItemNames.Add(item.itemName);

        if (item.isNeededInBag)
        {
            correctItemsCollected++;
            if (uiManager != null)
                uiManager.ShowFeedback("✓ " + item.itemName + " - NEEDED!", true);
            if (correctItemsCollected >= totalItemsNeeded)
                GoBagGameOver(true);
        }
        else
        {
            goBagTimeRemaining -= 5f;
            if (goBagTimeRemaining < 0) goBagTimeRemaining = 0;
            if (uiManager != null)
                uiManager.ShowFeedback("✗ " + item.itemName + " - Not needed (-5 sec)", false);
        }
        UpdateGoBagUI();
    }

    void UpdateGoBagUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateTimer(goBagTimeRemaining);
            uiManager.UpdateScore(correctItemsCollected, totalItemsNeeded);
        }
    }

    void GoBagGameOver(bool playerWon)
    {
        goBagGameActive = false;
        if (uiManager != null)
            uiManager.ShowGameOver(playerWon ? "YOU WIN! Go Bag Ready!" : "TIME'S UP! Try Again!", playerWon);
    }

    public void PlayerWentUnderTable()
    {
        if (IsGoBagScene) return;
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

    /// <summary>Go Bag: load Scene 3 when player finishes (wins) the game.</summary>
    public void GoToScene3()
    {
        SceneManager.LoadScene(3);
    }
}