/****************************************************
   文件：#SCRIPTNAME#.cs
   作者：Plane
   邮箱: 1785275942@qq.com
   日期：#CreateTime#
   功能：网络会话连接
*****************************************************/


using PEProtocol;
using PENet;

class ServerSession: PESession<GameMsg>
{
    protected override void OnConnected()
    {
        PETool.LogMsg("client Connect");
      
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PETool.LogMsg("client Req:"   );
        NetSvc.Instance.AddMsgQue(msg);
    }

    protected override void OnDisConnected()
    {
        PETool.LogMsg("client Disconnect");
 
    }

}
