using UnityEngine;
using EJETAGame.Inventory;

namespace EJETAGame
{
    public class MoveItem : MonoBehaviour, IInteractable
    {
        [Header("Move Item Settings")]
        public float moveDistance = 2f;
        public float moveSpeed = 2f;

        [Header("Audio (One-Shot)")]
        public AudioClip interactSound;        // plays once when movement starts
        public Vector2 pitchRange = new Vector2(0.95f, 1.05f);

        private bool isMoved = false;
        private bool isMoving = false;
        private Vector3 startPos;
        private Vector3 targetPos;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (!audioSource) audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.spatialBlend = 1f; // set to 0 for 2D
        }

        private void Start()
        {
            startPos = transform.position;
            targetPos = startPos + Vector3.right * moveDistance; // moves along +X
        }

        public void Interact()
        {
            if (!isMoved && !isMoving)
            {
                if (interactSound)
                {
                    audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
                    audioSource.PlayOneShot(interactSound);
                }

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


