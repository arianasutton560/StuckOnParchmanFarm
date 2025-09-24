namespace EJETAGame.Inventory
{
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryManager : MonoBehaviour
    {
        public List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
            Debug.Log("Added item: " + item.itemName);
        }
    }
}
