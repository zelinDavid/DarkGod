using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class MainCitySys : SystemRoot {
    public static MainCitySys Instance;
    public MainCityWnd maincityWnd;
    public InfoWnd infoWnd;
    public GuidWnd guidWnd;

    protected NavMeshAgent nav;
    protected PlayerController PlayerController;
    protected Transform charShowCam;
    protected Vector3 charCamOffset;
    private Vector3 charCamRotation;
    private AutoGuideCfg curtTaskData;
    private Transform[] npcPosTrans;

    private bool isNavGuide = false;

    public override void InitSystem() {
        base.InitSystem();
        Debug.Log("init MainCitySys");
        Instance = this;

    }

    public void EnterMainCity() {
        MapCfg mapData = resSvc.GetMapCfg(Constant.MainCityMapID);
        resSvc.AsyncLoadScene(mapData.sceneName, () => {

            GameRoot.Instance.GetComponent<AudioListener>().enabled = false;
            audioSvc.PlayBgAudio(Constant.BGMainCity);

            LoadPlayer(mapData);
            InitMainWnd();
            InitCamera();
            InitNpcInfo();
        });
    }

    private void InitNpcInfo() {
        GameObject go = GameObject.FindGameObjectWithTag("MapRoot");
        MainCityMap mainCity = go.GetComponent<MainCityMap>();
        npcPosTrans = mainCity.NpcPosTrans;

    }

    private void InitMainWnd() {
        maincityWnd.SetWndState(true);
    }

    public void ClickMeBtn() {

        //TODO:position计算公式有问题
        charShowCam.localPosition = PlayerController.transform.position + PlayerController.transform.forward * 3.5f + PlayerController.transform.up * 1.28f;
        charShowCam.localEulerAngles = PlayerController.transform.localEulerAngles + new Vector3(0, 180, 0);
        infoWnd.SetWndState(true);

    }

    private void InitCamera() {
        charShowCam = GameObject.Find("charShowCam").transform;

        Debug.Log("InitCamera:" + charShowCam);
        Debug.Log("InitCamera:" + PlayerController.transform);

        if (charShowCam == null) {
            Debug.LogError("charShowCam = null");
        }

    }

    private void Update() {
        if (isNavGuide && curtTaskData != null) {
            Vector3 targetPos = npcPosTrans[curtTaskData.npcID].position;
            Vector3 startPos = PlayerController.transform.position;
            float distance = Vector3.Distance(targetPos, startPos);
            if (distance <= 1f) {
                StopNavAndShowNav();
            }
        }
    }

    public void SetMoveDir(Vector2 dir) {
        //TODO:停止导航
        // Debug.Log("setMoveDir:" + dir);
        if (dir == Vector2.zero) {
            PlayerController.SetBlend(Constant.BlendIdle);
        } else {
            PlayerController.SetBlend(Constant.BlendMove);
        }
        PlayerController.Dir = dir;
    }

    //TODO:根据配置表中的信息加载人物位置
    private void LoadPlayer(MapCfg mapData) {
        GameObject player = resSvc.LoadPrefab(PathDefine.AssissnCityPlayerPrefab);
        player.transform.position = mapData.playerBornPos;
        player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        player.transform.localEulerAngles = mapData.playerBornRote;

        //相机初始化
        Camera.main.transform.position = mapData.mainCamPos;
        Camera.main.transform.localEulerAngles = mapData.mainCamRote;

        PlayerController = player.GetComponent<PlayerController>();
        PlayerController.Init();

        nav = player.GetComponent<NavMeshAgent>();

    }

    public void setPlayerRotation(Vector3 deltaRotation) {
        PlayerController.transform.localEulerAngles += deltaRotation;
    }

    public void guidWithCfg(AutoGuideCfg cfg) {
        //存取对应prefab位置的数组.  根据对应id获得对应的位置.
        /*
            判断起始点距离大小. 
            设置自动导航,
            update的时候判断是否完成导航,并且弹出对应的界面.
         */

        //解析任务数据
        nav.enabled = true;
        if (cfg.npcID != -1) {
            curtTaskData = cfg;
        }
        Vector3 targetPos = npcPosTrans[cfg.npcID].position;
        Vector3 startPos = PlayerController.transform.position;
        float distance = Vector3.Distance(targetPos, startPos);
        if (distance < 1f) {
            StopNavAndShowNav();
        } else {
            nav.enabled = true;
            nav.isStopped = false;
            nav.speed = Constant.PlayerMoveSpeed;
            isNavGuide = true;
            nav.destination = targetPos;
            PlayerController.SetBlend(Constant.BlendMove);
        }
    }

    private void StopNavAndShowNav() {
        nav.isStopped = true;
        nav.enabled = false;
        isNavGuide = false;
        // guidWnd.SetWndState();
        PlayerController.SetBlend(Constant.BlendIdle);

    }
}