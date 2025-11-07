using UnityEngine;
using EJETAGame.Inventory;

namespace EJETAGame
{
    public class MoveSack : MonoBehaviour, IInteractable
    {
        [Header("Move Sack Settings")]
        public float moveDistance = 2f;
        public float moveSpeed = 2f;

        [Header("Audio")]
        public AudioClip interactSound;          // plays once when you press E
        private AudioSource audioSource;

        private bool isMoved = false;
        private Vector3 startPos;
        private Vector3 targetPos;
        private bool isMoving = false;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (!audioSource) audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.spatialBlend = 1f; // 3D sound (set to 0 for 2D)
        }

        private void Start()
        {
            startPos = transform.position;
            targetPos = startPos + Vector3.forward * moveDistance;
        }

        public void Interact()
        {
            if (!isMoved && !isMoving)
            {
                if (interactSound) audioSource.PlayOneShot(interactSound);
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
        }
    }
}

