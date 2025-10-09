namespace EJETAGame
{

    using UnityEngine;
    using EJETAGame.Inventory;
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("Door Settings")]
        public bool isLocked = true;
        public Item requiredKey;   // assign the key item from your project
        private bool isOpen = false;

        public void Interact()
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

        private void OpenDoor()
        {
            if (!isOpen)
            {
                Debug.Log("Door opened!");
                // Example: animate or rotate
                transform.Rotate(0, 90, 0);
                //move exit dorr up
                ExitDoor exitDoor = GetComponent<ExitDoor>();
                if (exitDoor != null)
                {
                    transform.position += new Vector3(0, 5, 0);
                }
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
