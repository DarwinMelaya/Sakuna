using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalItemsToCollect = 3;
    public Text uiText;
    public GameObject winPanel;
    public GameObject failPanel;
    
    private int itemsCollected = 0;
    private bool gameEnded = false;

    void Start()
    {
        UpdateUI();
        
        if (winPanel != null) winPanel.SetActive(false);
        if (failPanel != null) failPanel.SetActive(false);
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
    }

    void WinGame()
    {
        gameEnded = true;
        Debug.Log("You Win! All items collected!");
        
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        
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