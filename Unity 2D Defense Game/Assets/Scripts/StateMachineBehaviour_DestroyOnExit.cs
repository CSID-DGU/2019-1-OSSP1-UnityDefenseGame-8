using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBehaviour_DestroyOnExit : StateMachineBehaviour
{

    override public void OnStateExit(Animator Animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Animator에 Attach된 GameObject를 Destroy한다.
        // 즉, 판다가 발사체를 맞아서 죽거나, 너무 많이 먹어서 죽으면 Scene에서 판다를 삭제시킬 때 사용한다.
        Destroy(Animator.gameObject);
    }

}
