using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemText;

    public void UpdateCounter(int collected, int total)
    {
        itemText.text = "Items: " + collected + " / " + total;
    }
}
