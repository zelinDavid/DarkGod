/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：#CreateTime#
	功能：网络通讯协议(客户端服务公用)
*****************************************************/

using System;
using PENet;

namespace PEProtocol
{
    [Serializable]   
    public class GameMsg: PEMsg
    {
        public ReqLogin reqLogin; 

    }

    [Serializable]
    public class ReqLogin
    {
        public string acct;
        public string pass;
    }


    public enum CMD
    {
        None = 0,
        ReqLogin = 101,
        RspLogin = 102,
    }

    public class SrvCfg
    {
        public const string srvIP = "127.0.0.1";
        public const int srvPort = 17666;
    }
        
}
    
 
