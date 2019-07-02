using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoWnd : WindowRoot {
    #region UI Define
    public RawImage imgChar;

    public Text txtInfo;
    public Text txtExp;
    public Image imgExpPrg;
    public Text txtPower;
    public Image imgPowerPrg;

    public Text txtJob;
    public Text txtFight;
    public Text txtHP;
    public Text txtHurt;
    public Text txtDef;

    public Button btnClose;

    public Button btnDetail;
    public Button btnCloseDetail;
    public Transform transDetail;

    public Text dtxhp;
    public Text dtxad;
    public Text dtxap;
    public Text dtxaddef;
    public Text dtxapdef;
    public Text dtxdodge;
    public Text dtxpierce;
    public Text dtxcritical;
    #endregion

    private Vector2 startPos;
    private float rotateSpeedBuffer = 5;
    protected override void InitWnd(){
        base.InitWnd();
        RegitTouch();
    }

    private void RegitTouch(){
        OnClickDown(imgChar.gameObject,(PointerEventData eventData) =>{
            startPos = eventData.position;

        });

         OnDrag(imgChar.gameObject,(PointerEventData eventData) =>{
              Vector2 current = eventData.position;
              float deltaRotation =  -(current.x - startPos.x)* rotateSpeedBuffer;
              MainCitySys.Instance.setPlayerRotation(new Vector3(0,deltaRotation,0));
              startPos = current;
        });

    }

    public void clickCloseBtn(){
        SetWndState(false);
    }

}