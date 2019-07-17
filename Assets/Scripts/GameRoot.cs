/****************************************************
    文件：GameRoot.cs
	
    日期：2018/12/3 5:30:21
	功能：游戏启动入口
*****************************************************/

using PEProtocol;

using UnityEngine;

public class GameRoot : MonoBehaviour {
    public static GameRoot Instance = null;
    private DynamicWnd dynamicWnd;
    public LoadingWnd loadingWnd;
    private BattleSys battleSys;

    public void Start() {
        Instance = this;
        DontDestroyOnLoad(this);

        ClearUI();
        Init();
        initData();
    }

    private void initData() {
        dynamicWnd = transform.Find("Canvas/DynamicWnd").GetComponent<DynamicWnd>();
        dynamicWnd.SetWndState();
    }

    private void ClearUI() {
        Transform canvs = transform.Find("Canvas");
        for (int i = 0; i < canvs.childCount; i++) {
            canvs.GetChild(i).gameObject.SetActive(false);
        }
    }

    //初始化各个模块
    protected void Init() {
        ResSvc resSev = GetComponent<ResSvc>();
        resSev.InitSvc();

        AudioSvc audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitService();

        //业务系统初始化
        LoginSys login = GetComponent<LoginSys>();
        login.InitSystem();

        //主场景
        MainCitySys main = GetComponent<MainCitySys>();
        main.InitSystem();

        battleSys = GetComponent<BattleSys>();
        battleSys.InitSystem();

        login.EnterLogin();
    }
 
    public PlayerData playerData;
    public void SetPlayerData(GameMsg msg) {
        this.playerData = msg.rspLogin.playerData;
    }



    public void AddTips(string tip) {
        // Debug.Log("dynamicWnd" + this.dynamicWnd);
        this.dynamicWnd.AddTips(tip);
    }

    public void SetPlayerName(string name){
        if (this.playerData == null)
        {
            Debug.LogError("SetPlayerName playerData 为null");
            return ;
        }
        this.playerData.name = name;
    }

    public void SendBattleMsg(){
        NetSvc.Instance.SendMessage(new GameMsg{
           cmd = (int)CMD.ReqFBFight,
                reqFBFight = new ReqFBFight{
                fbid = playerData.fuben,
            },
        });
    }

    public void LoadBattleScne(GameMsg msg){
        battleSys.LoadScene(msg.rspFBFight.fbid);
        MainCitySys.Instance.maincityWnd.SetWndState(false);
    }
 
}