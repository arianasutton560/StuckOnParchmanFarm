using UnityEngine;

public class RatSqueak : MonoBehaviour
{
    public AudioClip squeakSound;
    public float minDelay = 3f;
    public float maxDelay = 8f;

    private AudioSource audioSource;
    private float timer;
    private float nextSqueakTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextSqueakTime = Random.Range(minDelay, maxDelay);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextSqueakTime)
        {
            audioSource.PlayOneShot(squeakSound);
            timer = 0f;
            nextSqueakTime = Random.Range(minDelay, maxDelay);
        }
    }
}
