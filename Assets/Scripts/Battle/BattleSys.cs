using PEProtocol;

using UnityEngine;

public class BattleSys : SystemRoot {
    private int fbid;
    public static BattleSys Instance;
    public PlayerCtrWnd playerCtrlWnd;
    private PlayerController playerCtr;
    private GameObject player;
    public override void InitSystem() {
        base.InitSystem();
        Instance = this;

    }

    //创建新的scene.并且初始化各种东西.
    public void LoadScene(int fbid) {
        GameObject go = new GameObject("BattleRoot");
        go.transform.SetParent(GameRoot.Instance.transform);
        BattleMgr battle = go.AddComponent<BattleMgr>();
        battle.Init(fbid);

        MapCfg cfg = ResSvc.Instance.GetMapCfg(fbid);
        ResSvc.Instance.AsyncLoadScene(cfg.sceneName, () => {
            audioSvc.PlayBgAudio(Constant.BGHuangYe);
            LoadPlayer(cfg);

            playerCtrlWnd.SetWndState();

        });
    }

    //TODO:根据配置表中的信息加载人物位置
    private void LoadPlayer(MapCfg mapData) {
        player = ResSvc.Instance.LoadPrefab(PathDefine.AssissnCityPlayerPrefab);
        player.transform.position = mapData.playerBornPos;
        player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        player.transform.localEulerAngles = mapData.playerBornRote;

        //相机初始化
        Camera.main.transform.position = mapData.mainCamPos;
        Camera.main.transform.localEulerAngles = mapData.mainCamRote;

        playerCtr = player.GetComponent<PlayerController>();
        playerCtr.Init();

    }

    public void SetDir(Vector3 dir) {
        if (dir == Vector3.zero) {
            playerCtr.SetBlend((float) Constant.BlendIdle);
        } else {
            playerCtr.SetBlend((float) Constant.BlendMove);
        }
        playerCtr.Dir = dir;
    }
}

/*
    把他当做你今天下午的任务, 今天下午的任务:
    人物控制移动,生成怪物;
    
/*
    加载场景, 初始化脚本 battleMgr 及  playerCtrWnd等. StateMgr, SkillMgr

 */