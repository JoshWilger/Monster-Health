using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public TextMeshProUGUI itemsCollectedText;
    protected int totalItemsCollected;

    private void Start()
    {
        if (itemsCollectedText != null)
        {
            itemsCollectedText.text = totalItemsCollected.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Collectable") && other.enabled)
        {
            AudioManager.instance.PlayCoinEvent();
            UpdateCollection(other);
        }
    }

    protected void UpdateCollection(Collider other)
    {
        totalItemsCollected++;
        if (itemsCollectedText != null)
        {
            itemsCollectedText.text = totalItemsCollected.ToString();
        }
        other.gameObject.SetActive(false);
        Debug.Log($"{other.gameObject.name} collected: {totalItemsCollected}");
    }
}
