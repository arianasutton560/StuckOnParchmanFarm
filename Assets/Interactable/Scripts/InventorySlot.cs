using UnityEngine;
using UnityEngine.UI;

namespace EJETAGame.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;
        private Item item;

        public void SetItem(Item newItem)
        {
            item = newItem;
            if (icon != null && item.icon != null)
            {
                icon.sprite = item.icon;
                icon.color = Color.white; 
                Debug.Log($"✅ Assigned icon for {item.itemName}: {item.icon.name}");
            }
            else
            {
                Debug.LogWarning($"⚠️ Missing icon or item icon for {newItem.itemName}");
            }
        }

    }
}


