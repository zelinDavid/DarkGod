/****************************************************
    文件：LoginSys.cs
	
    日期：2018/12/3 5:31:49
	功能：登录注册业务系统
*****************************************************/

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
    }

    public void EnterLogin() {
        resSvc.AsyncLoadScene("SceneLogin", () => {
            loginWind.SetWindowActive();
            AudioSvc.Instance.PlayBgAudio(Constant.BGLogin);
        });
    }

    // public void RspLogin(string name) {
    //     loginWind.SetWindowActive(false);
    //     createWind.SetWindowActive();
    // }

}