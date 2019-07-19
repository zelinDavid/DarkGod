using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class StateMgr : MonoBehaviour {

    private Dictionary<AniState, IState> FSM = new Dictionary<AniState, IState>();
    public static StateMgr Instance;
    public void Init(){
        Instance = this;
        FSM.Add(AniState.Attack, new AttackState());
        FSM.Add(AniState.Born, new BornState());
        FSM.Add(AniState.Die, new DieState());
        FSM.Add(AniState.Hit, new HitState());
        FSM.Add(AniState.Idle, new IdleState());
        FSM.Add(AniState.Move, new MoveState());
        Debug.Log("StateMgr Init");
    } 

    public void ChangeStatus(EntityBase entity, AniState state, params object[] args) {
        if (entity.currentAniState == state) {
            return;
        }
        if (FSM.ContainsKey(state)) {
            if (entity.currentAniState != AniState.None) {
                FSM[entity.currentAniState].Exit(entity, args);
            }
            entity.currentAniState = state;
            FSM[state].Enter(entity, args);
            FSM[state].Process(entity, args);
        }
    }

}