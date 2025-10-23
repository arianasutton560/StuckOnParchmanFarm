using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EJETAGame.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance;

        [Header("UI References")]
        public Transform itemsParent;      // Parent that holds item slots
        public GameObject slotPrefab;      // Prefab for individual item slot

        private List<InventorySlot> slots = new List<InventorySlot>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
        }

        public void UpdateUI()
        {
            // Clear old slots
            foreach (Transform child in itemsParent)
                Destroy(child.gameObject);
            slots.Clear();

            // Create a new slot for each item
            foreach (Item item in InventoryManager.Inventory.items)
            {
                GameObject slot = Instantiate(slotPrefab, itemsParent);
                InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();

                if (inventorySlot != null)
                {
                    inventorySlot.SetItem(item);
                    slots.Add(inventorySlot);
                }
                else
                {
                    Debug.LogWarning("Slot prefab missing InventorySlot component!");
                }
            }
        }
    }
}
