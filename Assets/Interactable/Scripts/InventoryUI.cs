namespace EJETAGame.Inventory
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance;

        public Transform itemsParent;
        public GameObject inventoryPanel;

        private List<InventorySlot> slots = new List<InventorySlot>();

        public void UpdateUI()
        {
            foreach (Transform child in itemsParent)
            {
                Destroy(child.gameObject);
            }
            slots.Clear();

            //Create a new slot for each item in the inventory
            foreach (Item item in InventoryManager.Inventory.items)
            {
                GameObject slot = Instantiate(inventoryPanel, itemsParent);
                InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();

                if (inventorySlot != null)
                {
                    inventorySlot.SetItem(item);
                    slots.Add(inventorySlot);
                }
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        public void refreshInventory(List<Item> items)
        {
            foreach (Transform child in itemsParent)
            {
                Destroy(child.gameObject);
            }

            foreach (Item item in items)
            {
                GameObject slot = Instantiate(inventoryPanel, itemsParent);
                slot.GetComponentInChildren<Text>().text = item.itemName;

                Image iconImage = slot.transform.Find("Icon").GetComponent<Image>();
                if (iconImage != null && item.itemIcon != null)
                {
                    iconImage.sprite = item.itemIcon;
                }
            }
        }
    }
}
