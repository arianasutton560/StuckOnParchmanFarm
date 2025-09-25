namespace EJETAGame.Inventory
{
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Inventory;
        public List<Item> items = new List<Item>();

        private void Awake()
        {
            if (Inventory == null)Inventory = this;
            else Destroy(gameObject);
            
        }
        public void AddItem(Item item)
        {
            items.Add(item);
            Debug.Log("Added item: " + item.itemName);
        }
    }
}
