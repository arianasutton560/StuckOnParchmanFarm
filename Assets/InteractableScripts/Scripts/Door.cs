namespace EJETAGame
{

    using UnityEngine;
    using EJETAGame.Inventory;
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("Door Settings")]
        public bool isLocked = true;
        public Item requiredKey;   // assign the key item from your project
        protected bool isOpen = false;

        public virtual void Interact()
        {
            if (isLocked)
            {
                if (InventoryManager.Inventory.HasItem(requiredKey))
                {
                    Debug.Log("Unlocked door with: " + requiredKey.itemName);
                    InventoryManager.Inventory.RemoveItem(requiredKey);
                    isLocked = false;
                    OpenDoor();
                }
                else
                {
                    Debug.Log("Door is locked. Requires: " + requiredKey.itemName);
                }
            }
            else
            {
                OpenDoor();
            }
        }

        public void UnlockFromKeypad()
        {
            isLocked = false;
            OpenDoor();
        }

        protected virtual void OpenDoor()
        {
            if (!isOpen)
            {
                Debug.Log("Door opened!");
                // simple open animation
                transform.Rotate(0, 90, 0);
                isOpen = true;
            }
        }


        public string GetDescription()
        {
            if (isLocked)
                return "Locked Door (Needs " + requiredKey.itemName + ")";
            else if (!isOpen)
                return "Press E to Open Door";
            else
                return "Door is already open";
        }
    }
}
