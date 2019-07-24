public class MonsterEntity : EntityBase {
    public MonsterData md;

    public MonsterEntity(BattleMgr battle, StateMgr state, SkillMgr skill, Controller control) : base(battle, state, skill, control) {
        entityType = EntityType.Monster;
    }

    public override void SetBattleProps(BattleProps props) {
        int level = md.mLevel;
        props = new BattleProps {
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

    
}