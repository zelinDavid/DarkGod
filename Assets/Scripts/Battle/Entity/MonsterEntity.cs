using System;
using System.Collections;

using UnityEngine;

public class MonsterEntity : EntityBase {
    public MonsterData md;

    public MonsterEntity(BattleMgr battle, StateMgr state, SkillMgr skill, Controller control) : base(battle, state, skill, control) {
        entityType = EntityType.Monster;

        // TimeSvc.Instance.AddTimeTask((id) => {
        //     this.Attack(md.mCfg.skillID);
        // }, 2, PETimeUnit.Second, 1000);
    }

    public override void SetBattleProps(BattleProps props) {
        int level = md.mLevel;
        battleProp = new BattleProps {
            hp = props.hp * level,
            ad = props.ad * level,
            ap = props.ap * level,
            addef = props.addef * level,
            apdef = props.apdef * level,
            dodge = props.dodge * level,
            pierce = props.pierce * level,
            critical = props.critical * level
        };
        HP = props.hp;
    }

    private float checkTime = 2;
    private float checkCount = 0;
    private float attackInterval = 2;
    private float attackCount = 0;
    private bool isTrack = true;

    /*
        判断checktime
        true
            判断monster状态 
            idle move 
                判断player是否存在,且活着
                true
                    获取player方向.
                    判断是否在monster攻击范围内
                    true
                        判断攻击间隔
                        true
                            攻击
                            攻击计时间归零.
                        false
                            设置为idle状态.
                    false
                        朝target方向移动.
            other state
        false 

        每次将checkTime 随机计算一下增强游戏可玩性.

     */
    public void TrackPlayer() {
        if (!isTrack) { //发动攻击的时候,不进行track
            return;
        }
        if (battleMgr.playerEntity == null || battleMgr.playerEntity.currentAniState == AniState.Die)
        {
            isTrack = false;
            return;
        }
        checkCount += Time.deltaTime;
        attackCount += Time.deltaTime;
        if (checkCount < checkTime) {
            return;
        }
        checkCount = 0;
        checkTime = PETools.RDInt(1, 5) * 1.0f / 10;

        if (currentAniState == AniState.Idle || currentAniState == AniState.Move)
        {
            Vector2 dir = GetTargetDir();
            float dis = GetDistance();
            if (dis < md.mCfg.atkDis)
            {
                
                Debug.Log(attackCount + " " + attackInterval);
                if (attackCount < attackInterval)
                {
                    Idle();
                }else{
                    SetDirection(Vector2.zero);
                    controller.SetAtkRotationLocal(dir);
                    Attack(md.mCfg.skillID);
                    attackCount = 0;
                    Debug.Log("attack : " + md.mCfg.skillID);
                }
            }else{
                SetDirection(dir);
                Move();
            }
        }
    }

    private Vector2 GetTargetDir(){
        PlayerEntity player = battleMgr.playerEntity;
        Vector3 playerPos = player.GetPos();
        Vector3 monsterPos = GetPos();
        Vector2 dir =  new Vector2((playerPos.x - monsterPos.x), (playerPos.z - monsterPos.z));
        return dir.normalized;
    }

    private float GetDistance(){
       return Vector3.Magnitude(battleMgr.playerEntity.GetPos() - GetPos());
    }

    public override bool GetBreakState() {
        if (md.mCfg.isStop) {
            if (currentSkillCfg != null) {
                return currentSkillCfg.isBreak;
            }
            return true;
        } else {
            return false;
        }
    }

    public override void SetHurt(int damage) {
        GameRoot.Instance.dynamicWnd.SetHurt(GetName(), damage);
    }

}