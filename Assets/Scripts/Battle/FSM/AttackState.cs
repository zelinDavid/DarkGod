public class AttackState : IState {
    public void Enter(EntityBase entity, params object[] args) {
        int skillID = (int) args[0];
        entity.currentSkillCfg = ResSvc.Instance.GetSkillCfg(skillID);
        entity.canReleaseSkill = false;
    }

    public void Process(EntityBase entity, params object[] args) {
        entity.SkillAttack((int)args[0]);
    }

    public void Exit(EntityBase entity, params object[] args) {
        entity.ExitCurtSkill();
    }

}