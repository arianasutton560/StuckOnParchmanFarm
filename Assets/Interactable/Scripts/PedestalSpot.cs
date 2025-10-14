using UnityEngine;
using EJETAGame.Inventory;

namespace EJETAGame.Puzzles
{
    public class PedestalSpot : MonoBehaviour, IInteractable
    {
        [Header("Correct Item")]
        public Item requiredItem;
        public Transform placePosition; // where object will appear when placed
        public GameObject objectPrefab; // visual of the placed item
        private bool isPlaced = false;

        public string GetDescription()
        {
            if (isPlaced)
                return "Something is already placed here.";
            return $"Press E to place {requiredItem}";
        }

        public void Interact()
        {
            if (isPlaced)
            {
                Debug.Log("Pedestal already has an item.");
                return;
            }

            if (InventoryManager.Inventory != null && InventoryManager.Inventory.HasItem(requiredItem))
            {
                PlaceItem();
            }
            else
            {
                Debug.Log("You don't have the correct item to place here.");
            }
        }

        private void PlaceItem()
{
    if (requiredItem == null || placePosition == null)
    {
        Debug.LogError("PedestalSpot: requiredItem or placePosition is not assigned!");
        return;
    }

    if (requiredItem.worldModel != null)
    {
        GameObject placed = Instantiate(requiredItem.worldModel, placePosition.position, placePosition.rotation);
        placed.transform.localScale = requiredItem.worldModel.transform.localScale; // ensure correct scale
    }
    else
    {
        Debug.LogError($"PedestalSpot: {requiredItem.itemName} has no worldModel assigned!");
        return;
    }

    InventoryManager.Inventory.RemoveItem(requiredItem);
    isPlaced = true;
    Debug.Log($"Placed {requiredItem.itemName} on {gameObject.name}!");
}

    }
}
