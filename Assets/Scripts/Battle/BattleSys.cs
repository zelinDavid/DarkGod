using PEProtocol;

using UnityEngine;

public class BattleSys : SystemRoot {
    private int fbid;
    public static BattleSys Instance;
    public PlayerCtrWnd playerCtrlWnd;
    public PlayerController playerCtr;
    public GameObject player;
    public BattleMgr battleMgr;
    private PlayerEntity playerEntity;

    public override void InitSystem() {
        base.InitSystem();
        Instance = this;
        

    }

    //创建新的scene.并且初始化各种东西.
    public void LoadScene(int fbid) {
        MapCfg cfg = ResSvc.Instance.GetMapCfg(fbid);
        ResSvc.Instance.AsyncLoadScene(cfg.sceneName, () => {
            audioSvc.PlayBgAudio(Constant.BGHuangYe);
            LoadPlayer(cfg);
            GameObject go = new GameObject("BattleRoot");
            go.transform.SetParent(GameRoot.Instance.transform);
            battleMgr = go.AddComponent<BattleMgr>();
            battleMgr.Init(fbid);
            playerEntity = battleMgr.playerEntity;
            playerCtrlWnd.SetWndState();
        });
    }

    //TODO:根据配置表中的信息加载人物位置
    private void LoadPlayer(MapCfg mapData) {
        player = ResSvc.Instance.LoadPrefab(PathDefine.AssissnBattlePlayerPrefab);
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

    public bool CanReleaseSkill() {
        return battleMgr.playerEntity.canReleaseSkill;
    }

    public void Attack(int index) {
        switch (index) {
            case 0:
                ReleaseSkill1();
                break;
            case 1:
                ReleaseSkill2();
                break;
            case 2:
                ReleaseSkill3();
                break;
        }


    }

    private void ReleaseSkill1() {
      
        playerEntity.Attack(101);
    }
    private void ReleaseSkill2() {
         
        playerEntity.Attack(102);
    }
    private void ReleaseSkill3() {
     
        playerEntity.Attack(103);
    }

}