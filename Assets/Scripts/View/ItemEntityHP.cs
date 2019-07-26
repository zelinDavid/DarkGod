using System.Net.Mime;

using UnityEngine;
using UnityEngine.UI;

public class ItemEntityHP : MonoBehaviour {
    /*
    各种血条.  暴击动画 文字动画
    scaleRate. 
    update ->更新UI位置 更新血量.
    初始化,重置血量状态
     */

    public Image hpRed;
    public Image hpGray;

    public Animation criticalAni;
    public Text txtCritical;

    public Animation hpAni;
    public Text txtHp;

    public Animation dodgeAni;
    public Text txtDodge;

    private RectTransform rect;
    private Transform rootTrans;
    private int hpCount;
    public float scaleRate = 1.0f * Constant.ScreenStandardHeight / Screen.height;
    private float targetBlood;
    private float curBlood;
    private float curBloodValue;
    public void Init(Transform trans, int hp) {
        hpCount = hp;
        curBloodValue = hp;
        curBlood = 1;
        targetBlood = 1;
        hpRed.fillAmount = 1;
        hpGray.fillAmount = 1;
        rect = GetComponent<RectTransform>();
        rootTrans = trans;
        
    }

    private void Update() {
        UpdatePos();
        UpdateBlood();
    }

    // public int count = 1;
    // public Vector3 rootTrans_position;

    public Transform rootTrnas;
    public float StanderHeight = Constant.ScreenStandardHeight;
    public float realHeight = Screen.height;
    private void UpdatePos() {

        float temScale =1.0f * Constant.ScreenStandardHeight / Screen.height;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(rootTrans.position);
        rect.anchoredPosition = screenPos * temScale;
        rootTrnas = rootTrans;
    }

    private void UpdateBlood() {
        // float speed = Constant.AccelerHPSpeed * Time.deltaTime;
        float speed = 0.014f;
        // Debug.Log("targetBlood curBlood" + targetBlood + " " +  curBlood );
        if (Mathf.Abs(targetBlood - curBlood) < speed) {
            curBlood = targetBlood;
        } else if (targetBlood > curBlood) {
            curBlood +=  speed;
        } else {
            curBlood -=  speed;
        }
        hpGray.fillAmount = curBlood;

        if (curBlood <= 0 && targetBlood <= 0) {

        }
    }

    
    public void SetCritical(int critical) {
        criticalAni.Stop();
        txtCritical.text = "暴击" + critical;
        criticalAni.Play();
    }

    public void SetDodge() {
        dodgeAni.Stop();
        txtDodge.text = "闪避";
        dodgeAni.Play();
    }

    public void SetHurt(int hurt) {
        hpAni.Stop();
        txtHp.text = "-" + hurt;
        hpAni.Play();

        curBloodValue -= hurt;
        curBloodValue = curBloodValue > 0 ? curBloodValue : 0;
        float newRate = curBloodValue / hpCount;
        targetBlood = newRate;
        hpRed.fillAmount = targetBlood;
        Debug.Log("targetBlood:" + (targetBlood < 0));
        if (targetBlood <= 0) {
            Destroy(this.gameObject);
        }
    }

}