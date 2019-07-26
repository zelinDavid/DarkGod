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
        SkillCfg skillCfg = ResSvc.Instance.GetSkillCfg(skillID);
        AttackEffect(entity, skillID);
        AttackDamage(entity, skillCfg, skillID);
    }

    /*
    根据技能获取对应的技能动作配置, 根据技能动作配置在对应的时间计算伤害.

    判断释放伤害对象
    if player then 
        遍历monsters: 判断它相对于人物是否在技能范围内, 是否角度在相关角度上. 
        如果在, 计算伤害:
            ad:
                是否miss;
                是否暴击, 及对应label显示.
                穿甲护甲计算.
                计算出总伤害.
            ap:

            else:

            计算出总伤害;
            血量是否低于总伤害: 是, 死亡及相关动画; 不是, 血量减少及相关动画.

    else 


    end 
     */

    private void AttackDamage(EntityBase entity, SkillCfg skillCfg, int skillID) {
        int sum = 0;
        for (int i = 0; i < skillCfg.skillActionLst.Count; i++) {
            int actionID = skillCfg.skillActionLst[i];
            SkillActionCfg actionCfg = ResSvc.Instance.GetSkillActionCfg(actionID);
            sum += actionCfg.delayTime;
            Debug.Log("i:" + i);
            int index = i;
            if (sum > 0) {
                int timeId = timer.AddTimeTask((timerid) => {
                    AttackActionDamage(entity, skillCfg, index);
                    // timer.DelTask(timerid);
                }, actionCfg.delayTime);
            } else {
                AttackActionDamage(entity, skillCfg, index);
            }
        }
    }

    //计算单个动作造成的伤害
    private void AttackActionDamage(EntityBase entity, SkillCfg skillCfg, int index) {
        int actionID = skillCfg.skillActionLst[index];

        SkillActionCfg actionCfg = ResSvc.Instance.GetSkillActionCfg(actionID);
        if (entity.entityType == EntityType.Player) {
            List<MonsterEntity> monsters = entity.battleMgr.GetMonsterList();
            foreach (MonsterEntity monster in monsters) {
                if (IsInRange(entity, monster, actionCfg) && IsInAngle(entity, monster, actionCfg)) {
                    CalDamage(entity, monster, skillCfg, index);
                }
            }
        } else if(entity.entityType == EntityType.Monster){
            if (IsInRange(entity, entity.battleMgr.playerEntity, actionCfg) && IsInAngle(entity, entity.battleMgr.playerEntity, actionCfg)) {
                    CalDamage(entity, entity.battleMgr.playerEntity, skillCfg, index);
                }
        }
    }

    private bool IsInRange(EntityBase entity, EntityBase target, SkillActionCfg cfg) {
        float range = cfg.radius;
        float dis = Vector3.Distance(entity.GetPos(), target.GetPos());
        return dis <= range;
    }

    private bool IsInAngle(EntityBase entity, EntityBase target, SkillActionCfg cfg) {
        Vector3 forward = entity.GetForward();
        Vector3 dir = Vector3.Normalize(target.GetPos() - entity.GetPos());
        float angle = Vector3.Angle(dir, forward);
        if (Mathf.Abs(angle) <= cfg.angle / 2) {
            return true;
        }
        return false;
    }

    /*
     如果在, 计算伤害:
            ad:
                是否miss;
                是否暴击, 及对应label显示.
                穿甲护甲计算.
                计算出总伤害.
            ap:

            else:

            计算出总伤害;
            血量是否低于总伤害: 是, 死亡及相关动画; 不是, 血量减少及相关动画.
     */
    private System.Random random = new System.Random();
    private void CalDamage(EntityBase entity, EntityBase target, SkillCfg cfg, int index) {

        DynamicWnd wnd = GameRoot.Instance.dynamicWnd;
        int damage = cfg.skillDamageLst[index];
        if (cfg.dmgType == DamageType.AD) {
            int rd = PETools.RDInt(1, 100, random);
            if (rd < 50) { //miss
                wnd.ShowDodge(target.GetName());
                return;
            }

            int baoji = PETools.RDInt(1, 100, random);
            if (baoji < 50) {
                float criticalRate = 1 + baoji / 100;
                damage += (int) (criticalRate * damage);
                wnd.ShowCritical(target.GetName(), damage);

            }
            // int addef = (int) (1 - entity.battleProp.pierce / 100) * target.battleProp.addef;
            int addef = 0;
            damage -= addef;

        } else if (cfg.dmgType == DamageType.AP) {
            //计算属性加成
            damage += entity.battleProp.ap;
            //计算魔法抗性
            damage -= target.battleProp.apdef;
        } else {}
        //TODO:warning test data
        if (entity.entityType == EntityType.Player) {
            damage = 100;
        }
        if (damage < 0) {
            damage = 0;
            return;
        }
 
        target.SetHurt(damage);
        target.battleProp.hp -= damage;
        //TODO:你上次写到这里
        
        if (damage >= target.battleProp.hp) {
            target.Die();
            if (target.entityType == EntityType.Monster) {
                BattleSys.Instance.RmMonsterByKey(target.GetName());

            } else if (target.entityType == EntityType.Player) {
                GameRoot.Instance.dynamicWnd.AddTips("战斗结束!");
                BattleSys.Instance.DestoryBattle();
                //TODO: 战斗结束逻辑
            }
        } else {
            if (target.entityState == EntityState.None && target.GetBreakState()) {
                target.Hit();
            }
        }
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

        if (skillCfg.isBreak == false) {
            entity.entityState = EntityState.BatiState;
        }

        entity.skillEndCB = timer.AddTimeTask((timeID) => {
            entity.Idle();
            entity.skillEndCB = -1;
            // Debug.Log("skillEndCB: ");
        }, skillCfg.skillTime);

        // Debug.Log("timer:  " + timer);
    }

    private void CalMoveDistance(EntityBase entity, int skillID) {
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