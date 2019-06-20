/****************************************************
    文件：ClientSession.cs
	
    日期：2018/12/7 5:22:14
	功能：客户端网络会话
*****************************************************/

using PENet;
using PEProtocol;
using UnityEngine;


public class ClientSession : PESession<GameMsg>
{
  
    protected override void OnConnected()
    {
        Debug.LogWarning("OnConnected");
    }
    protected override void OnDisConnected()
    {
        Debug.LogWarning("OnDisConnected");
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        Debug.LogWarning("OnReciveMsg");
        NetSvc.Instance.AddMessageQueue(msg);
    }




}

