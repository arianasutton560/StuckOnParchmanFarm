namespace EJETAGame.Interactable
{

    using UnityEngine;
    using TMPro;

    public class Keypad : MonoBehaviour
    {
        [Header("Keypad Settings")]
        public string correctCode = "1234";
        public TextMeshProUGUI inputDisplay;
        public Door connectedDoor;  // assign the door this keypad unlocks

        private string currentInput = "";
        public GameObject keypadUI;

        void Update()
        {
            if (keypadUI.activeSelf && Input.GetKeyDown(KeyCode.Return)) { SubmitCode(); }

            //Press Escape to close keypad
            if (Input.GetKeyDown(KeyCode.Escape)) { CloseKeypad(); }
        }
        
        void Start()
        {
            if(connectedDoor == null)
            {
                connectedDoor = GetComponent<Door>();
            }
        }

        public void ButtonPress(string number)
        {
           
            currentInput += number;
            inputDisplay.text = currentInput;
            
        }

        public void ClearInput()
        {
            currentInput = "";
            inputDisplay.text = "";
        }

        public void SubmitCode()
        {
            if (currentInput.Trim() == correctCode)
            {
                Debug.Log("Correct Code! Door unlocked.");
                connectedDoor.UnlockFromKeypad(); // custom method in Door
            }
            else
            {
                Debug.Log("Incorrect Code!");
                inputDisplay.text = "WRONG";
                ClearInput();
            }
        }
        public PlayerController playerController;
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

    }
}

