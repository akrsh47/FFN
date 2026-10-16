using UnityEngine;

public class MovingObjectAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip cop;
    public AudioClip auto;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    
    public void PlayAudio(AudioClip clip, float pitch = 1f, float volume = 0.5f)
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopAudio() { 
    
        audioSource.Stop();

    }
}
