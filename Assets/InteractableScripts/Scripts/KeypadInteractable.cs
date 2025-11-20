namespace EJETAGame
{
    using UnityEngine;

    public class KeypadInteractable : MonoBehaviour, IInteractable
    {
        [Header("Keypad UI")]
        public GameObject keypadUI;

        [Header("Thought Bubble")]
        public GameObject thoughtBubble;   // assign this in the Inspector

        public void Interact()
        {
            if (keypadUI != null)
            {
                keypadUI.SetActive(true);

                // Unlock cursor for UI interaction
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Debug.Log("Keypad activated.");
            }
        }

        // Used by your UI system if you want dynamic text
        public string GetDescription()
        {
            return "Press E to open locket";
        }

        // 🔹 Called by Interactor when the player first looks at the keypad
        public void OnInteractEnter()
        {
            // Show thought bubble
            if (thoughtBubble != null)
                thoughtBubble.SetActive(true);

            // Show prompt text
            InteractionText.instance.SetText(GetDescription());
            InteractionText.instance.textAppear.gameObject.SetActive(true);
        }

        // 🔹 Called by Interactor when the player stops looking at the keypad
        public void OnInteractExit()
        {
            // Hide thought bubble
            if (thoughtBubble != null)
                thoughtBubble.SetActive(false);

            // Hide prompt text
            InteractionText.instance.textAppear.gameObject.SetActive(false);
        }
    }
}

