using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：#CreateTime#
	功能：Nothing
*****************************************************/
  
 

public class ServerRoot
{
    private static ServerRoot instance = null;
    public static ServerRoot Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ServerRoot();
            }
            return instance;
        }
    }

    public void Init()
    {
        //数据层TODO

        //服务层
        //CacheSvc.Instance.Init();
        NetSvc.Instance.Init();

        //业务系统层
        LoginSys.Instance.Init();
    }
}
 