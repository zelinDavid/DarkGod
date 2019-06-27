using System.Collections.Generic;
using System.IO;
using System.Xml;
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

    public void InitService() {
        Instance = this;

        InitRDNameCfg(); //TODO;
        Debug.Log("Init Service...");

    }

    private Action prgCB = null;

    public void AsyncLoadScene(string name, Action finishAction) {
        StartCoroutine(coroutineLoadSync(name, finishAction));

    }
    private IEnumerator coroutineLoadSync(string name, Action finishAction) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        operation.allowSceneActivation = false;
        GameRoot.Instance.loadingWnd.SetWndState(true);
        GameRoot.Instance.loadingWnd.SetProgress(0f);

        float progress;
        float currentProgress = 0;
        while (operation.progress < 0.9f) {
            progress = operation.progress;
            while (currentProgress < progress) {
                currentProgress += 0.01f;
                 GameRoot.Instance.loadingWnd.SetProgress(currentProgress);
                yield return null;
            }
        }
        operation.allowSceneActivation = true;
        progress = 0.98f;
        while (currentProgress < progress) {
            currentProgress += 0.01f;
            GameRoot.Instance.loadingWnd.SetProgress(currentProgress);

            yield return null;
        }
        Debug.Log("coroutineLoadSync Finish");

        while (operation.isDone == false)
        {
            yield return null;
        }
        GameRoot.Instance.loadingWnd.SetProgress(1f);
        GameRoot.Instance.loadingWnd.SetWndState(false);

        finishAction();
    }

    #region  RandomName
    private List<string> Mans = new List<string>();
    private List<string> Womans = new List<string>();
    private List<string> Surnames = new List<string>();

    private void InitRDNameCfg() {
        string text = File.ReadAllText(Application.dataPath + "/Resources/" + PathDefine.RDNameCfg + ".xml");
        // Debug.Log(text);
        if (text.Length > 0) {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(text);

            XmlNodeList nodList = doc.SelectSingleNode("root").ChildNodes;
            for (int i = 0; i < nodList.Count; i++) {
                XmlElement ele = nodList[i] as XmlElement;
                foreach (XmlNode node in ele.ChildNodes) {
                    switch (node.Name) {
                        case "surname":
                            Surnames.Add(node.InnerText);
                            break;
                        case "man":
                            Mans.Add(node.InnerText);
                            break;
                        case "woman":
                            Womans.Add(node.InnerText);
                            break;
                    }
                }
            }
        }
        // Debug.Log(Mans.Count);
    }

    public System.Random Random = new System.Random();
    public string GetRDNameData(bool man = true) {
        int first = Random.Next(0,Surnames.Count - 1);
        int sec = man ? Random.Next(0,Mans.Count - 1): Random.Next(0,Womans.Count - 1) ;
        string name = Surnames[first] + (man?Mans[sec]:Womans[sec]);
         Debug.Log("name:" + name);
         return name;
    }
    #endregion

    private Dictionary<string, AudioClip> clipCache = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path, bool isCache = true) {
        if (clipCache.TryGetValue(path, out AudioClip clip)) {
            return clip;
        }

        clip = Resources.Load<AudioClip>(path);
        if (isCache) {
            clipCache.Add(path, clip);
        }
        return clip;
    }

    private Dictionary<string,GameObject> PrefabCache = new Dictionary<string, GameObject>();
    public GameObject LoadPrefab(string prefabStr,bool needCache = true){
        PrefabCache.TryGetValue(prefabStr, out GameObject prefab);
        if (prefab != null)
        {
            return prefab;
        }
        
        prefab = Resources.Load<GameObject>(prefabStr);
        if (needCache)
        {
        
            PrefabCache.Add(prefabStr, prefab);
        }
         GameObject go = Instantiate(prefab);
        // Debug.Log("prefabbb +" + go);
        return go;
    }

}