/****************************************************
	文件：ServerSession.cs
	作者：SIKI学院——Plane
	邮箱: 1785275942@qq.com
	日期：2018/12/07 5:09   	
	功能：网络会话连接
*****************************************************/

using PENet;
using PEProtocol;

public class ServerSession : PESession<GameMsg> {
    protected override void OnConnected() {
        PECommon.Log("Client Connect");
    }

    protected override void OnReciveMsg(GameMsg msg) {
        PECommon.Log("RcvPack CMD:" + ((CMD)msg.cmd).ToString());
        NetSvc.Instance.AddMsgQue(this, msg);
    }

    protected override void OnDisConnected() {
        PECommon.Log("Client DisConnect");
    }
}
