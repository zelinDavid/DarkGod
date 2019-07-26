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
    /// <summary>
    /// 创建血条信息
    /// </summary>
    /// <param name="mName">prefab名称</param>
    /// <param name="trans">对象血条所在位置到的transform</param>
    /// <param name="hp">血量</param>
    public void AddHpItemInfo(string mName, Transform hpRoot, int hp) {
        Debug.Log("AddHpItemInfo: " + mName);
        if (itemDic.ContainsKey(mName)) {
            return;
        }
        GameObject itemPrefab = resSvc.LoadPrefab(PathDefine.HPItemPrefab, true);
        itemPrefab.transform.SetParent(hpItemRoot);
        ItemEntityHP entityHP = itemPrefab.GetComponent<ItemEntityHP>();
        entityHP.Init(hpRoot, hp);
        itemDic.Add(mName, entityHP);
    }

    public void RemoveHP(string name) {
        if (itemDic.TryGetValue(name, out ItemEntityHP obj)) {
            itemDic.Remove(name);
            Debug.Log("RemoveHP:" + name);
            obj.gameObject.SetActive(false);
            Destroy(obj.gameObject);
        }
    }

    public void ShowDodge(string name) {
        if (itemDic.TryGetValue(name, out ItemEntityHP itemHp)) {
            itemHp.SetDodge();
        }
    }

    public void ShowCritical(string name, int damage) {
        if (itemDic.TryGetValue(name, out ItemEntityHP itemHp)) {
            itemHp.SetCritical(damage);
        }
    }

    public void SetHurt(string name, int damage) {
        if (itemDic.TryGetValue(name, out ItemEntityHP itemHp)) {
            itemHp.SetHurt(damage);
        }
    }

    public void RmAllHpInfo() {
        foreach (var item in itemDic) {

            Destroy(item.Value.gameObject);
             

        }
        itemDic.Clear();
    }
}