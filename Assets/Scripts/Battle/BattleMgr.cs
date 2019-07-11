using UnityEngine;

public class BattleMgr : MonoBehaviour {
    //你想怎么写都可以, 给的空间够大, 够刺激吧..

    public void Init(int id) {

        /*
            初始化状态管理器;技能管理器;
         */

        
        MapCfg cfg = ResSvc.Instance.GetMapCfg(id);
        ResSvc.Instance.AsyncLoadScene(cfg.mapName, () => {
            //加载完战斗场景所需要做的事
            /*
                显示人物; 显示对应战斗场景UI.对应战斗控制器
            
             */
        });
    }
}