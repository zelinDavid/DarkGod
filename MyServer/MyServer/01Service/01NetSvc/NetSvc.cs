using PENet;
using PEProtocol;
using System.Collections.Generic;
using System.Collections.Generic;

public class NetSvc
{
    private static NetSvc instance = null;
    public static NetSvc Instance
    {
        get{
            if (instance == null)
            {
                instance = new NetSvc();
            }
            return instance;
        }
    }
    public static readonly string obj = "lock";
    private Queue<GameMsg> msgQueue = new Queue<GameMsg>();

    public void Init()
    {
        PESocket<ServerSession, GameMsg> server = new PESocket<ServerSession, GameMsg>();
        server.StartAsServer(SrvCfg.srvIP, SrvCfg.srvPort);

        PETool.LogMsg("NetSvc Init Done");


     }

    public void AddMsgQue(GameMsg msg)
    {
        lock (obj)
        {
            msgQueue.Enqueue(msg);
        }
    }

    public void update()
    {
        if (msgQueue.Count > 0)
        {
            PECommon.Log("packageCountr:" + msgQueue.Count);
            lock (obj)
            {
                GameMsg msg = msgQueue.Dequeue();

            }
        }
    }

    private void HandoutMsg(GameMsg msg)
    {
        switch ((CMD) msg.cmd)
        {
            case CMD.None:

                break;
        }
       
    }

}

