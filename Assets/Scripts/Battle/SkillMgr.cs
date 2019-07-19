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

 
    public void Init(){
        timer = TimeSvc.Instance;


    }   
    
    public void SkillAttack(EntityBase entity,int skillID){

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
    public void AttackEffect(EntityBase entity,int skillID){
        SkillCfg skillCfg = ResSvc.Instance.GetSkillCfg(skillID);
        if (skillCfg.isCollide == false)
        {
            Physics.IgnoreLayerCollision(9,10);
            timer.AddTimeTask((delta)=>{
                Physics.IgnoreLayerCollision(9,10,false);
            },skillCfg.skillTime);
        }

        if (entity.entityType == EntityType.Player)
        {
            if (entity.GetDirInput() == Vector2.zero)
            { //没有方向输入
                //TODO:写到这
            }else{ //向某个方向放技能

            }
        }
    }

    public Vector2 getCloseMonsterDir(){
        
        return Vector2.zero;
    }

}