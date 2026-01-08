using UnityEngine;

public class LindolCollectibleItem : MonoBehaviour
{
    public string itemName = "Object";
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify the game manager
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.CollectItem(itemName);
            }
            
            // Destroy this collectible
            Destroy(gameObject);
        }
    }
}