namespace EJETAGame.Inventory
{
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Inventory;
        public List<Item> items = new List<Item>();
        public InventoryUI inventoryUI;

        public bool HasItem(Item item){return items.Contains(item);}

        public void RemoveItem(Item item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                inventoryUI.UpdateUI();
                Debug.Log("Removed item: " + item.itemName);
            }
        }


        private void Awake()
        {
            if (Inventory == null) Inventory = this;
            //else Destroy(gameObject);
            else DontDestroyOnLoad(gameObject);

        }
        public void AddItem(Item item)
        {
            items.Add(item);
            inventoryUI.UpdateUI();
            Debug.Log("Added item: " + item.itemName);
        }
    }
}
