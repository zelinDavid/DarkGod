/****************************************************
	文件：Class1.cs
	作者：SIKI学院——Plane
	邮箱: 1785275942@qq.com
	日期：2018/12/07 5:04   	
	功能：网络通信协议（客户端服务端共用）
*****************************************************/

using System;
using PENet;

namespace PEProtocol {
    [Serializable]
    public class GameMsg : PEMsg {
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
    }

    [Serializable]
    public class ReqLogin {
        public string acct;
        public string pass;
    }

    [Serializable]
    public class RspLogin {
        public PlayerData playerData;
    }

    [Serializable]
    public class PlayerData {
        //public int id;
        //public string name;
        //public int lv;
        //public int exp;
        //public int power;
        //public int coin;
        //public int diamond;

        public int id;
        public string name;
        public int lv;
        public int exp;
        public int power;
        public int coin;
        public int diamond;
    }

    //public enum ErrorCode {
    //    None = 0,//没有错误

    //    AcctIsOnline,//账号已经上线
    //    WrongPass,//密码错误
    //}

    public enum ErrorCode
    {
        None = 0, 
        AcctIsOnline, 
        WrongPass,
    }

    public enum CMD {
        None = 0,
        //登录相关 100
        ReqLogin = 101,
        RspLogin = 102,
    }

    public class SrvCfg {
        public const string srvIP = "127.0.0.1";
        public const int srvPort = 17666;
    }
}
