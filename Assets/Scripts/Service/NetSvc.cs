/****************************************************
    文件：NetSvc.cs
	作者：BearYang
    邮箱: 1785275942@qq.com
    日期：2019/1/9 11:16:23
	功能：Nothing
*****************************************************/

using PENet;
using UnityEngine;
using PEProtocol;
using System;
using System.Collections.Generic;

public class NetSvc : MonoBehaviour 
{
    public static NetSvc Instance;
    public PESocket<ClientSession, GameMsg> socket;
    private Queue<GameMsg> msgQueue = new Queue<GameMsg>();
    private bool coldTime;

    string obj = "lock";

    private void Start()
    {
        Init();
        Instance = this;
    }

    private void Update()
    {
        lock (obj)
        {
            if ( msgQueue.Count > 0)
            {
                 GameMsg msg = msgQueue.Dequeue();
                ProcessMsg(msg);
            }
        }
        
    }

    public void Init()
    {
        socket = new PESocket<ClientSession, GameMsg>();

        socket.SetLog(true,(string msg,int lv) =>
        {
            switch (lv)
            {
                case 0:
                    msg = "Log:" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg = "Warn:" + msg;
                    Debug.LogWarning(msg);
                    break;
                case 2:
                    msg = "Error:" + msg;
                    Debug.LogError(msg);
                    break;
                case 3:
                    msg = "Info:" + msg;
                    Debug.Log(msg);
                    break;
            }
        });
        socket.StartAsClient(SrvCfg.srvIP, SrvCfg.srvPort);
        PECommon.Log("Init NetSvc...");
    }
    
   private void ProcessMsg(GameMsg msg)
    {
        if (msg.err != (int)ErrorCode.None)
        {
            switch (msg.err)
            {
                case (int)ErrorCode.WrongPass:
                    Debug.LogWarning("密码错误");
                    break;
                case (int)ErrorCode.AcctIsOnline:
                    Debug.LogWarning("账号已在线");
                    break;
                 default:
                    break;
            }
            return;
        }

        if ((CMD)msg.cmd == CMD.RspLogin)
        {
            Debug.Log("登录成功");

        }



    }
   
    public void AddMessageQueue(GameMsg msg)
    {
        lock (obj)
        {
            msgQueue.Enqueue(msg);
        }
    }

    public void SendMessage(GameMsg msg)
    {
       if (socket.session != null )
       {
            socket.session.SendMsg(msg);
        }
        else
        {
            Debug.Log("服务器未连接");
        }
    }

}