namespace EJETAGame.Inventory
{
    using UnityEngine;

    public class NewMonoBehaviourScript : MonoBehaviour
    {
        public Item item;

        void OnTriggerEnter(Collider other)
        {
            InventoryManager inv = other.GetComponent<InventoryManager>();
            if (inv != null)
            {
                inv.AddItem(item);
                Destroy(gameObject); // remove from world to indicate it's been picked up
            }
        }
    }
}
