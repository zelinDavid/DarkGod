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
    public Image cd1;
    public Text cd1Text;
    private float sk1CDTime;

    public Button secondSkil;
    public Image cd2;
    public Text cd2Text;
    private float sk2CDTime;

    public Button thirdSkill;
    public Image cd3;
    public Text cd3Text;
    private float sk3CDTime;

    public Button normalAttack;

    public Image energy;
    public Image blood;
    public Text bloodText;
    public Text level;

    public Text bossText;
    public Image bossImg;

    public Button  playerHead;

    private bool isSkill1CD;
    private bool isSkill2CD;
    private bool isSkill3CD;

    private float sk1TimeCount = 0;
    private float sk2TimeCount = 0;
    private float sk3TimeCount = 0;

    private float sk1FillCount = 0;
    private float sk2FillCount = 0;
    private float sk3FillCount = 0;

    private float sk1Num = 0;
    private float sk2Num = 0;
    private float sk3Num = 0;

    private BattleSys battleSys;
 
    protected override void InitWnd() {
        base.InitWnd();
        SetActive(imgDirPoint, false);
        battleSys = BattleSys.Instance;
        sk1CDTime = resSvc.GetSkillCfg(101).cdTime / 1000.0f;
        sk2CDTime = resSvc.GetSkillCfg(102).cdTime / 1000.0f;
        sk3CDTime = resSvc.GetSkillCfg(103).cdTime / 1000.0f;
  
    }

    private void Start() {
        RegisterTouchEvents();
    }
    private void Update() {
        if (isSkill1CD) {
            sk1TimeCount += Time.deltaTime;
            sk1FillCount += Time.deltaTime;
            if (sk1TimeCount >= 1) {
                sk1TimeCount -= 1;
                sk1Num -= 1;

                if (sk1Num <= 0) {
                    SetActive(cd1, false);
                    isSkill1CD = false;
                } else {
                    SetText(cd1Text, sk1Num);
                }
            }
            cd1.fillAmount = 1 - sk1FillCount / sk1CDTime;
        }

        if (isSkill2CD) {
            sk2TimeCount += Time.deltaTime;
            sk2FillCount += Time.deltaTime;
            if (sk2TimeCount >= 1) {
                sk2TimeCount -= 1;
                sk2Num -= 1;

                if (sk2Num <= 0) {
                    SetActive(cd2, false);
                    isSkill2CD = false;
                } else {
                    SetText(cd2Text, sk2Num);
                }
            }
            cd2.fillAmount = 1 - sk2FillCount / sk2CDTime;
        }

        if (isSkill3CD) {
            sk3TimeCount += Time.deltaTime;
            sk3FillCount += Time.deltaTime;
            if (sk3TimeCount >= 1) {
                sk3TimeCount -= 1;
                sk3Num -= 1;

                if (sk3Num <= 0) {
                    SetActive(cd3, false);
                    isSkill3CD = false;
                } else {
                    SetText(cd3Text, sk3Num);
                }
            }
            cd3.fillAmount = 1 - sk3FillCount / sk3CDTime;
        }

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
            if (!isSkill1CD && CanReleaseSkill()) {
                SetActive(cd1, true);
                cd1.fillAmount = 1;
                SetText(cd1Text, sk1CDTime);
                sk1Num = sk1CDTime;
                sk1FillCount = 0;
                sk1TimeCount = 0;
                battleSys.Attack(0);
                isSkill1CD = true;
            }
        });

        secondSkil.onClick.AddListener(() => {
            if (!isSkill2CD && CanReleaseSkill()) {
                SetActive(cd2, true);
                cd2.fillAmount = 1;
                SetText(cd2Text, sk2CDTime);
                sk2Num = sk2CDTime;
                sk2FillCount = 0;
                sk2TimeCount = 0;
                battleSys.Attack(1);
                isSkill2CD = true;
            }
        });

        thirdSkill.onClick.AddListener(() => {
            if (!isSkill3CD && CanReleaseSkill()) {
                SetActive(cd3, true);
                cd3.fillAmount = 1;
                SetText(cd3Text, sk3CDTime);
                sk3Num = sk3CDTime;
                sk3FillCount = 0;
                sk3TimeCount = 0;
                battleSys.Attack(2);
                isSkill3CD = true;
            }
        });

        normalAttack.onClick.AddListener(() => {
            battleSys.Attack(3);
            Debug.Log("test2");
        });
 
        playerHead.onClick.AddListener(()=> {
            battleSys.DestoryBattle();
        });
    }

    private bool CanReleaseSkill() {
        bool ret = BattleSys.Instance.CanReleaseSkill();
        Debug.Log("canReleaseSkill:" + ret);
        return ret;

    }

}


// 抓紧时间学习吧, 锻炼, 学习.
