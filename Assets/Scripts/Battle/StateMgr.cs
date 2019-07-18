using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class StateMgr : MonoBehaviour {

    private Dictionary<AniState, IState> FSM = new Dictionary<AniState, IState>();

    private void Start() {

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