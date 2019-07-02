using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class MainCitySys : SystemRoot {
    public static MainCitySys Instance;
    public MainCityWnd maincityWnd;
    public InfoWnd infoWnd;
    protected NavMeshAgent nav;
    protected PlayerController PlayerController;
    protected Transform charShowCam;
    protected Vector3 charCamOffset;
    private Vector3 charCamRotation;
    
    public override void InitSystem() {
        base.InitSystem();
        Debug.Log("init MainCitySys");
        Instance = this;

    }

    public void EnterMainCity() {

        resSvc.AsyncLoadScene("SceneMainCity", () => {

            GameRoot.Instance.GetComponent<AudioListener>().enabled = false;
            audioSvc.PlayBgAudio(Constant.BGMainCity);

            LoadPlayer();
            InitMainWnd();
            Invoke("InitCamera", 2);
            // InitCamera();
         
        });

    }

    private void InitMainWnd() {
        maincityWnd.SetWndState(true);
    }

    public void ClickMeBtn() {

        //TODO:position计算公式有问题
        charShowCam.localPosition = PlayerController.transform.position + PlayerController.transform.forward * 3.5f + PlayerController.transform.up *1.28f;
        charShowCam.localEulerAngles = PlayerController.transform.localEulerAngles + new Vector3(0,180,0);
        infoWnd.SetWndState(true);
          

    }
    
    private void InitCamera() {
       charShowCam = GameObject.Find("charShowCam").transform;
 
       Debug.Log("InitCamera:" + charShowCam);
       Debug.Log("InitCamera:" + PlayerController.transform);

       if (charShowCam == null)
       {
           Debug.LogError("charShowCam = null");
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
    private void LoadPlayer() {
        GameObject player = resSvc.LoadPrefab(PathDefine.AssissnCityPlayerPrefab);
        player.transform.position = new Vector3(55.503f, -0.722f, 54.4783f);
        player.transform.localScale = new Vector3(1.5F, 1.5F, 1.5F);
        player.transform.localEulerAngles = new Vector3(0, 0, 0);

        PlayerController = player.GetComponent<PlayerController>();
        PlayerController.Init();

        nav = player.GetComponent<NavMeshAgent>();

    }
    
    public void setPlayerRotation(Vector3 deltaRotation){
        PlayerController.transform.localEulerAngles += deltaRotation;
    }
}