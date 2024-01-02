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
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeMe()
    {
        if (initialized) return;
        initialized = true;
        sources = new();
        GameObject parentGO = new GameObject("AudioParent");

        parent = parentGO.transform;
        for (int i = 0; i < 10; i++)
        {
            GameObject newAudioGO = new GameObject("Instatiated Audio Source");
            var source = newAudioGO.AddComponent<AudioSource>();
            sources.Enqueue(source);
            newAudioGO.transform.SetParent(parent);
        }
        Object.DontDestroyOnLoad(parent.gameObject);
    }

    public static void PlaySound(Sound sound)
    {
        AudioSource aS = GetAvaliableSource();
        aS.outputAudioMixerGroup = (Resources.Load("AudioMixer") as AudioMixer).FindMatchingGroups("SFX")[0];
        aS.PlayOneShot(GetAudioClip(sound));
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
}
