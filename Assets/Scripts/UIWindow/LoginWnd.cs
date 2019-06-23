/****************************************************
    文件：LoginWnd.cs
	
    日期：2018/12/4 3:53:36
	功能：登录注册界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;
using PEProtocol;

public class LoginWnd : WindowRoot {
    public InputField accInput;
    public InputField passInput;
    

    public void clickEnterBtn( )
    {
        string acc = accInput.text;
        string pass = accInput.text;

        NetSvc.Instance.SendMessage(new GameMsg {
            cmd = (int)CMD.ReqLogin,
            reqLogin = new ReqLogin
            {
                acct = acc,
                pass = pass,
            },
        });
    }

  
}