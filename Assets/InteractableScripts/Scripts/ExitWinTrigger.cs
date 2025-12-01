using UnityEngine;

namespace EJETAGame
{
    public class ExitWinTrigger : MonoBehaviour
    {
        public ExitDoor exitDoor;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && exitDoor != null && !exitDoor.isLocked)
            {
                exitDoor.WinGame();
            }
        }
    }
}

