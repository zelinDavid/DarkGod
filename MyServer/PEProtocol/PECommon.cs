/****************************************************
	文件：PECommon.cs
	作者：SIKI学院——Plane
	邮箱: 1785275942@qq.com
	日期：2018/12/07 5:56   	
	功能：客户端服务端共用工具类
*****************************************************/

using PENet;

public enum LogType {
    Log = 0,
    Warn = 1,
    Error = 2,
    Info = 3
}

public class PECommon {


    //public static void Log(string msg = "", LogType tp = LogType.Log) {
    //    LogLevel lv = (LogLevel)tp;
    //    PETool.LogMsg(msg, lv);
    //}

    public static void Log(string msg = "", LogType tp = LogType.Log){
        LogLevel lv = (LogLevel)tp;
        PETool.LogMsg(msg, lv);
    }

     

}
