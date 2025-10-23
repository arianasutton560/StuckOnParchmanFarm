namespace EJETAGame.Inventory
{
    //using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class InventorySlot : MonoBehaviour
    {
        public Image icon;
        //public TextMeshProUGUI countText;

        private Item item;

        public void SetItem(Item newItem)
        {
            item = newItem;
            icon.sprite = newItem.itemIcon;  // Your item should have a sprite reference
            icon.enabled = true;
           // countText.text = "1"; // or item.amount if you track quantity
        }

        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
           // countText.text = "";
        }
    }
}
