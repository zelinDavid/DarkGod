using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AudioSvc : MonoBehaviour {
    public static AudioSvc Instance = null;
    public AudioSource bgAudio;
    public AudioSource uiAudio;

    public void InitService() {
        Instance = this;
        Debug.Log("Init AudioSvc");
    }

    public void PlayBgAudio(string name, bool runloop = true) {
        AudioClip clip = ResSvc.Instance.LoadAudio("ResAudio/" + name);
        bgAudio.loop = runloop;
        bgAudio.PlayOneShot(clip);
        if (clip == null) {
            Debug.LogError("PlayBgAudio failed:" + name);
        }
    }

    public void PlayUIAudio(string name) {
        AudioClip clip = ResSvc.Instance.LoadAudio("ResAudio/" + name);
        uiAudio.loop = false;
        uiAudio.PlayOneShot(clip);
        if (clip == null) {
            Debug.LogError("PlayUIAudio failed:" + name);
        }

    }

    public void PlayCharAudio(string name,AudioSource audioSource){
        AudioClip clip = ResSvc.Instance.LoadAudio("ResAudio/" + name);
        if (audioSource)
        {
            audioSource.PlayOneShot(clip);
        }
    }



}