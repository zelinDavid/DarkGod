using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCtrWnd : WindowRoot {
    //TODO: 您上次写到这里,可以开始UI搭建及 战斗逻辑梳理.
    float pointDis;
    Vector3 defaulPos;
    private Vector3 startPos;

    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;

    public Button firstSkill;
    public Button secondSkil;
    public Button thirdSkill;
    public Button normalAttack;

    public Image  energy;
    public Image blood;
    public Text bloodText;
    public Text level;
    
    public Text bossText;
    public Image bossImg;

    protected override void InitWnd(){
        RegisterTouchEvents();
         SetActive(imgDirPoint, false);
    }

    private void RegisterTouchEvents() {
        OnClickDown(imgTouch.gameObject, (PointerEventData evt) => {
            startPos = evt.position;
            SetActive(imgDirPoint, true);
            imgDirBg.transform.position = evt.position;
            pointDis = Screen.height * 1f / Constant.ScreenStandardHeight * Constant.ScreenOPDis;
            defaulPos = imgDirBg.transform.position;
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
            // MainCitySys.Instance.SetMoveDir(dirction.normalized);
            BattleSys.Instance.SetDir(dirction.normalized);
        });

        OnClickUp(imgTouch.gameObject, (PointerEventData evt) => {
            imgDirBg.transform.position = defaulPos;
            SetActive(imgDirPoint, false);
            imgDirPoint.transform.localPosition = Vector3.zero;
            BattleSys.Instance.SetDir(Vector2.zero);
            // MainCitySys.Instance.SetMoveDir(Vector2.zero);

            // Debug.Log("OnClickUp dirction:");
        });

        firstSkill.onClick.AddListener(() => {
            Debug.Log("firstSkill");
        });

        secondSkil.onClick.AddListener(() => {
            Debug.Log("firstSkill");

        });
        thirdSkill.onClick.AddListener(() => {

        });

        normalAttack.onClick.AddListener(() => {

        });

    }
}