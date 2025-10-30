using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EJETAGame.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;
        public TMP_Text nametext;
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

            if (nametext != null)
            {
                nametext.text = item.getItemName();
            }
        }
        
        public void ClearSlot()
        {
            item = null;
            if (icon != null)
            {
                icon.sprite = null;
                icon.color = Color.clear;
            }
            if (nametext != null)
            {
                nametext.text = "";
            }
        }

    }
}


