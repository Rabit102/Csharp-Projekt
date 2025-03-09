using UnityEngine;

public class AudioCode : MonoBehaviour
{

    public static AudioCode instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
