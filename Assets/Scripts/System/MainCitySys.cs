using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class MainCitySys : SystemRoot {
    public static MainCitySys Instance;

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

            /*
            关闭listener
            更新背景音乐
            创建player.
                创建player相关属性对象.
            其他配置: 摄像机展示人物

                之后,控制人物移动
             */

            GameRoot.Instance.GetComponent<AudioListener>().enabled = false;
            audioSvc.PlayBgAudio(Constant.BGMainCity);

            LoadPlayer();
            InitCamera();

            //TODO: 展示人物的摄像机激活状态设置为false

        });

    }

    private void InitCamera() {

    }

    private void LoadPlayer() {
        GameObject player = resSvc.LoadPrefab(PathDefine.AssissnCityPlayerPrefab);
        player.transform.localPosition = new Vector3(55.503f, 0f, 55.503f);
        player.transform.localScale = new Vector3(1.5F, 1.5F, 1.5F);
        player.transform.localEulerAngles = new Vector3(0, 0, 0);
         
        PlayerController = player.GetComponent<PlayerController>();

        nav = player.GetComponent<NavMeshAgent>();
    }
}