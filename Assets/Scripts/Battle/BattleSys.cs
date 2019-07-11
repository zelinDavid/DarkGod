using UnityEngine;

public class BattleSys : SystemRoot {
    private int fbid;
    // public PlayerCtrlWnd playerCtrlWnd;
    
    public override void InitSystem() {
        base.InitSystem();

    }

    //创建新的scene.并且初始化各种东西.
    public void loadScene(int mapID) {
        GameObject go = new GameObject("BattleRoot");
        go.transform.SetParent(GameRoot.Instance.transform);

        BattleMgr battle = go.AddComponent<BattleMgr>();
        battle.Init(mapID);

    }

}