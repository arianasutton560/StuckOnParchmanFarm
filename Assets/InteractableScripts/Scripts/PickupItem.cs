namespace EJETAGame.Inventory
{
    using UnityEngine;

    public class NewMonoBehaviourScript : MonoBehaviour, IInteractable
    {
        public Item item;

        [Header("Audio")]
        public AudioClip interactSound;   // assign in Inspector
        public bool loopOnInteract = false; // turn on if you want it to keep playing

        private AudioSource audioSource;

        private void Awake()
        {
            // Ensure there is an AudioSource
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.playOnAwake = false;
            audioSource.loop = loopOnInteract;
            audioSource.spatialBlend = 1f; // 3D sound (set to 0 for 2D)
        }

        public void Interact()
        {
            Debug.Log("Picked up: " + item.itemName + " from " + gameObject.name);

            // Play the sound at this position (spawns a temp AudioSource)
            if (interactSound != null)
                AudioSource.PlayClipAtPoint(interactSound, transform.position, 1f);

            // Add to inventory
            if (InventoryManager.Inventory != null)
                InventoryManager.Inventory.AddItem(item);
            else
                Debug.LogWarning("InventoryManager instance is null. Cannot add item.");

            // Now it’s safe to destroy immediately
            Destroy(gameObject);
        }


        // Optional helper if you used loopOnInteract:
        public void StopInteractSound()
        {
            if (audioSource != null && audioSource.isPlaying) audioSource.Stop();
        }

        public string GetDescription()
        {
            return "Press E to Interact";
        }
    }
}
