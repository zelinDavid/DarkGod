﻿/****************************************************
	文件：ServerRoot.cs
	作者：SIKI学院——Plane
	邮箱: 1785275942@qq.com
	日期：2018/12/07 4:36   	
	功能：服务器初始化
*****************************************************/

public class ServerRoot {
    private static ServerRoot instance = null;
    public static ServerRoot Instance {
        get {
            if (instance == null) {
                instance = new ServerRoot();
            }
            return instance;
        }
    }

    public void Init() {
        //数据层TODO

        //服务层
        CacheSvc.Instance.Init();
     

        //业务系统层
        LoginSys.Instance.Init();


    }

    //public void Update() {
    //    NetSvc.Instance.Update();
    //}

    public void Update(){
        NetSvc.Instance.Update();
    }
}
