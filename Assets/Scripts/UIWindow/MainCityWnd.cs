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
    public Button btnGuide;

    private Vector3 startPos;
    private bool menuState = true;
    private AutoGuideCfg curtTaskData;

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
        
        Invoke("ClickBattleBtn", 1);
    }

    private void refreshUI() {
        /*
        从数据库加载基本信息
        
        经验进度条更新 TODO
        
        TODO: 设置任务图标更新
         */

        PlayerData pd = GameRoot.Instance.playerData;
        SetText(txtFight, PECommon.GetFightByProps(pd));
        SetText(txtPower, "体力:" + pd.power + "/" + PECommon.GetPowerLimit(pd.lv));
        imgPowerPrg.fillAmount = pd.power * 1.0f / PECommon.GetPowerLimit(pd.lv);
        SetText(txtLevel, pd.lv);
        SetText(txtName, pd.name);

        //更新任务图标
        curtTaskData = resSvc.GetAutoGuideCfg(pd.guideid);
        if (curtTaskData != null) {
            updateGuidIcon(curtTaskData.npcID);
        } else {
            updateGuidIcon(-1);
        }
    }

    private void updateGuidIcon(int npcID) {
        string spPath;
        switch (npcID) {
            case Constant.NPCWiseMan:
                spPath = PathDefine.WiseManHead;
                break;
            case Constant.NPCGeneral:
                spPath = PathDefine.GeneralHead;
                break;
            case Constant.NPCArtisan:
                spPath = PathDefine.ArtisanHead;
                break;
            case Constant.NPCTrader:
                spPath = PathDefine.TraderHead;
                break;
            default:
                spPath = PathDefine.TaskHead;
                break;
        }
        Image image = btnGuide.GetComponent<Image>();
        SetSprite(image, spPath);

    }

    public void ClickMenuBtn() {
        audioSvc.PlayUIAudio(Constant.UIExtenBtn);
        menuState = !menuState;
        string name = menuState ? "OpenMenu" : "CloseMenu";
        menuAni.Play(name);
    }

    private void RegisterTouchEvents() {
        OnClickDown(imgTouch.gameObject, (PointerEventData evt) => {
            startPos = evt.position;
            SetActive(imgDirPoint, true);
            imgDirBg.transform.position = evt.position;

        });
        OnDrag(imgTouch.gameObject, (PointerEventData evt) => {

            Vector3 destination = evt.position;
            float dis = (destination - startPos).magnitude;
            Vector3 dirction = (destination - startPos);
            if (dis > pointDis) {
                Vector3 tem = Vector3.ClampMagnitude(dirction, pointDis);
                imgDirPoint.transform.position = tem + startPos;

            } else {
                imgDirPoint.transform.position = destination;
            }
            //  Debug.Log("OnDrag dirction:" + dirction);
            MainCitySys.Instance.SetMoveDir(dirction.normalized);
        });
        OnClickUp(imgTouch.gameObject, (PointerEventData evt) => {
            imgDirBg.transform.position = defaulPos;
            SetActive(imgDirPoint, false);
            imgDirPoint.transform.localPosition = Vector3.zero;
            MainCitySys.Instance.SetMoveDir(Vector2.zero);
            // Debug.Log("OnClickUp dirction:");

        });

        OnClick(btnGuide.gameObject, (obj) => {
            if (curtTaskData == null || curtTaskData.npcID == -1) {
                Debug.Log("更多任务,敬请开发等待");
            }
            MainCitySys.Instance.guidWithCfg(curtTaskData);
        }, null);

    }

    public void ClickBattleBtn() {
        GameRoot.Instance.SendBattleMsg();
    }

}