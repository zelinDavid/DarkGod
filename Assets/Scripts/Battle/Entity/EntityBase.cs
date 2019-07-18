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

    public BattleProps battleProp;
    public bool canReleaseSkill = true; //是否能释放技能
    public SkillCfg currentSkillCfg;
    private bool canControl = true;
    //组合动作技能,queue保存的是动作id.
    private Queue<int> comboQue = new Queue<int>();
    public int nextSkillID;

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

}