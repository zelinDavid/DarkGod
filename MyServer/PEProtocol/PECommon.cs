/****************************************************
    文件：#SCRIPTNAME#.cs
   作者：Plane
    邮箱: 1785275942@qq.com
    日期：#CreateTime#
   功能：客户端服务端公用工具类
*****************************************************/

using PENet;

public enum LogType
{
    Log = 0,
    Warn =1, 
    Error = 2,
}

public class PECommon
{

    public static void Log(string msg = "", LogType tp = LogType.Log)
    {
        LogLevel lv = (LogLevel)tp;
        PETool.LogMsg(msg, lv);
    }
}

