using UnityEngine;
using EJETAGame.Inventory;

namespace EJETAGame.Puzzles
{
    public class PedestalSpot : MonoBehaviour, IInteractable
    {
        [Header("Correct Item")]
        public Item requiredItem;
        public Transform placePosition; // where object will appear when placed
        public GameObject objectPrefab; // visual model of the pedestal (optional)
        private bool isPlaced = false;

        [Header("Physics Settings")]
        public float itemFallForce = 1f;
        public float pedestalFallDelay = 0.5f; // time before pedestal starts to fall

        public string GetDescription()
        {
            if (isPlaced)
                return "Something is already placed here.";
            return $"Press E to place {requiredItem.itemName}";
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
                // ✅ Spawn the item
                GameObject placed = Instantiate(requiredItem.worldModel, placePosition.position, placePosition.rotation);
                placed.transform.localScale = requiredItem.worldModel.transform.localScale;

                // ✅ Add Rigidbody so it can fall
                Rigidbody itemRb = placed.GetComponent<Rigidbody>();
                if (itemRb == null)
                    itemRb = placed.AddComponent<Rigidbody>();

                itemRb.useGravity = true;
                itemRb.isKinematic = false;
                itemRb.AddForce(Vector3.up * itemFallForce, ForceMode.Impulse);

                // ✅ Remove from inventory
                InventoryManager.Inventory.RemoveItem(requiredItem);
                isPlaced = true;

                // ✅ Make the pedestal fall
                StartCoroutine(FallAfterDelay());

                Debug.Log($"Placed {requiredItem.itemName} and pedestal is falling!");
            }
            else
            {
                Debug.LogError($"PedestalSpot: {requiredItem.itemName} has no worldModel assigned!");
                return;
            }
        }

        private System.Collections.IEnumerator FallAfterDelay()
        {
            yield return new WaitForSeconds(pedestalFallDelay);

            Rigidbody pedestalRb = GetComponent<Rigidbody>();
            if (pedestalRb == null)
                pedestalRb = gameObject.AddComponent<Rigidbody>();

            pedestalRb.useGravity = true;
            pedestalRb.isKinematic = false;

            pedestalRb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);
        }
    }
}


