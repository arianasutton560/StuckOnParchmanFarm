namespace EJETAGame.Inventory
{
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Inventory;
        public List<Item> items = new List<Item>();
        public InventoryUI inventoryUI;

        private void Awake()
        {
            if (Inventory == null) Inventory = this;
            else Destroy(gameObject);

        }
        public void AddItem(Item item)
        {
            items.Add(item);
            inventoryUI.UpdateUI();
            Debug.Log("Added item: " + item.itemName);
        }
    }
}
