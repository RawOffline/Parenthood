using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    //public AudioClip music1;
    //public AudioClip music2;

    //private AudioSource audioSource;

    public float firstSongDuration;
    public SoundManager.Sound firstSong;
    public SoundManager.Sound secondSong;

    public bool playAdditionalSound;
    public SoundManager.Sound additionalSound;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.volume = 0f;

        //// Play the first music with fade in
        //StartCoroutine(Fade(true, audioSource, 20f, 1f));
        //PlayMusic(music1, false);
        SoundManager.PlaySound(firstSong, true, false);
        StartCoroutine(PlaySecondSong(firstSongDuration));
        if (playAdditionalSound)
        {
            SoundManager.PlaySound(additionalSound, true, true);
        }
    }

    //void Update ()
    //{
    //    if (Input.GetKey(KeyCode.N))
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //}
    IEnumerator PlaySecondSong(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SoundManager.PlaySound(SoundManager.Sound.Level_1_Loop, true, true);
    }

    //void Update()
    //{
    //    // Check if the first music has finished playing
    //    if (!audioSource.isPlaying)
    //    {
    //        PlayMusic(music2, true);
    //    }
    //}

    //void PlayMusic(AudioClip music, bool loop)
    //{
    //    audioSource.clip = music;
    //    audioSource.loop = loop;
    //    audioSource.Play();
    //}

    //public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume)
    //{
    //    if (!fadeIn)
    //    {
    //        print("source.clip.samples: " + source.clip.samples);
    //        print("source.clip.frequency: " + source.clip.frequency);

    //        double lengthOfSource = (double)source.clip.samples / source.clip.frequency;
    //        yield return new WaitForSecondsRealtime((float)(lengthOfSource - duration));
    //    }

    //    float time = 0f;
    //    float startVol = source.volume;
    //    while (time < duration)
    //    {
    //        time += Time.deltaTime;
    //        source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
    //        yield return null;
    //    }

    //    yield break;
    //}
}
