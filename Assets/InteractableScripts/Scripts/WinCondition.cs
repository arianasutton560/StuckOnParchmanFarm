using UnityEngine;
using System.Collections;
using EJETAGame.Inventory;

namespace EJETAGame
{
    public class ExitDoor : Door
    {
        [Header("Win Settings")]
        public GameObject winUI;

        [Header("Break Progression (Material-Based)")]
        public Material[] stageMaterials;  // 0 = normal, 4 = destroyed
        public Item[] requiredItems;       // items needed to interact with door
        private int usedItemCount = 0;

        private MeshRenderer doorRenderer;

        private void Start()
        {
            doorRenderer = GetComponent<MeshRenderer>();
            UpdateVisualStage();
        }

        public override void Interact()
        {
            // If already open, ignore
            if (isOpen) return;

            // If still locked, try to use an item
            if (isLocked)
            {
                TryUseRequiredItem();
            }
            else
            {
                // Door is unlocked — just open it
                base.Interact();
            }
        }

        private void TryUseRequiredItem()
        {
            if (usedItemCount < requiredItems.Length)
            {
                Item currentItem = requiredItems[usedItemCount];

                if (InventoryManager.Inventory.HasItem(currentItem))
                {
                    // Use the item
                    InventoryManager.Inventory.RemoveItem(currentItem);
                    usedItemCount++;

                    // Update door visuals
                    UpdateVisualStage();

                    Debug.Log($"Used {currentItem.itemName} on the Exit Door ({usedItemCount}/{requiredItems.Length})");

                    if (usedItemCount >= requiredItems.Length)
                    {
                        UnlockDoor();
                    }
                }
                else
                {
                    Debug.Log($"Missing required item: {currentItem.itemName}");
                }
            }
            else
            {
                Debug.Log("All required items have already been used.");
            }
        }

        private void UpdateVisualStage()
        {
            if (doorRenderer != null && stageMaterials.Length > 0)
            {
                int stageIndex = Mathf.Clamp(usedItemCount, 0, stageMaterials.Length - 1);
                doorRenderer.material = stageMaterials[stageIndex];
            }
        }

        private void UnlockDoor()
        {
            isLocked = false;
            Debug.Log(" Exit door fully broken — unlocked!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isLocked && other.CompareTag("Player"))
            {
                WinGame();
            }
        }

        public void WinGame()
        {
            Debug.Log("Player exited through the door — You Win!");
            StartCoroutine(ShowWinUIDelayed());
        }

        private IEnumerator ShowWinUIDelayed()
        {
            yield return new WaitForSecondsRealtime(2f);

            if (winUI != null)
            {
                winUI.SetActive(true);
                Debug.Log( "Win UI Activated!");
            }
            else
            {
                Debug.LogWarning("Win UI not assigned!");
            }

            Time.timeScale = 0f;
        }
    }
}


