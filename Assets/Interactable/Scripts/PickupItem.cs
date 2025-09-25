namespace EJETAGame.Inventory
{
    using UnityEngine;
    using EJETAGame.Inventory;

    public class NewMonoBehaviourScript : MonoBehaviour, IInteractable
    {
        public Item item;
        
        
        public void Interact()
        {
            Debug.Log("Picked up: " + item.itemName + "from" + gameObject.name);

            if (InventoryManager.Inventory != null)
            {
                InventoryManager.Inventory.AddItem(item);
            }
            else
            {
                Debug.LogWarning("InventoryManager instance is null. Cannot add item.");
            }

            Destroy(gameObject); // remove from world to indicate it's been picked up
            
        }

        public string GetDescription()
        {
            return "Pick up " + item.itemName;
        }
    }
}
