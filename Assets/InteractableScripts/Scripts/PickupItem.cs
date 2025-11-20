namespace EJETAGame.Inventory
{
    using UnityEngine;
    using EJETAGame; 

    public class NewMonoBehaviourScript : MonoBehaviour, IInteractable
    {
        public Item item;

        [Header("Thought Bubble")]
        public GameObject thoughtBubble;   

        [Header("Audio")]
        public AudioClip interactSound;   
        public bool loopOnInteract = false;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.playOnAwake = false;
            audioSource.loop = loopOnInteract;
            audioSource.spatialBlend = 1f; 

            if (thoughtBubble != null)
                thoughtBubble.SetActive(false);
        }

        // Called when player presses E while looking at this item
        public void Interact()
        {
            Debug.Log("Picked up: " + item.itemName + " from " + gameObject.name);

            // Play the interact sound at this position
            if (interactSound != null)
                AudioSource.PlayClipAtPoint(interactSound, transform.position, 1f);

            if (InventoryManager.Inventory != null)
                InventoryManager.Inventory.AddItem(item);
            else
                Debug.LogWarning("InventoryManager instance is null. Cannot add item.");

            if (thoughtBubble != null)
                thoughtBubble.SetActive(false);

            if (InteractionText.instance != null)
                InteractionText.instance.textAppear.gameObject.SetActive(false);

            Destroy(gameObject);
        }

        public void StopInteractSound()
        {
            if (audioSource != null && audioSource.isPlaying)
                audioSource.Stop();
        }

        public string GetDescription()
        {
            return "Press E to pick up " + item.itemName;
        }

        public void OnInteractEnter()
        {
            if (thoughtBubble != null)
                thoughtBubble.SetActive(true);

            if (InteractionText.instance != null)
            {
                InteractionText.instance.SetText(GetDescription());
                InteractionText.instance.textAppear.gameObject.SetActive(true);
            }
        }

        public void OnInteractExit()
        {
            if (thoughtBubble != null)
                thoughtBubble.SetActive(false);

            if (InteractionText.instance != null)
                InteractionText.instance.textAppear.gameObject.SetActive(false);
        }
    }
}

