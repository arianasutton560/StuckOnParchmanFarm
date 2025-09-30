namespace EJETAGame.Interactable
{

    using UnityEngine;
    using TMPro;

    public class Keypad : MonoBehaviour
    {
        [Header("Keypad Settings")]
        public string correctCode = "1234";
        public TMP_Text inputDisplay;
        public Door connectedDoor;  // assign the door this keypad unlocks

        private string currentInput = "";

        public void ButtonPress(string number)
        {
            if (currentInput.Length < correctCode.Length)
            {
                currentInput += number;
                inputDisplay.text = currentInput;
            }
        }

        public void ClearInput()
        {
            currentInput = "";
            inputDisplay.text = "";
        }

        public void SubmitCode()
        {
            if (currentInput == correctCode)
            {
                Debug.Log("Correct Code! Door unlocked.");
                connectedDoor.UnlockFromKeypad(); // custom method in Door
            }
            else
            {
                Debug.Log("Incorrect Code!");
                inputDisplay.text = "WRONG";
                Invoke(nameof(ClearInput), 1.5f);
            }
        }
    }
}

