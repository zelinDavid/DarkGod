using System.Collections.Generic;

using UnityEngine;

public class SkillMgr : MonoBehaviour {

    /*  
        init,初始化属性;

        skillAttack{
            清除entity中的相关信息. 
            技能表现效果

            计算伤害
                如果是瞬时技能,则直接计算伤害. 如果有技能延时则在动作结束后计算伤害。
        }
     */
    private TimeSvc timer;
    private List<MonsterEntity> monsters;

    public void Init() {
        timer = TimeSvc.Instance;

    }

    public void SkillAttack(EntityBase entity, int skillID) {
        entity.skActionCBLst.Clear();
        entity.skMoveCBLst.Clear();
        entity.skillEndCB = -1;

        AttackEffect(entity, skillID);
        
    }

    /*
     是否忽略撞击
     是否是player,如果是则根据技能方向调整释放技能方向
     释放技能状态
     释放特效
     声音
     计算技能移动距离
     是否可控制
     移动方向清零
     是否霸体
     技能释放结束后回调.
     */
    public void AttackEffect(EntityBase entity, int skillID) {
        SkillCfg skillCfg = ResSvc.Instance.GetSkillCfg(skillID);
        if (skillCfg.isCollide == false) {
            Physics.IgnoreLayerCollision(9, 10);
            timer.AddTimeTask((delta) => {
                Physics.IgnoreLayerCollision(9, 10, false);
            }, skillCfg.skillTime);
        }

        if (entity.entityType == EntityType.Player) {
            if (entity.GetDirInput() == Vector2.zero) { //没有方向输入,计算怪物位置,并且发出攻击.

            } else { //向某个方向放技能
                Vector2 dir = entity.GetDirInput();
                entity.controller.SetAtkRotationCam(dir);
            }
        }

        entity.SetAction(skillCfg.aniAction);
        entity.controller.SetFX(skillCfg.fx, skillCfg.skillTime); //这里已经有特效音乐了

        CalMoveDistance(entity, skillID);
        entity.canControl = false;
        //当将direction设置为zero时, controller.move = false;
        entity.SetDirection(Vector2.zero);

        if (skillCfg.isBreak == false)
        {
            entity.entityState = EntityState.BatiState;
        }

        entity.skillEndCB = timer.AddTimeTask((timeID) =>{
            entity.Idle();
            entity.skillEndCB = -1;
            Debug.Log("skillEndCB: ");
        }, skillCfg.skillTime);

    }

    private void CalMoveDistance(EntityBase entity, int skillID) {
        // SkillMoveCfg moveCfg = ResSvc.Instance.GetSkillMoveCfg(skillID);
        SkillCfg skillCfg = ResSvc.Instance.GetSkillCfg(skillID);
        if (skillCfg.skillMoveLst == null || skillCfg.skillMoveLst.Count == 0) {
            return;
        }

        int sum = 0;
        foreach (int item in skillCfg.skillMoveLst) {
            SkillMoveCfg moveCfg = ResSvc.Instance.GetSkillMoveCfg(item);
            sum += moveCfg.delayTime;
            float dis = moveCfg.moveDis;
            float time = moveCfg.moveTime;
            float speed = dis / (time / 1000);
            if (sum > 0) {
                int timeid = TimeSvc.Instance.AddTimeTask((tid) => {
                    entity.controller.SetSkillMove(true, speed);
                    entity.RmvMoveCB(tid);
                }, sum);
                entity.skActionCBLst.Add(timeid);
            } else {
                entity.controller.SetSkillMove(true, speed);
            }
            sum += moveCfg.moveTime;

            int timeid1 = TimeSvc.Instance.AddTimeTask((tid) => {
                entity.controller.SetSkillMove(false, 0);
                entity.RmvMoveCB(tid);
            }, sum);
            entity.skActionCBLst.Add(timeid1);
        }

    }

    private void Move(SkillMoveCfg moveCfg, EntityBase entity) {

    }

    public Vector2 getCloseMonsterDir() {
        return Vector2.zero;
    }

}