using System.Collections.Generic;
using System.IO;

using UnityEngine;

public abstract class EntityBase {
    public AniState currentAniState;
    public EntityState entityState;
    public EntityType entityType;
    public StateMgr stateMgr;
    public SkillMgr skillMgr;
    public Controller controller;
    public BattleMgr battleMgr;

    public BattleProps battleProp;
    public bool canReleaseSkill = true; //是否能释放技能
    public SkillCfg currentSkillCfg;
    private bool canControl = true;
    //组合动作技能,queue保存的是动作id.
    private Queue<int> comboQue = new Queue<int>();
    public int nextSkillID;
    //技能唯一的回调time Task ID;
    private List<int> skMoveCBLst = new List<int>();
    //技能伤害计算回调time Task ID;
    private List<int> skActionCBLst = new List<int>();

    public int skillEndCB = -1;

    public void Idle() {
        stateMgr.ChangeStatus(this, AniState.Idle, null);
    }

    public void Move() {
        stateMgr.ChangeStatus(this, AniState.Move, null);
    }

    public void Attack(int skillID) {
        stateMgr.ChangeStatus(this, AniState.Attack, skillID);
    }

    public void SkillAttack(int skillID) {
        //skillMgr.attack() 
    }
    public void Hit() {
        stateMgr.ChangeStatus(this, AniState.Hit, null);
    }

    public void Born() {
        stateMgr.ChangeStatus(this, AniState.Born, null);
    }

    public void Die() {
        stateMgr.ChangeStatus(this, AniState.Die, null);
    }

    public void ExitCurtSkill() {
        canControl = true;
        if (!currentSkillCfg.isBreak) {
            entityState = EntityState.None;
        }
        if (currentSkillCfg.isCombo) {
            if (comboQue.Count > 0) {
                int id = comboQue.Dequeue();
                nextSkillID = id;
            } else {
                nextSkillID = 0;
            }
        }
        currentSkillCfg = null;
        SetAction(Constant.ActionDefault);
    }

    public virtual void SetAction(int action) {
        if (controller) {
            controller.SetAction(action);
        }
    }

    public void SetDirection(UnityEngine.Vector2 dir) {
        if (controller) {
            controller.Dir = dir;
        }

    }

    public virtual Vector2 GetDirInput() {
        return Vector2.zero;
    }

    public void SetBlend(float blend) {
        if (controller == null) {
            Debug.LogError("not have controller");
            return;
        }
        controller.SetBlend(blend);
    }

    /*
        人物禁止;
        清除技能移动状态;
        清除技能移动回调数组;
        清除动作回调数组;
        清除攻击结束回调.
        清除技能组合id数组;
        battleMgr. 技能释放记录清零, 组合技能index清零.
    */
    public void RemoveSkillCB() {
        if (entityType == EntityType.Player)
        {
           canReleaseSkill = false; 
        }
        SetDirection(Vector2.zero);
        controller.SetMove(false, 0);
        TimeSvc timer = TimeSvc.Instance;
        foreach (int item in skMoveCBLst) {
            timer.DelTask(item);
        }
        skMoveCBLst.Clear();

        foreach (int item in skActionCBLst) {
            timer.DelTask(item);
        }
        skActionCBLst.Clear();
        if (skillEndCB != -1) {
            timer.DelTask(skillEndCB);
            skillEndCB = -1;
        }

        comboQue.Clear();
        nextSkillID = 0;
        // battleMgr.lastAtkTime = 0;
        battleMgr.comIndex = 0;

    }

    public void SetActive(bool active) {
        if (controller == null) {
            Debug.LogError("not have controller");
            return;
        }
        controller.gameObject.SetActive(active);
    }

    public AnimationClip[] GetClips(){
        if(controller == null) return null;
        return  controller.ani.runtimeAnimatorController.animationClips;
    }
}