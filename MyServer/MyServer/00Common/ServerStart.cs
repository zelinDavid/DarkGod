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
	功能：服务器入口
*****************************************************/



class ServerStart
{
    static void Main(string[] args)
    {
        Console.WriteLine("main");
        ServerRoot.Instance.Init();
        while (true)
        {

        }
    }
}

