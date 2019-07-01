using System.Collections.Generic;
/**************************************************** 	
	功能：表现实体控制器抽象基类
*****************************************************/

using UnityEngine;

public class Controller : MonoBehaviour {
    /*
        属性:characterController, isMove animator, transform , dir,  skillMove, fxDic.
     */

    public Animator ani;
    public CharacterController ctrl;
    public Transform hpRoot;

    public bool isMove { get; private set; } = false;

    private Vector2 dir = Vector2.zero;
    public Vector2 Dir {
        get {
            return dir;
        }
        set {
            isMove = value != Vector2.zero;
            dir = value;
            //Debug.Log("ISMove:" + isMove);
        }
    }
    protected Transform camTrans;

    protected bool skillMove = false;
    protected float skillMoveSpeed = 0f;

    public Dictionary<string, GameObject> fxDic = new Dictionary<string, GameObject>();
    protected TimeSvc timerSvc;

    public virtual void Init() {
        timerSvc = TimeSvc.Instance;
       ani.SetFloat("Blend", Constant.BlendIdle);
    }
    public virtual void SetBlend(float blend) {
        ani.SetFloat("Blend", blend);
    }
    public virtual void SetAction(int act) {
        ani.SetInteger("Action", act);
    }
    public virtual void SetFX(string name, float destory) {

    }

    public virtual void SetMove(bool move, float speed) {
        isMove = move;
        skillMoveSpeed = speed;
        // Debug.Log("ISMove:" + isMove);
    }

    public virtual void SetAtkRotationLocal(Vector2 atkDir) {
        float angle = Vector2.SignedAngle(atkDir, new Vector2(0,1));
        transform.localEulerAngles = new Vector3(0,angle, 0);
    }
    public virtual void SetAtkRotationCam(Vector2 camDir) {
        float angle = Vector2.SignedAngle(camDir, new Vector2(0,1)) + camTrans.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(0,angle, 0); 
    }
}