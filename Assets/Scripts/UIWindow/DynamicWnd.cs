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
    public Transform hpItemRoot;

    protected override void InitWnd() {
        base.InitWnd();
        SetActive(tipText, false);
    }

    public void AddTips(string tip) {
        // Debug.Log("addTips: " + tip);
        tipsQueue.Enqueue(tip);
    }

    private void Update() {

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

    private Dictionary<string, ItemEntityHP> itemDic = new Dictionary<string, ItemEntityHP>();
    public void AddHpItemInfo(string mName, Transform trans, int hp) {
        //创建hpUI到 hpRoot下, 更新其位置, 添加entityHp组件并添加到字典中
        //TODO:待修改
        if (itemDic.ContainsKey(mName)) {
            return;
        }
        GameObject itemPrefab = resSvc.LoadPrefab(PathDefine.HPItemPrefab, true);
        itemPrefab.transform.SetParent(hpItemRoot);
        ItemEntityHP entityHP = itemPrefab.GetComponent<ItemEntityHP>();
        itemDic.Add(mName, entityHP);
    }
}