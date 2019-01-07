/****************************************************
    文件：GameRoot.cs
	作者：SIKI学院——Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/3 5:30:21
	功能：游戏启动入口
*****************************************************/

using UnityEngine;

public class GameRoot : MonoBehaviour {
    public static GameRoot Instance = null;

    private void Start()
    {
        DontDestroyOnLoad(this);

    }

  protected void Init()
    {
        LoginSys login = GetComponent<LoginSys>();

    }
}
}