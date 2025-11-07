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
        public Door connectedDoor;
        public GameObject keypadUI;
        public PlayerController playerController;

        [Header("UI Buttons")]
        public Button[] keypadButtons;

        [Header("Audio")]
        public AudioClip successClip;   // plays when correct
        public AudioClip failClip;      // optional: plays when wrong
        public float sfxVolume = 1f;

        private string currentInput = "";
        private bool isLockedOut = false;

        // internal
        private AudioSource _audio;

        void Awake()
        {
            _audio = GetComponent<AudioSource>();
            if (_audio == null) _audio = gameObject.AddComponent<AudioSource>();
            _audio.playOnAwake = false;
            _audio.loop = false;
            _audio.spatialBlend = 0f; // keypad UI sound = 2D
            _audio.volume = sfxVolume;
        }

        void Start()
        {
            if (connectedDoor == null) connectedDoor = GetComponent<Door>();
        }

        void Update()
        {
            if (keypadUI.activeSelf && Input.GetKeyDown(KeyCode.Return)) SubmitCode();
            if (Input.GetKeyDown(KeyCode.Escape)) CloseKeypad();
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
                if (successClip != null) _audio.PlayOneShot(successClip, sfxVolume);

                Debug.Log("Correct Code! Door unlocked.");
                connectedDoor.UnlockFromKeypad();
                inputDisplay.text = "UNLOCKED";
                DisableKeypad();
                GetComponent<Collider>().enabled = false; // stop further interaction
            }
            else
            {
                if (failClip != null) _audio.PlayOneShot(failClip, sfxVolume);

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
            foreach (Button btn in keypadButtons) btn.interactable = false;
        }
    }
}



