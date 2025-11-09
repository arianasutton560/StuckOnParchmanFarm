using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicTracks; // assign 2 or more tracks in Inspector
    private int currentTrackIndex = 0;

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        PlayNextTrack();
    }

    private void Update()
    {
        // When the current track stops playing → play the next one
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (musicTracks.Length == 0) return;

        // Pick next track in list
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;

        audioSource.clip = musicTracks[currentTrackIndex];
        audioSource.Play();
    }
}
