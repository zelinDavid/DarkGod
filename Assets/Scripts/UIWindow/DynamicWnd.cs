using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DynamicWnd : WindowRoot {
    public Text tipText;
    public Animation tipAnim;
    private Queue tipsQueue = new Queue();
    private bool isShow = false;
    protected override void InitWnd() {
        base.InitWnd();
        SetActive(tipText, false);
    }

    public void AddTips(string tip) {
        Debug.Log("addTips: " + tip);
        tipsQueue.Enqueue(tip);
        Debug.Log("addTips111: " + tipsQueue.Count);

        
    }
   
    private void Update() {
        Debug.Log("update " + tipsQueue.Count );
        if (tipsQueue.Count > 0 && !isShow) {
            lock(tipsQueue) {
                string tip = tipsQueue.Dequeue() as string;
                SetTips(tip);
                Debug.LogWarning("tipsss: " + tip);
                StartCoroutine(AniPlayDone(tipAnim.clip.length, () => {
                    SetActive(tipText, false);
                    isShow = false;
                }));
            }
        }

    }

    private void SetTips(string tip) {
        tipText.text = tip;
        SetActive(tipText, true);
    }
    private IEnumerator AniPlayDone(float sec, Action cb) {
        tipAnim.Play();
        isShow = true;
        Debug.Log("AniPlayDone");
        yield return new WaitForSeconds(sec);
        if (cb != null) {
            cb();
        }
    }
}