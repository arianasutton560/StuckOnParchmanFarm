namespace EJETAGame
{

    using UnityEngine;
    using System.Collections;

    public class ExitDoor : Door
    {
        public GameObject winUI;

        private void OnTriggerEnter(Collider other)
        {
            if (!isLocked && other.CompareTag("Player"))
            {
                WinGame();
            }
        }

        public void WinGame()
        {
            Debug.Log("Player exited through the door — You Win!");
            StartCoroutine(ShowWinUIDelayed());
        }

        private IEnumerator ShowWinUIDelayed()
        {
            // Wait 5 seconds in real time (ignores Time.timeScale)
            yield return new WaitForSecondsRealtime(5f);

            if (winUI != null)
            {
                winUI.SetActive(true);
                Debug.Log("Win UI Activated after delay!");
            }
            else
            {
                Debug.LogWarning("Win UI not assigned!");
            }

            // Stop game time AFTER showing UI (optional)
            Time.timeScale = 0f;
        }
    }
}
