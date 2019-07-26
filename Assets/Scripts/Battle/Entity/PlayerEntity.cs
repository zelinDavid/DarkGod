

using UnityEngine;

public class PlayerEntity : EntityBase
{
    public PlayerEntity(BattleMgr battle,StateMgr state, SkillMgr skill,Controller controller):base(battle,state,skill,controller)
    {
        canReleaseSkill = true;
        canControl = true;
        entityType = EntityType.Player;
        RemoveSkillCB();
    }

      
   public override Vector2 GetDirInput(){
       if(!controller) {
           return Vector2.zero;
       }
       return controller.Dir;
    }

  
}