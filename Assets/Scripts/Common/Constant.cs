/****************************************************
    文件：Constant.cs
	作者：BearYang
    日期：2019/1/8 17:9:15
	功能：Nothing
*****************************************************/
public enum MonsterType {
    None,
    Normal = 1,
    Boss = 2
}

public class Constant {

    //场景名称/ID
    public const string SceneLogin = "SceneLogin";
    public const int MainCityMapID = 10000;
    //public const string SceneMainCity = "SceneMainCity";

    //音效名称
    public const string BGLogin = "bgLogin";
    public const string BGMainCity = "bgMainCity";
    public const string BGHuangYe = "bgHuangYe";
    public const string AssassinHit = "assassin_Hit";

    //登录按钮音效
    public const string UILoginBtn = "uiLoginBtn";

    //常规UI点击音效
    public const string UIClickBtn = "uiClickBtn";
    public const string UIExtenBtn = "uiExtenBtn";
    public const string UIOpenPage = "uiOpenPage";
    public const string FBItemEnter = "fbitem";

    public const string FBLose = "fblose";
    public const string FBLogoEnter = "fbwin";

    //屏幕标准宽高
    public const int ScreenStandardWidth = 1334;
    public const int ScreenStandardHeight = 750;
    //摇杆点标准距离
    public const int ScreenOPDis = 90;

    //角色移动速度
    public const int PlayerMoveSpeed = 8;
    public const int MonsterMoveSpeed = 3;

    //混合参数
    public const int BlendIdle = 0;
    public const int BlendMove = 1;

    //运动平滑加速度
    public const float AccelerSpeed = 5;
    public static float AccelerHPSpeed = 0.3F;

    //普攻连招有效间隔
    public static int ComboSpace = 500;

}