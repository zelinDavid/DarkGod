 
using UnityEngine.UI;
using UnityEngine;

public class DieState : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
        entity.SetDirection(Vector2.zero);
        entity.RemoveSkillCB();
    }

    public void Exit(EntityBase entity, params object[] args)
    {
        
    }

    public void Process(EntityBase entity, params object[] args)
    {
         entity.SetAction(Constant.ActionDie);
         if (entity.entityType == EntityType.Monster)
         { 
             entity.controller.enabled = false;
             TimeSvc.Instance.AddTimeTask((deltaTime)=> {
                 entity.SetActive(false);
                 Object.Destroy(entity.controller.gameObject);
             },Constant.DieAniLength);
         }
    }
}