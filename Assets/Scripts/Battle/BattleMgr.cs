using System.Collections.Generic;
using PEProtocol;

using UnityEngine;

public class BattleMgr : MonoBehaviour {
    //你想怎么写都可以, 给的空间够大, 够刺激吧..
    SkillMgr skillMgr;
    StateMgr stateMgr;

    public float lastAtkTime = 0;
    public int comIndex = 0;
    public PlayerEntity playerEntity;
    public MapCfg mapCfg;
    private ResSvc resSvc;
    public void Init(MapCfg mapCfg) {
        this.mapCfg = mapCfg;
        InitData();
        skillMgr = gameObject.AddComponent<SkillMgr>();
        skillMgr.Init();
        stateMgr = gameObject.AddComponent<StateMgr>();
        stateMgr.Init();
        initPlayer();

        //TODO:暂时写到这里
        LoadMonsterByWaveID(1);
    }

    private void InitData() {
        resSvc = ResSvc.Instance;

    }

    /*
    初始化战斗相关属性.
    battle.controller,skill.state 
    */
    private void initPlayer() {
        Debug.Log("BattleSys.Instance.playerCtr" + BattleSys.Instance.playerCtr);
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
        
    }

    public void Attack(int skillID) {
        playerEntity.Attack(skillID);
    }

    //TODO:
    public Dictionary<string,MonsterEntity> monsterDic = new Dictionary<string, MonsterEntity>();
    public void LoadMonsterByWaveID(int wave) {
        foreach (MonsterData md in mapCfg.monsterLst) {
            if (md.mWave == wave) {
                //创建monster, 绑定monsterEntity对象. monsterEntity绑定对应的管理器及monsterController.
                GameObject go = resSvc.LoadPrefab(md.mCfg.resPath);
                go.transform.localPosition = md.mBornPos;
                go.transform.localEulerAngles = md.mBornRote;
                go.transform.localScale = Vector3.one;
                go.name = "m" + md.mWave + "_" + md.mIndex;
                // go.SetActive(false);
                MonsterController monsterCt = go.GetComponent<MonsterController>();
                monsterCt.Init();
                MonsterEntity monsterEntity = new MonsterEntity(this, stateMgr, skillMgr, monsterCt);
                monsterEntity.md = md;
                monsterEntity.SetBattleProps(md.mCfg.bps);
                monsterEntity.Born();
                monsterDic.Add(go.name, monsterEntity);
                if (md.mCfg.mType == MonsterType.Boss)
                {
                    
                }else if(md.mCfg.mType == MonsterType.Normal){
                    GameRoot.Instance.AddHpItemInfo(go.name,monsterCt.hpRoot,monsterEntity.battleProp.hp);
                }
            }
        }
        Debug.Log("monsters: " + monsterDic.Keys.Count);
    }
    
    private List<MonsterEntity> monsters = new List<MonsterEntity>();
    public List<MonsterEntity> GetMonsterList(){
        monsters.Clear();
        foreach (var item in monsterDic)
        {
            monsters.Add(item.Value);
        }
        return monsters;
    }

    public void RmMonsterByKey(string key){
        if (monsterDic.TryGetValue(key, out MonsterEntity monster))
        {
            monsterDic.Remove(key);
            GameRoot.Instance.dynamicWnd.RemoveHP(key);
        }
    }
    
    
}