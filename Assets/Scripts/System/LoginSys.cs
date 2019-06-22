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
    public override void InitSystem () {
        base.InitSystem ();

        Instance = this;

        Debug.Log ("init loginSys");
    }

    public void EnterLogin () {
        resSvc.AsyncLoadScene ("SceneLogin", () => {
            loginWind.SetWndState ();
            AudioSvc.Instance.PlayBgAudio (Constant.BGLogin);
        });
    }

    public void RspLogin (GameMsg msg) {
        GameRoot.Instance.AddTips ("登录成功");
        GameRoot.Instance.SetPlayerData (msg);

        if (msg.rspLogin.playerData == null || msg.rspLogin.playerData.name == "") {
            //开始生成
            createWind.SetWndState (true);
        } else {
            //进入场景TODO
        }
        Debug.Log (msg.rspLogin.playerData);

        loginWind.SetWndState (false);
    }

    public void RspRename (GameMsg msg) {
        GameRoot.Instance.SetPlayerName (msg.rspRename.name);
        //todo:进入主场景

        // createWind.SetWndState (false);
        
    }

}