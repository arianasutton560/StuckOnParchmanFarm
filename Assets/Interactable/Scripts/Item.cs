namespace EJETAGame
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public string itemName;

        public string getItemName()
        {
            return itemName;
        }
        public Sprite itemIcon;

    }
}
