using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2D : MonoBehaviour
{
    public static SoundManager2D instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip menuMusic;
    public AudioClip[] gameMusic;
    public AudioClip endGameLoop;

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        } else if (instance !=this)
        {
            Destroy(this);
        }
    }

    public static void PlaySFX(AudioClip clip)
    {
        instance.sfxSource.PlayOneShot(clip);
    }

    public static void PlaySFX(AudioClip clip, float volume)
    {
        instance.sfxSource.volume = volume;
        instance.sfxSource.PlayOneShot(clip);
    }

    public static void SetVolumeSFX(float volume)
    {
        instance.sfxSource.volume = volume;
    }

    public static void SetVolumeMusic(float volume)
    {
        instance.musicSource.volume = volume;
    }

    public static void StartMenuMusic()
    {
         instance.StartCoroutine(instance.MusicTransition(instance.menuMusic, 2.0f));
    }

    public static void StartGameMusic()
    {
        instance.StartCoroutine(instance.MusicTransition(instance.gameMusic[Random.Range(0, instance.gameMusic.Length)], 2.0f));
    }

    public static void StartEndMusic()
    {
        instance.StartCoroutine(instance.MusicTransition(instance.endGameLoop, 2.0f));
    }

    public IEnumerator MusicTransition(AudioClip newClip, float duration)
    {
        float timer = 0.0f;
        float storedMusicVolume = musicSource.volume;
        float actualVolume = musicSource.volume;
        while (timer < (duration/2.0f))
        {
            timer += Time.deltaTime;
            actualVolume = Mathf.MoveTowards(actualVolume, 0.0f, timer / (duration / 2.0f));
            musicSource.volume = actualVolume;
            yield return new WaitForEndOfFrame();

        }
        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
        while (timer < duration)
        {
            timer += Time.deltaTime;
            actualVolume = Mathf.MoveTowards(actualVolume, storedMusicVolume, timer / duration);
            musicSource.volume = actualVolume;
            yield return new WaitForEndOfFrame();
        }
    }

}
