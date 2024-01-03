using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip music1;
    public AudioClip music2;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;

        // Play the first music with fade in
        StartCoroutine(Fade(true, audioSource, 20f, 1f));
        PlayMusic(music1, false);
    }

    void Update()
    {
        // Check if the first music has finished playing
        if (!audioSource.isPlaying)
        {
            PlayMusic(music2, true);
        }
    }

    void PlayMusic(AudioClip music, bool loop)
    {
        
        audioSource.clip = music;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume)
    {
        if (!fadeIn)
        {
            print("source.clip.samples: " + source.clip.samples);
            print("source.clip.frequency: " + source.clip.frequency);

            double lengthOfSource = (double)source.clip.samples / source.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(lengthOfSource - duration));
        }

        float time = 0f;
        float startVol = source.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            yield return null;
        }

        yield break;
    }
}
