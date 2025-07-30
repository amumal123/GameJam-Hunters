using UnityEngine;

public class AudioManager : BaseSingleton<AudioManager>
{
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
