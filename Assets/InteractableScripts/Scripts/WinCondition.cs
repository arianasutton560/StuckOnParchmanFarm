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
            // Find any required item the player has but has not used yet
            for (int i = 0; i < requiredItems.Length; i++)
            {
                Item item = requiredItems[i];

                // Skip items that are no longer in inventory
                if (!InventoryManager.Inventory.HasItem(item))
                    continue;

                // Use this item
                InventoryManager.Inventory.RemoveItem(item);
                usedItemCount++;

                Debug.Log($"Used {item.itemName} on the Exit Door ({usedItemCount}/{requiredItems.Length})");

                // Update break material
                UpdateVisualStage();

                // If all items have been used → unlock door
                if (usedItemCount >= requiredItems.Length)
                {
                    Debug.Log("All required items used — Calling UnlockDoor()");
                    UnlockDoor();
                }

                return; // Stop after using one matching item
            }

            Debug.Log("You don't have any usable required items.");
        }



        private void UpdateVisualStage()
        {
            if (doorRenderer != null && stageMaterials.Length > 0)
            {
                int stageIndex = Mathf.Clamp(usedItemCount, 0, stageMaterials.Length - 1);
                doorRenderer.material = stageMaterials[stageIndex];
            }
        }

        protected override void OpenDoor()
        {
            if (!isOpen)
            {
                Debug.Log("EXIT DOOR SLIDING DOWN ✅");

                StartCoroutine(SlideDoorDown());
                isOpen = true;
            }
        }

        private IEnumerator SlideDoorDown()
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + new Vector3(0, -3.5f, 0); // adjust drop depth here
            float duration = 1.5f; // how fast the door lowers
            float t = 0f;

            while (t < duration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, t / duration);
                t += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
        }


        private void UnlockDoor()
        {
            isLocked = false;
            Debug.Log(" Exit door fully broken — unlocked!");

            OpenDoor();
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


