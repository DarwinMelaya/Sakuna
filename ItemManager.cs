using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public int totalItems;
    private int collectedItems = 0;

    public Door door;
    public ItemUI itemUI;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        itemUI.UpdateCounter(collectedItems, totalItems);
    }

    public void CollectItem()
    {
        collectedItems++;

        itemUI.UpdateCounter(collectedItems, totalItems);

        if (collectedItems >= totalItems)
        {
            door.OpenDoor();
        }
    }
}
