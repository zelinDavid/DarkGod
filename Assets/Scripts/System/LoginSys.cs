/****************************************************
    文件：LoginSys.cs
	作者：SIKI学院——Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/3 5:31:49
	功能：登录注册业务系统
*****************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSys : SystemRoot {
    public LoginWnd loginWind;
    public override void Start()
    {
        base.Start();
        this.loadLoginWindow();
    }
    protected void Init()
    {
        
    }

    protected void loadLoginWindow()
    {
        
        resSvc.LoadSceneAsync("SceneLogin", () => {
               loginWind.activeWindow();
        });        
    }

} 