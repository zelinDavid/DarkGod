using System;

using UnityEngine;

public class PlayerController : Controller {

    public GameObject daggerskill1fx;
    public GameObject daggerskill2fx;
    public GameObject daggerskill3fx;

    public GameObject daggeratk1fx;
    public GameObject daggeratk2fx;
    public GameObject daggeratk3fx;
    public GameObject daggeratk4fx;
    public GameObject daggeratk5fx;

    private Vector3 camOffset;

    private float targetBlend = 0;
    private float currentBlend = 0;

    /*
        Init, 初始化技能, 放到技能字典中.
        update中, 更新: 移动方向,移动速度, 摄像机位置, 技能移动.
    
     */
    public override void Init() {
        base.Init();
        camTrans = Camera.main.transform;

        if (daggerskill1fx != null) {
            fxDic.Add(daggerskill1fx.name, daggerskill1fx);
        }
        if (daggeratk2fx != null) {
            fxDic.Add(daggerskill2fx.name, daggerskill2fx);
        }
        if (daggeratk3fx != null) {
            fxDic.Add(daggerskill3fx.name, daggerskill3fx);
        }

        if (daggeratk1fx != null) {
            fxDic.Add(daggeratk1fx.name, daggeratk1fx);
        }
        if (daggeratk2fx != null) {
            fxDic.Add(daggeratk2fx.name, daggeratk2fx);
        }
        if (daggeratk3fx != null) {
            fxDic.Add(daggeratk3fx.name, daggeratk3fx);
        }
        if (daggeratk4fx != null) {
            fxDic.Add(daggeratk4fx.name, daggeratk4fx);
        }
        if (daggeratk5fx != null) {
            fxDic.Add(daggeratk5fx.name, daggeratk5fx);
        }

        camOffset = transform.position - camTrans.position;
      
    }

    private void CalcuOffset() {
        Debug.Log("camerans pos:" + camTrans.position);
    }

    private void SetCam() {
        Vector3 camPos = transform.position - camOffset;
        if (camTrans != null) {
            // Debug.Log("setCam:" + camPos);
            camTrans.position = camPos;
        }
    }

    private void Update() {
        //blend
        if (currentBlend != targetBlend) {
            UpdateMixBlend();
        }
        if (isMove) {
            SetDir();
            SetMove();
            SetCam();
        }

        if (skillMove) {
            SetSkillMove();
            SetCam();
        }
    }

    //暂时没搞明白为什么这么写;
    //answer:运动方向加上摄像机旋转方向, 使用人物移动方向始终和屏幕上下左右保持一致.
    private void SetDir() {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1)) + camTrans.eulerAngles.y;
        // float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1)) ;
        // Debug.Log("setDir:" + angle);
        Vector3 eulerAngles = new Vector3(0, angle, 0);
        transform.localEulerAngles = eulerAngles;
    }

    private void SetMove() {
        ctrl.Move(Time.deltaTime * transform.forward * Constant.PlayerMoveSpeed);
    }

    private void SetSkillMove() {
        ctrl.Move(transform.forward * Time.deltaTime * skillMoveSpeed);
    }

    private void UpdateMixBlend() {
        if (Mathf.Abs(currentBlend - targetBlend) < Time.deltaTime * Constant.AccelerSpeed) {
            currentBlend = targetBlend;
        } else if (currentBlend > targetBlend) {
            currentBlend -= Time.deltaTime * Constant.AccelerSpeed;
        } else {
            currentBlend += Time.deltaTime * Constant.AccelerSpeed;
        }
        ani.SetFloat("Blend", currentBlend);
    }

    public override void SetBlend(float blend) {
        targetBlend = blend;
        SetMove(blend == Constant.BlendMove, 0f);
    }

    public override void SetFX(string name, float destoryTime) {
        GameObject go;
        if (fxDic.TryGetValue(name, out go)) {
            go.SetActive(true);
            timerSvc.AddTimeTask((int tid) => {
                go.SetActive(false);
            }, destoryTime);
        }
    }
}