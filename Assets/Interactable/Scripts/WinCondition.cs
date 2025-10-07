namespace EJETAGame
{

    using UnityEngine;

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

        private void WinGame()
        {
            Debug.Log("Player exited through the door — You Win!");
            if (winUI != null)
                winUI.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}
