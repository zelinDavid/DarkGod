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
    private float scaleRate = 1.0f * Constant.ScreenStandardHeight / Screen.height;
    private float targetBlood;
    private float curBlood;

    public void Init(Transform trans, int hp) {
        rootTrans = trans;
        hpCount = hp;
        curBlood = 1;
        hpRed.fillAmount = 1;
        hpGray.fillAmount = 1;
        rect = GetComponent<RectTransform>();
    }

    private void Update() {
        UpdatePos();
        UpdateBlood();
    }

    private void UpdatePos() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(rootTrans.position);
        rect.anchoredPosition = screenPos * scaleRate;
    }

    private void UpdateBlood() {
        float speed = Constant.AccelerHPSpeed * Time.deltaTime;
        if (Mathf.Abs(targetBlood - curBlood) < speed) {
            curBlood = targetBlood;
        } else if (targetBlood > curBlood) {
            curBlood += curBlood + speed;
        } else {
            curBlood -= curBlood + speed;
        }
        hpGray.fillAmount = curBlood;
    }

    public void SetHpVal(int oldVal, int newVal) {
        float newRate = newVal / hpCount;
        targetBlood = newRate;
        hpRed.fillAmount = targetBlood;
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
    }

    

}