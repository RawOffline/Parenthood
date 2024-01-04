using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public static class SoundManager
{
    private static Queue<AudioSource> sources;
    private static Transform parent;
    private static bool initialized;

    public enum Sound
    {
        Call,
        Idle1,
        Idle2,
        Idle3,
        ParentJump,
        ParentLadding,
        Level_1,
        Level_1_Loop,
        Level_2,
        Level_2_Loop,
        Level_3,
        Level_3_Loop,
        Level_credit,
        Level_credit_Loop,
        creditSceneWind,
        CutsceneWind,
        Cutscene_SoundAudio_level_1,
        Cutscene_SoundAudio_level_3,
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeMe()
    {
        if (initialized) return;
        initialized = true;
        sources = new();
        GameObject parentGO = new GameObject("AudioParent");

        parent = parentGO.transform;
        Object.DontDestroyOnLoad(parentGO);
        for (int i = 0; i < 10; i++)
        {
            GameObject newAudioGO = new GameObject("Instatiated Audio Source");
            var source = newAudioGO.AddComponent<AudioSource>();
            sources.Enqueue(source);
            newAudioGO.transform.SetParent(parent);
        }
        Object.DontDestroyOnLoad(parent.gameObject);
    }

    public static void PlaySound(Sound sound, bool isMusic, bool loop)
    {
        AudioSource audioSource = GetAvaliableSource();
        if (isMusic)
        {
            audioSource.outputAudioMixerGroup = (Resources.Load("AudioMixer") as AudioMixer).FindMatchingGroups("Music")[0];
            audioSource.loop = loop;
            audioSource.clip = GetAudioClip(sound);
            audioSource.Play();
        }
        else
        {
            audioSource.outputAudioMixerGroup = (Resources.Load("AudioMixer") as AudioMixer).FindMatchingGroups("SFX")[0];
            audioSource.PlayOneShot(GetAudioClip(sound));
        }

    }

    private static AudioSource GetAvaliableSource()
    {
        var s = sources.Dequeue();
        sources.Enqueue(s);
        return s;
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
    public static void StopAllSounds()
    {
        foreach (var audioSource in sources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
