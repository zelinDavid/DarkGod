/****************************************************
	文件：LoginSys.cs
	作者：SIKI学院——Plane
	邮箱: 1785275942@qq.com
	日期：2018/12/07 4:41   	
	功能：登录业务系统
*****************************************************/

using PEProtocol;

public class LoginSys {
    private static LoginSys instance = null;
    public static LoginSys Instance {
        get {
            if (instance == null) {
                instance = new LoginSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc = null;

    public void Init() {
        cacheSvc = CacheSvc.Instance;
        PECommon.Log("LoginSys Init Done.");
    }

    public void ReqLogin(MsgPack pack) {
        //ReqLogin data = pack.msg.reqLogin;
        ////当前账号是否已经上线
        //GameMsg msg = new GameMsg {
        //    cmd = (int)CMD.RspLogin
        //};
        //if (cacheSvc.IsAcctOnLine(data.acct)) {
        //    //己上线：返回错误信息
        //    msg.err = (int)ErrorCode.AcctIsOnline;
        //}
        //else {
        ////未上线：
        ////账号是否存在 
        //PlayerData pd = cacheSvc.GetPlayerData(data.acct, data.pass);
        //if (pd == null) {
        //    //存在，密码错误
        //    msg.err = (int)ErrorCode.WrongPass;
        //}
        //else {
        //    msg.rspLogin = new RspLogin {
        //        playerData = pd
        //    };
        //    //缓存账号数据
        //    cacheSvc.AcctOnline(data.acct, pack.session, pd);
        //}
        //}
        ////回应客户端
        //pack.session.SendMsg(msg);


        //当前账号是否已经上线

        //已上线：返回错误信息

            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.RspLogin,
               
            };
        ReqLogin data = pack.msg.reqLogin;
        bool login = cacheSvc.IsAcctOnLine(data.acct);
        if (login)
        {
            msg.err = (int)ErrorCode.AcctIsOnline;

        }else
        {
            PlayerData pd = cacheSvc.GetPlayerData(data.acct, data.pass);
            if (pd == null)
            { //存在密码错误
                msg.err = (int)ErrorCode.WrongPass;

            }else {
                msg.rspLogin = new RspLogin
                {
                    playerData = pd
                };

                cacheSvc.AcctOnline(data.acct,pack.session, pd);
            }
        }

        //未上线：
        //账号是否存在
        //存在，检测密码
        //不存在，创建默认的账号密码

        //回应客户端


        pack.session.SendMsg(msg);
    }
}
