using System.Net.Mime;

using PEProtocol;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCityWnd : WindowRoot {

    float pointDis;
    Vector3 defaulPos;

    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;

    public Animation menuAni;
    public Button btnMenu;

    public Text txtFight;
    public Text txtPower;
    public Image imgPowerPrg;
    public Text txtLevel;
    public Text txtName;
    public Text txtExpPrg;

    private Vector3 startPos;

    protected override void InitWnd() {
        /*
            宽高比例
            默认遥感中间位置, 默认不激活
            注册监听事件
            刷新UI
         */
        base.InitWnd();
        pointDis = Screen.height * 1f / Constant.ScreenStandardHeight * Constant.ScreenOPDis;
        defaulPos = imgDirPoint.transform.position;
        SetActive(imgDirPoint, false);

        RegisterTouchEvents();
        refreshUI();
    }

    private void refreshUI() {
        /*
        从数据库加载基本信息
        
        经验进度条更新 TODO
        
        TODO: 设置任务图标更新
         */

        // PlayerData pd = GameRoot.Instance.playerData;
        // SetText(txtFight, PECommon.GetFightByProps(pd));
        // SetText(txtPower, "体力:" + pd.power + "/" + PECommon.GetPowerLimit(pd.lv));
        // imgPowerPrg.fillAmount = pd.power * 1.0f / PECommon.GetPowerLimit(pd.lv);
        // SetText(txtLevel, pd.lv);
        // SetText(txtName, pd.name);

    }

    private void RegisterTouchEvents() {
        OnClickDown(imgTouch.gameObject, (PointerEventData evt) => {
            startPos = evt.position;
            SetActive(imgDirPoint, true);
            imgDirBg.transform.position = evt.position;

        });
        OnDrag(imgTouch.gameObject, (PointerEventData data) => {
            //TODO:你上次写到这里
        });
        OnClickUp(imgTouch.gameObject, (PointerEventData data) => {

        });
    }

}