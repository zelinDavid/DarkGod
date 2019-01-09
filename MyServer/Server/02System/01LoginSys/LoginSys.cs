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
   
}
