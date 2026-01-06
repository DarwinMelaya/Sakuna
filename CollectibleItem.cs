using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayCollectSound();
        }
        else
        {
            Debug.LogWarning("AudioManager instance is null! Make sure AudioManager exists in the scene.");
        }

        if (ItemManager.instance != null)
        {
            ItemManager.instance.CollectItem();
        }
        else
        {
            Debug.LogWarning("ItemManager instance is null! Make sure ItemManager exists in the scene.");
        }

        Destroy(gameObject);
    }
}
