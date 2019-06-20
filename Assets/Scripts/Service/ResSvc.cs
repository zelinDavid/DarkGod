using System.Collections.Generic;
/****************************************************
    文件：ResSvc.cs
	作者：BearYang
    邮箱: 1785275942@qq.com
    日期：2019/1/7 19:54:16
	功能：Nothing
*****************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : MonoBehaviour {
    public static ResSvc Instance;

    public void InitService () {
        Instance = this;

        InitRDNameCfg (); //TODO;
        Debug.Log ("Init Service...");

    }

    private Action prgCB = null;

    public void LoadSceneAsync (string name, Action finishAction) {
        StartCoroutine (coroutineLoadSync (name, finishAction));

    }
    private IEnumerator coroutineLoadSync (string name, Action finishAction) {
        AsyncOperation operation = SceneManager.LoadSceneAsync (name);
        operation.allowSceneActivation = false;

        float progress;
        float currentProgress = 0;
        while (operation.progress < 0.9) {
            progress = operation.progress;
            while (currentProgress < progress) {
                currentProgress++;
                GameRoot.Instance.UpdateloadingInfo (currentProgress, name);
                yield return null;
            }
        }
        progress = 1.0f;
        while (currentProgress < progress) {
            currentProgress++;
            GameRoot.Instance.UpdateloadingInfo (currentProgress, name);
            yield return null;
        }
        GameRoot.Instance.UpdateloadingInfo (1.0f, name);
        finishAction ();
    }

    private void InitRDNameCfg () {

    }

    private Dictionary<string, AudioClip> clipCache = new Dictionary<string, AudioClip> ();
    public AudioClip LoadAudio (string path, bool isCache = true) {
        if (clipCache.TryGetValue (path, out AudioClip clip)) {
            return clip;
        }

        clip = Resources.Load<AudioClip> (path);
        if (isCache) {
            clipCache.Add (path, clip);
        }
        return clip;
    }

}