using UnityEngine;

public class IdleState : IState {
    public void Enter(EntityBase entity, params object[] args) {
        // entity.dir
        entity.SetDirection(Vector2.zero);
    }

    public void Exit(EntityBase entity, params object[] args) {
        Debug.Log("idleState Exit");
    }

    public void Process(EntityBase entity, params object[] args) {
        if (entity.nextSkillID != 0) //组合技能动作
        {
            entity.Attack(entity.nextSkillID);
        } else {
            /*
                如果是playerEntity, 则改为可以释放技能.
                是否当前的direction == zero或者, 获取到的方向时zero, 如果是,则改为move状态.
                否则 blend,设置为idle.
             */
            if (entity.entityType == EntityType.Player) {
                entity.canReleaseSkill = true;
                // Debug.Log("IDle CanReleaseSkill");
            }
            // Debug.Log("entityType:" + (entity.entityType == EntityType.Player));


            if (entity.GetDirInput() != Vector2.zero) {
                entity.SetDirection(entity.GetDirInput());
                entity.Move();
            } else {
                entity.SetBlend(Constant.BlendIdle);
            }
        }
    }
}