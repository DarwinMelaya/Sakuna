using UnityEngine;

public class LindolCollectibleItem : MonoBehaviour
{
    public string itemName = "Object";
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play collect sound
            PlayCollectSound();
            
            // Notify the game manager
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.CollectItem(itemName);
            }
            
            // Destroy this collectible
            Destroy(gameObject);
        }
    }
    
    private void PlayCollectSound()
    {
        // Try to play sound through AudioManager first
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayCollectSound();
        }
    }
}