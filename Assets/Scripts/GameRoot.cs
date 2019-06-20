/****************************************************
    文件：GameRoot.cs
	作者：SIKI学院——Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/3 5:30:21
	功能：游戏启动入口
*****************************************************/

using UnityEngine;

public class GameRoot : MonoBehaviour {
    public static GameRoot Instance = null;

    public void Start () {
        Instance = this;
        DontDestroyOnLoad (this);

        ClearUI ();
        Init();
    }

    private void ClearUI () {
        Transform canvs = transform.Find ("Canvas");
        for (int i = 0; i < canvs.childCount; i++) {
            canvs.GetChild (i).gameObject.SetActive (false);
        }
    }

    //初始化各个模块
    protected void Init () {
        ResSvc resSev = GetComponent<ResSvc>();
        resSev.InitService();

        AudioSvc audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitService();

        //业务系统初始化
        LoginSys login = GetComponent<LoginSys>();

    }

    public void UpdateloadingInfo (float progress, string name) {

    }

}