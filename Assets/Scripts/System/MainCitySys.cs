using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class MainCitySys : SystemRoot {
    public static MainCitySys Instance;
    public MainCityWnd maincityWnd;

    protected NavMeshAgent nav;
    protected PlayerController PlayerController;

    public override void InitSystem() {
        base.InitSystem();
        Debug.Log("init MainCitySys");
        Instance = this;

    }

    public void EnterMainCity() {
        //TODO:编码 resSvc.Config()
        resSvc.AsyncLoadScene("SceneMainCity", () => {

            GameRoot.Instance.GetComponent<AudioListener>().enabled = false;
            audioSvc.PlayBgAudio(Constant.BGMainCity);

            LoadPlayer();
            InitCamera();

            maincityWnd.SetWndState(true);

            //TODO: 展示人物的摄像机激活状态设置为false

        });

    }

    private void InitCamera() {

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

    private void LoadPlayer() {
        GameObject player = resSvc.LoadPrefab(PathDefine.AssissnCityPlayerPrefab);
        player.transform.position = new Vector3(55.503f, -0.722f, 54.4783f);
        player.transform.localScale = new Vector3(1.5F, 1.5F, 1.5F);
        player.transform.localEulerAngles = new Vector3(0, 0, 0);

        PlayerController = player.GetComponent<PlayerController>();
        PlayerController.Init();

        nav = player.GetComponent<NavMeshAgent>();

    }
}