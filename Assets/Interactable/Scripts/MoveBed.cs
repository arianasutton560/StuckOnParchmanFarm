using UnityEngine;
using EJETAGame.Inventory;

namespace EJETAGame
{
    public class MoveItem : MonoBehaviour, IInteractable
    {
        [Header("Move Item Settings")]
        public float moveDistance = 2f;  // how far to move (in meters)
        public float moveSpeed = 2f;     // how fast to move
        private bool isMoved = false;
        private Vector3 startPos;
        private Vector3 targetPos;
        private bool isMoving = false;

        private void Start()
        {
            startPos = transform.position;
            targetPos = startPos + Vector3.right * moveDistance;
        }

        public void Interact()
        {
            if (!isMoved && !isMoving)
            {
                Debug.Log("Moving item forward...");
                StartCoroutine(MoveToPosition(targetPos));
                isMoved = true;
            }
        }

        private System.Collections.IEnumerator MoveToPosition(Vector3 target)
        {
            isMoving = true;
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = target;
            isMoving = false;
            Debug.Log("Item finished moving!");
        }
    }
}

