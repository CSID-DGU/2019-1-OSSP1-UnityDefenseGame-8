using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingCupcakeTowerScript : MonoBehaviour
{
    private NoCupcakeAreaScript noCupcakeArea;

    // Start is called before the first frame update
    void Start()
    {
        noCupcakeArea = FindObjectOfType<NoCupcakeAreaScript>();
        // 컵케이크 타워를 구매한 후 배치할 것이므로 영역 콜라이더들을 모두 활성화시킨다.
        noCupcakeArea.EnableColliders();
    }

    // 배치해야 할 컵케이크가 사용자의 마우스 포인터를 따라다닌다.
    // 마우스 클릭을 했을 경우, 컵케이크를 놓을 수 있는 영역인지 확인한 후 그곳에 놓는다.
    // 즉, 컵케이크타워 스크립트를 enable한 후 이 스크립트는 destroy 시킨다.
    void Update()
    {
        // 마우스 포인터의 x,y 좌표를 구한 뒤 컵케이크를 그곳으로 옮긴다 (즉, 마우스 포인터를 따라다니는 효과)
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 7));

        if(Input.GetMouseButtonDown(0) && noCupcakeArea.IsPointerOnAllowedArea())
        {
            // 초반에 disable 되어있던 컵케이크타워 스크립트를 enable 시킨다.
            GetComponent<CupcakeTowerScript>().enabled = true;
            // 초반에 disable 되어있던 boxCollider2D를 enable 시킨다.
            GetComponent<BoxCollider2D>().enabled = true;
            
            noCupcakeArea.DisableColliders();

            Destroy(this);
        }
    }
}
