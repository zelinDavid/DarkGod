/****************************************************
	文件：NetSvc.cs
	作者：SIKI学院——Plane
	邮箱: 1785275942@qq.com
	日期：2018/12/07 4:40   	
	功能：网络服务
*****************************************************/

using PENet;
using PEProtocol;
using System.Collections.Generic;

public class MessagePack
{
    public GameMsg msg;
    public ServerSession session;
    public MessagePack(ServerSession session, GameMsg msg)
    {
        this.msg = msg;
        this.session = session;
    }
}

public class NetSvc {
    private static NetSvc instance;
    public static NetSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetSvc();
            }
            return instance;
        }
    }

    public void Init()
    {
        //启动服务器
        PESocket<ServerSession, GameMsg> server = new PESocket<ServerSession, GameMsg>();
        server.StartAsServer(SrvCfg.srvIP, SrvCfg.srvPort);

        PECommon.Log("服务器已启用");
    }

    //public void SendMessage(MessagePack)
    //{

    //}

    public void DealMessage()
    {

    }

    private Queue<MessagePack> msgPackQue = new Queue<MessagePack>();
    string obj = "lock";
    public void AddMsgQue(ServerSession session, GameMsg msg)
    {
        lock (obj)
        {
            msgPackQue.Enqueue(new MessagePack(session, msg));
        }
    }

    public void Update()
    {
        if (msgPackQue.Count > 0)
        {
            //PECommon.Log("QueCount:" + msgPackQue.Count);
            lock (obj)
            {
                MessagePack pack = msgPackQue.Dequeue();
                HandOutMsg(pack);
            }
        }
    }

    private void HandOutMsg(MessagePack pack)
    {
        GameMsg msg = pack.msg;
        ServerSession session = pack.session;

        switch (msg.cmd)
        {
            case (int)CMD.ReqLogin:
                 //handle

                break;
            default:
                break;
        }
    }
    private void hanleReLogin(MessagePack pack)
    {
        GameMsg msg = pack.msg;
        ServerSession session = pack.session;

        string acc = msg.reqLogin.acct;
        string pass = msg.reqLogin.pass;
        //成功了
        if (true)
        {
            session.SendMsg(new GameMsg
            {
                cmd = (int)CMD.RspLogin,
                rspLogin = new RspLogin {
                    playerData = new PlayerData
                    {

                    },
                },


            });
        }
    }


}
