using UnityEngine;
using TMPro;

public class ProceedToNextLevelText : MonoBehaviour
{
    [SerializeField] Door door;
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        if (door == null) door = FindObjectOfType<Door>();
        textMesh = GetComponent<TextMeshProUGUI>();
        if (textMesh != null) textMesh.enabled = false;
    }

    private void Update()
    {
        if (door != null && door.IsFullyOpen && textMesh != null && !textMesh.enabled)
            textMesh.enabled = true;
    }
}
