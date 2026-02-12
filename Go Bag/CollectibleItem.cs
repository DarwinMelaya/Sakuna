using UnityEngine;
using UnityEngine.InputSystem;

public class GoBagCollectibleItem : MonoBehaviour
{
    public ItemData itemData;
    private bool isCollected = false;
    private Material originalMaterial;
    private Renderer itemRenderer;
    private Collider itemCollider;

    void Start()
    {
        itemRenderer = GetComponentInChildren<Renderer>();
        if (itemRenderer != null)
        {
            originalMaterial = itemRenderer.material;
        }

        itemCollider = GetComponent<Collider>();
        if (itemCollider == null)
        {
            itemCollider = GetComponentInChildren<Collider>();
        }
        if (itemCollider == null)
        {
            var box = gameObject.AddComponent<BoxCollider>();
            itemCollider = box;
        }
    }

    void Update()
    {
        // New Input System: OnMouseDown doesn't fire when only new Input is active, so detect click + raycast here
        if (isCollected) return;
        var mouse = Mouse.current;
        if (mouse == null || !mouse.leftButton.wasPressedThisFrame) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        // Gamitin ang crosshair (gitna ng screen), hindi ang cursor
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = cam.ScreenPointToRay(screenCenter);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)) return;

        // Payagan ang kahit aling collider sa loob ng object hierarchy
        var hitItem = hit.collider.GetComponentInParent<GoBagCollectibleItem>();
        if (hitItem != this) return;

        CollectItem();
    }

    void OnMouseEnter()
    {
        if (!isCollected && itemRenderer != null)
        {
            itemRenderer.material.color = Color.yellow;
        }
    }

    void OnMouseExit()
    {
        if (!isCollected && itemRenderer != null)
        {
            itemRenderer.material.color = Color.white;
        }
    }

    void CollectItem()
    {
        if (itemData == null)
        {
            Debug.LogWarning("GoBagCollectibleItem on " + gameObject.name + " has no ItemData assigned.");
            return;
        }
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager.Instance is null. Is a GameManager in the scene?");
            return;
        }

        isCollected = true;
        GameManager.Instance.CollectItem(itemData);
        gameObject.SetActive(false);
        Debug.Log("Collected: " + itemData.itemName);
    }
}