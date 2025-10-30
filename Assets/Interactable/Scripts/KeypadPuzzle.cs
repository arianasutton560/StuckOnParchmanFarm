namespace EJETAGame.Interactable
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class Keypad : MonoBehaviour
    {
        [Header("Keypad Settings")]
        public string correctCode = "1940";
        public TextMeshProUGUI inputDisplay;
        public Door connectedDoor;  // assign the door this keypad unlocks
        public GameObject keypadUI;
        public PlayerController playerController;

        [Header("UI Buttons")]
        public Button[] keypadButtons;

        private string currentInput = "";
        private bool isLockedOut = false; 

        void Update()
        {
            if (keypadUI.activeSelf && Input.GetKeyDown(KeyCode.Return)) { SubmitCode(); }

            //Press Escape to close keypad
            if (Input.GetKeyDown(KeyCode.Escape)) { CloseKeypad(); }
        }
        
        void Start()
        {
            if (connectedDoor == null)
            {
                connectedDoor = GetComponent<Door>();
            }
        }

        public void ButtonPress(string number)
        {
            if (isLockedOut) return;
            currentInput += number;
            inputDisplay.text = currentInput;
        }

        public void ClearInput()
        {
            if (isLockedOut) return; 
            currentInput = "";
            inputDisplay.text = "";
        }

        public void SubmitCode()
        {
            if (isLockedOut) return;

            if (currentInput.Trim() == correctCode)
            {
                Debug.Log("Correct Code! Door unlocked.");
                connectedDoor.UnlockFromKeypad(); // custom method in Door
                inputDisplay.text = "UNLOCKED";
                DisableKeypad();

                GetComponent<Collider>().enabled = false; // disable further interaction
            }
            else
            {
                Debug.Log("Incorrect Code!");
                inputDisplay.text = "WRONG";
                ClearInput();
            }
            
        }

        public void OpenKeypad()
        {
            keypadUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerController.enabled = false;
        }

        public void CloseKeypad()
        {
            keypadUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerController.enabled = true;
        }

        private void DisableKeypad()
        {
            isLockedOut = true;

            foreach (Button btn in keypadButtons)
            {
                btn.interactable = false; // makes all buttons unclickable
            }
        }
    }
}


