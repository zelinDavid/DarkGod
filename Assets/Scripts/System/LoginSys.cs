/****************************************************
    文件：LoginSys.cs
	
    日期：2018/12/3 5:31:49
	功能：登录注册业务系统
*****************************************************/

using PEProtocol;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSys : SystemRoot {
    public LoginWnd loginWind;
    public CreateWindow createWind;
    public static LoginSys Instance;
    public override void InitSystem() {
        base.InitSystem();

        Instance = this;

        Debug.Log("init loginSys");

        Invoke("Test",1f);
    }

    private void Test(){
        loginWind.clickEnterBtn();
    }

    public void EnterLogin() {
        audioSvc.PlayUIAudio(Constant.UIClickBtn);
        resSvc.AsyncLoadScene("SceneLogin", () => {
            loginWind.SetWndState();
            AudioSvc.Instance.PlayBgAudio(Constant.BGLogin);
        });
    }

    public void RspLogin(GameMsg msg) {
        GameRoot.Instance.AddTips("登录成功");
        GameRoot.Instance.SetPlayerData(msg);

        if (msg.rspLogin.playerData == null || msg.rspLogin.playerData.name == "") {
            //开始生成
            Debug.Log("msg.rspLogin.playerData.name");
            Debug.Log(msg.rspLogin.playerData.name );
            Debug.Log(msg.rspLogin.playerData);

            createWind.SetWndState(true);
        } else {
           
            MainCitySys.Instance.EnterMainCity();
        }
        Debug.Log(msg.rspLogin.playerData);

        loginWind.SetWndState(false);
    }

    public void RspRename(GameMsg msg) {
        GameRoot.Instance.SetPlayerName(msg.rspRename.name);
    
        MainCitySys.Instance.EnterMainCity();
        createWind.SetWndState(false);

    }

}