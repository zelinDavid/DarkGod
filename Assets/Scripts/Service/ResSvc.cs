using UnityEngine;

#if UnityEditor
    using UnityEditor;
#endif

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

        while (operation.isDone == false) {
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
        int first = Random.Next(0, Surnames.Count - 1);
        int sec = man ? Random.Next(0, Mans.Count - 1) : Random.Next(0, Womans.Count - 1);
        string name = Surnames[first] + (man?Mans[sec] : Womans[sec]);
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

    private Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();
    public GameObject LoadPrefab(string prefabStr, bool needCache = true) {
        PrefabCache.TryGetValue(prefabStr, out GameObject prefab);
        if (prefab != null) {
            return prefab;
        }

        prefab = Resources.Load<GameObject>(prefabStr);
        if (needCache) {

            PrefabCache.Add(prefabStr, prefab);
        }
        GameObject go = Instantiate(prefab);
        // Debug.Log("prefabbb +" + go);
        return go;
    }

    #region 地图信息
    private Dictionary<int, MapCfg> mapCfgDataDic = new Dictionary<int, MapCfg>();
 
    public  void InitMapCfg(string path) {
        Dictionary<int, MapCfg> mapCfgDataDic = new Dictionary<int, MapCfg>();
        TextAsset ta = Resources.Load<TextAsset>(path);
        string content = ta.text;
        if (content == null || content.Length == 0) {
            Debug.LogError("未获取到文件");
        } else {
            XmlDocument document = new XmlDocument();
            document.LoadXml(content);

            XmlNode root = document.SelectSingleNode("root");
            foreach (XmlElement ele in root) {
                if (ele.GetAttributeNode("ID") == null) {
                    continue;
                }

                int id = int.Parse(ele.GetAttribute("ID"));

                MapCfg mc = new MapCfg {
                    ID = id,
                    monsterLst = new List<MonsterData>()
                };

                foreach (XmlNode e in ele.ChildNodes) {
                    switch (e.Name) {
                        case "mapName":
                            mc.mapName = e.InnerText;
                            break;
                        case "sceneName":
                            mc.sceneName = e.InnerText;
                            break;
                        case "power":
                            mc.power = int.Parse(e.InnerText);
                            break;
                        case "mainCamPos":
                            {
                                string[] valArr = e.InnerText.Split(',');
                                mc.mainCamPos = new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
                            }
                            break;
                        case "mainCamRote":
                            {
                                string[] valArr = e.InnerText.Split(',');
                                mc.mainCamRote = new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
                            }
                            break;
                        case "playerBornPos":
                            {
                                string[] valArr = e.InnerText.Split(',');
                                mc.playerBornPos = new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
                            }
                            break;
                        case "playerBornRote":
                            {
                                string[] valArr = e.InnerText.Split(',');
                                mc.playerBornRote = new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
                            }
                            break;
                        case "monsterLst":
                            {
                                // string[] valArr = e.InnerText.Split('#');
                                // for (int waveIndex = 0; waveIndex < valArr.Length; waveIndex++) {
                                //     if (waveIndex == 0) {
                                //         continue;
                                //     }
                                //     string[] tempArr = valArr[waveIndex].Split('|');
                                //     for (int j = 0; j < tempArr.Length; j++) {
                                //         if (j == 0) {
                                //             continue;
                                //         }
                                //         string[] arr = tempArr[j].Split(',');
                                //         MonsterData md = new MonsterData {
                                //             ID = int.Parse(arr[0]),
                                //             mWave = waveIndex,
                                //             mIndex = j,
                                //             // mCfg = GetMonsterCfg(int.Parse(arr[0])),
                                //             mBornPos = new Vector3(float.Parse(arr[1]), float.Parse(arr[2]), float.Parse(arr[3])),
                                //             mBornRote = new Vector3(0, float.Parse(arr[4]), 0),
                                //             mLevel = int.Parse(arr[5])
                                //         };
                                //         mc.monsterLst.Add(md);
                                //     }
                                // }
                            }
                            break;
                        case "coin":
                            mc.coin = int.Parse(e.InnerText);
                            break;
                        case "exp":
                            mc.exp = int.Parse(e.InnerText);
                            break;
                        case "crystal":
                            mc.crystal = int.Parse(e.InnerText);
                            break;
                    }
                    mapCfgDataDic.Add(id, mc);
                }
            }
            foreach (MapCfg item in mapCfgDataDic.Values) {

                Debug.Log(item.ID);
                Debug.Log(item.mapName);
                Debug.Log(item.sceneName);
                Debug.Log(item.power);
            }
        }

        // public MonsterCfg GetMonsterCfg(int id) {
        //     MonsterCfg data;
        //     if (monsterCfgDataDic.TryGetValue(id, out data)) {
        //         return data;
        //     }
        //     return null;
        // }
    }
    #endregion
}