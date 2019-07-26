using System;

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
            playerCtrlWnd.SetWndState();
            GameObject go = new GameObject("BattleRoot");
            go.transform.SetParent(GameRoot.Instance.transform);
            battleMgr = go.AddComponent<BattleMgr>();
            battleMgr.Init(cfg);
            playerEntity = battleMgr.playerEntity;
            playerEntity.controller.gameObject.SetActive(false);
            Invoke("Born", 0.1f);
        });
    }

    private void Born() {
        playerEntity.controller.gameObject.SetActive(true);
        playerEntity.Born();
    }
    //TODO:根据配置表中的信息加载人物位置
    private void LoadPlayer(MapCfg mapData) {
        player = ResSvc.Instance.LoadPrefab(PathDefine.AssissnBattlePlayerPrefab);
        player.transform.position = mapData.playerBornPos;
        player.transform.localScale = Vector3.one;
        player.transform.localEulerAngles = mapData.playerBornRote;

        //相机初始化
        Camera.main.transform.position = mapData.mainCamPos;
        Camera.main.transform.localEulerAngles = mapData.mainCamRote;

        playerCtr = player.GetComponent<PlayerController>();
        playerCtr.Init();

    }

    public void SetDir(Vector3 dir) {
        if (playerEntity.canControl == false) {
            return;
        }
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
            case 3:
                ReleaseNormal();
                break;
        }

    }

    private double lastAtTime = 0;
    private int[] comBoArr = new int[] { 111, 112, 113, 114, 115 };
    public int comboIndex = 0;
    private void ReleaseNormal() {
        Debug.Log("ReleaseNormal" + playerEntity.currentAniState);
        if (playerEntity.currentAniState == AniState.Idle || playerEntity.currentAniState == AniState.Move) {
            playerEntity.Attack(comBoArr[0]);
            comboIndex = 0;
            lastAtTime = TimeSvc.Instance.GetNowTime();
        } else if (playerEntity.currentAniState == AniState.Attack && lastAtTime != 0) {
            if (comboIndex == comBoArr.Length - 1) {
                comboIndex = 0;
                lastAtTime = 0;
            } else if (TimeSvc.Instance.GetNowTime() - lastAtTime < Constant.ComboSpace && lastAtTime != 0) {
                comboIndex++;
                playerEntity.AddComnQueue(comBoArr[comboIndex]);
                lastAtTime = TimeSvc.Instance.GetNowTime();
            }
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

    public void RmMonsterByKey(string name) {
        battleMgr.RmMonsterByKey(name);
        GameRoot.Instance.dynamicWnd.RemoveHP(name);
    }

    public void EndBattle(bool isWin, int resHp) {
        // d
    }

    public void clickExitBtn(){
        // audioSvc.play
        MainCitySys.Instance.EnterMainCity();
        
    }

    public void DestoryBattle(){
        playerCtrlWnd.SetWndState(false);
        GameRoot.Instance.dynamicWnd.RmAllHpInfo();
        MainCitySys.Instance.EnterMainCity();
        Destroy(battleMgr.gameObject);
    }
}