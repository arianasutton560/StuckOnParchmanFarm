using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EJETAGame.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Inventory;
        public List<Item> items = new List<Item>();

        public InventoryUI inventoryUI;

        private void Awake()
        {
            if (Inventory != null && Inventory != this)
            {
                Destroy(gameObject);
                return;
            }

            Inventory = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            inventoryUI = Object.FindFirstObjectByType<InventoryUI>(FindObjectsInactive.Include);

            if (inventoryUI != null)
                inventoryUI.UpdateUI();
        }

        public bool HasItem(Item item) => items.Contains(item);

        public void AddItem(Item item)
        {
            items.Add(item);

            if (inventoryUI == null)
                inventoryUI = InventoryUI.Instance ?? Object.FindFirstObjectByType<InventoryUI>(FindObjectsInactive.Include);

            if (inventoryUI != null)
                inventoryUI.UpdateUI();

            Debug.Log("Added item: " + item.itemName);
        }

        public void RemoveItem(Item item)
        {
            if (!items.Contains(item)) return;

            items.Remove(item);

            if (inventoryUI == null)
                inventoryUI = InventoryUI.Instance ?? Object.FindFirstObjectByType<InventoryUI>(FindObjectsInactive.Include);

            if (inventoryUI != null)
                inventoryUI.UpdateUI();

            Debug.Log("Removed item: " + item.itemName);
        }
    }
}


