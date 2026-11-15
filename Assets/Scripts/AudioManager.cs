using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource sfxSource;

    void Awake()
    {
        Instance = this; //create easy global access to this script by referencing AudioManager.Instance
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, bool randomizePitch = false)
    {
        if(randomizePitch)
            sfxSource.pitch = Random.Range(0.9f,1.1f);
        else
            sfxSource.pitch = 1f;
        
        sfxSource.PlayOneShot(clip,volume);
    }

}