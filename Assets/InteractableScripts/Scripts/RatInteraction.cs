using UnityEngine;
using EJETAGame; 

public class RatInteraction : MonoBehaviour, IInteractable
{
    [Header("Audio")]
    public AudioSource voiceSource;
    public AudioClip ratVoiceClip;

    [Header("Thought Bubble")]
    public GameObject thoughtBubble; 

    public void PlayVoice()
    {
        if (voiceSource != null && ratVoiceClip != null)
        {
            if (!voiceSource.isPlaying)
            {
                voiceSource.PlayOneShot(ratVoiceClip);
            }
        }
        else
        {
            Debug.LogWarning("RatInteraction: Missing voiceSource or ratVoiceClip on " + gameObject.name);
        }
    }

    // Called by Interactor when player press E while looking at the rat
    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name + " (rat)");
        PlayVoice();
    }

    // Called when the ray first hits this object
    public void OnInteractEnter()
    {
        if (thoughtBubble != null)
            thoughtBubble.SetActive(true);

        // Optional: still show your “Press E” text
        InteractionText.instance.SetText("Press E on rat");
        InteractionText.instance.textAppear.gameObject.SetActive(true);
    }

    // Called when you look away
    public void OnInteractExit()
    {
        if (thoughtBubble != null)
            thoughtBubble.SetActive(false);

        InteractionText.instance.textAppear.gameObject.SetActive(false);
    }
}
