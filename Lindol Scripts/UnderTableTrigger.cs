using UnityEngine;

public class UnderTableTrigger : MonoBehaviour
{
    void Awake()
    {
        // Ensure the collider is set as a trigger so player can pass through
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        else
        {
            Debug.LogWarning("UnderTableTrigger: No Collider component found! Please add a Collider component.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if all items are collected
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.PlayerWentUnderTable();
            }
        }
    }
}