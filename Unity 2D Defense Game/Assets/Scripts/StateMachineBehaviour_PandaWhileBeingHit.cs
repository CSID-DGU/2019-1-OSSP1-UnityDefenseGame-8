using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBehaviour_PandaWhileBeingHit : StateMachineBehaviour
{
    // 애니메이터 컨트롤러가 부착된 판다 오브젝트의 판다스크립트
    public PandaScript pandascript = null;

    // 판다가 맞는 애니메이션에 들어왔을 때 moveable = false. 맞고 있는 동안 이동을 하지 못 하게 위해서이다.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 판다스크립트를 처음 지정하는 경우
        if (pandascript == null)
        {
            pandascript = animator.gameObject.GetComponent<PandaScript>();
        }
        pandascript.moveable = false;
    }

    // 판다가 맞는 애니메이션을 나갈 때 moveable = true. 이제 판다는 움직일 수 있다.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pandascript.moveable = true;
    }
}
