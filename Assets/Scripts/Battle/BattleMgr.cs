using UnityEngine;

public class BattleMgr : MonoBehaviour {
    //你想怎么写都可以, 给的空间够大, 够刺激吧..
    SkillMgr skillMgr;
    StateMgr stateMgr;

    public void Init(int id) {
        skillMgr =  gameObject.AddComponent<SkillMgr>();
        stateMgr =  gameObject.AddComponent<StateMgr>();
         
        
    }
}