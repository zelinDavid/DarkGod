/****************************************************
    文件：ClientSession.cs
	作者：SIKI学院——Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/7 5:22:14
	功能：客户端网络会话
*****************************************************/

using PENet;
using PEProtocol;

 

public class ClientSession:PESession<GameMsg>
{
    private PESocket<ClientSession, GameMsg> socket;
    void Init()
    {
        socket = new PESocket<ClientSession, GameMsg>();
        socket.StartAsClient("ip", 333);
    }
}