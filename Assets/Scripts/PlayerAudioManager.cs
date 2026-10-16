using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip hitting;
    public AudioClip hit;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }


    public void PlayAudio(AudioClip clip,float pitch=1f,float volume=0.5f)
    {
        audioSource.pitch=pitch;
        audioSource.volume=volume;
        audioSource.PlayOneShot(clip);
    }
}
