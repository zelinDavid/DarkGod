using PEProtocol;

using UnityEngine;

public class BattleMgr : MonoBehaviour {
    //你想怎么写都可以, 给的空间够大, 够刺激吧..
    SkillMgr skillMgr;
    StateMgr stateMgr;

    public float lastAtkTime = 0;
    public int comIndex = 0;
    public PlayerEntity playerEntity;

    public void Init(int id) {
        skillMgr = gameObject.AddComponent<SkillMgr>();
        stateMgr = gameObject.AddComponent<StateMgr>();
        stateMgr.Init();
        initPlayer();
    }
    /*
    初始化战斗相关属性.
    battle.controller,skill.state 
    */
    private void initPlayer() {
        Debug.Log("BattleSys.Instance.playerCtr"+ BattleSys.Instance.playerCtr);
        playerEntity = new PlayerEntity(this, stateMgr, skillMgr, BattleSys.Instance.playerCtr);
        playerEntity.name = Constant.PlayerEntityName;

        PlayerData pd = GameRoot.Instance.playerData;
        BattleProps props = new BattleProps {
            hp = pd.hp,
            ad = pd.ad,
            ap = pd.ap,
            addef = pd.addef,
            apdef = pd.apdef,
            dodge = pd.dodge,
            pierce = pd.pierce,
            critical = pd.critical
        };
        playerEntity.SetBattleProps(props);
        //初始化装填
        Invoke("ddd",5);
        playerEntity.Born();
    }
    public void ddd(){
        
    }
}