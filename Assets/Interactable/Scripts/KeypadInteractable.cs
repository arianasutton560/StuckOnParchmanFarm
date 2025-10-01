namespace EJETAGame
{

    using UnityEngine;

    public class KeypadInteractable : MonoBehaviour, IInteractable
    {
        public GameObject keypadUI;

        public void Interact()
        {
            if (keypadUI != null)
            {
                keypadUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Debug.Log("Keypad activated.");
            }
        }

        public string GetDescription()
        {
            return "Press [E] to use Keypad";
        }
    }
}
