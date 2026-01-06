using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemManager.instance.CollectItem();
            Destroy(gameObject);
        }
    }
}
