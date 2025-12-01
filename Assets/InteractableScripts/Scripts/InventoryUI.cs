using System.Collections.Generic;
using UnityEngine;

namespace EJETAGame.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance;

        [Header("UI References")]
        public Transform itemsParent;
        public GameObject slotPrefab;

        private List<InventorySlot> slots = new List<InventorySlot>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void UpdateUI()
        {
            if (itemsParent == null)
            {
                Debug.LogError("InventoryUI: itemsParent (Content) is not assigned or has been destroyed!");
                return;
            }

            foreach (Transform child in itemsParent)
                if (child != null) Destroy(child.gameObject);

            slots.Clear();

            foreach (Item item in InventoryManager.Inventory.items)
            {
                if (item == null) continue;

                GameObject slot = Instantiate(slotPrefab, itemsParent);
                InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();

                if (inventorySlot != null)
                {
                    inventorySlot.SetItem(item);
                    slots.Add(inventorySlot);
                }
            }
        }
    }
}

