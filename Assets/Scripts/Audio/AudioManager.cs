using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


    [SerializeField] private float musicVolume = 1;

    private AudioSource music1;
    private AudioSource music2;
    private AudioSource sfxSource;

    private bool isFirstMusicSourceActive;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        music1 = gameObject.AddComponent<AudioSource>();
        music2 = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        music1.loop = true;
        music2.loop = true;
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }
    public void PlaySFX(AudioClip audioClip, float volume)
    {
        sfxSource.PlayOneShot(audioClip, volume);
    }

    public void PlayMusicWithXFade(AudioClip audioClip, float transitionTime = 1f)
    {
        AudioSource activeSource = isFirstMusicSourceActive ? music1 : music2;
        AudioSource newSource = isFirstMusicSourceActive ? music2 : music1;

        isFirstMusicSourceActive = !isFirstMusicSourceActive;

        newSource.clip = audioClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithXFade(activeSource, newSource, audioClip, transitionTime));
    }

    private IEnumerator UpdateMusicWithXFade(AudioSource originalSource, AudioSource newSource, AudioClip music, float transitionTime)
    {
        if (!originalSource.isPlaying)
        {
            originalSource.Play();
        }

        newSource.Stop();
        newSource.clip = music;
        newSource.Play();

        float time = 0;

        for (time = 0; time <= transitionTime; time += Time.deltaTime)
        {
            originalSource.volume = musicVolume - ((time / transitionTime) * musicVolume);
            newSource.volume = (time / transitionTime) * musicVolume;
            yield return null;
        }

        originalSource.volume = 0;
        newSource.volume = musicVolume;
        originalSource.Stop();
    }
}
