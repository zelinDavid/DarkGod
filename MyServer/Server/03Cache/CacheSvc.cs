/****************************************************
	文件：CacheSvc.cs
	作者：SIKI学院——Plane
	邮箱: 1785275942@qq.com
	日期：2018/12/09 5:38   	
	功能：缓存层
*****************************************************/

using System.Collections.Generic;
using PEProtocol;

public class CacheSvc {
    private static CacheSvc instance = null;
    public static CacheSvc Instance {
        get{
            if (instance == null )
            {
                instance = new CacheSvc();
            }
            return instance;
        }
    }

    public void Init() {
        PECommon.Log("CacheSvc Init Done.");
    }

    //private Dictionary<string, ServerSession> onLineAcctDic = new Dictionary<string, ServerSession>();
    private Dictionary<ServerSession, PlayerData> onLineSessionDic = new Dictionary<ServerSession, PlayerData>();
    private Dictionary<string, ServerSession> onLineAcctDic = new Dictionary<string, ServerSession>();

    public bool IsAcctOnLine(string acct){
        return onLineAcctDic.ContainsKey(acct;
    }


    //public bool IsAcctOnLine(string acct) {
    //    return onLineAcctDic.ContainsKey(acct);
    //}

    /// <summary>
    /// 根据账号密码返回对应账号数据，密码错误返回null，账号不存在则默认创建新账号
    /// </summary>
    public PlayerData GetPlayerData(string acct, string pass) {
        //TODO
        //从数据库中查找账号数据
        return null;
    }

    /// <summary>
    /// 账号上线，缓存数据
    /// </summary>
    public void AcctOnline(string acct, ServerSession session, PlayerData playerData) {
        onLineAcctDic.Add(acct, session);
        onLineSessionDic.Add(session, playerData);
    }
}
