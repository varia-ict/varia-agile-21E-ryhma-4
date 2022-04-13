using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();  
        if (inventory != null)
        {
            inventory.DiamondCollected();
            gameObject.SetActive(false);
        }
    }
}
