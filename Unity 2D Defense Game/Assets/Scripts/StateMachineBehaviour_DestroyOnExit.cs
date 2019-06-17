using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBehaviour_DestroyOnExit : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Destroy the gameobject where the animator is attached to
        Destroy(animator.gameObject);

        // 게임 매니저에서 죽은 판다 한 마리 추가
        GameManagerScript.Instance.OneMorePandaInHeaven();
    }

}
