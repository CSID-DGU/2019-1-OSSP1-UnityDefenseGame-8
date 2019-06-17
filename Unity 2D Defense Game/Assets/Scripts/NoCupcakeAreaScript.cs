using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 컵케이크를 놓을 수 없는 콜라이더 영역을 관리하는 게임오브젝트의 스크립트.
public class NoCupcakeAreaScript : MonoBehaviour
{
    BoxCollider2D [] colliders;
    private bool _isPointerOnAllowedArea;

    // Start is called before the first frame update
    void Start()
    {
        _isPointerOnAllowedArea = true;
        colliders = GetComponents<BoxCollider2D>();
        // 처음 콜라이더들은 disable 상태로 초기화되며, 플레이어가 컵케이크 타워를 구매하고 배치를 시도할 동안에만 enable 된다.
        DisableColliders();
    }

    // 모든 차일드 콜라이더들을 enable 시킨다.
    public void EnableColliders()
    {
        foreach(var c in colliders)
        {
            c.enabled = true;
        }
    }

    // 모든 차일드 콜라이더들을 disable 시킨다.
    public void DisableColliders()
    {
        foreach (var c in colliders)
        {
            c.enabled = false;
        }
    }

    // OnMouseEnter는 마우스 포인터가 처음 콜라이더에 들어왔을 때 호출된다. 
    private void OnMouseEnter()
    {
        _isPointerOnAllowedArea = false;
    }

    // OnMouseEnter를 보완하기 위해 OnMouseOver 또한 구현한다.
    private void OnMouseOver()
    {
        _isPointerOnAllowedArea = false;
    }

    private void OnMouseExit()
    {
        _isPointerOnAllowedArea = true;
    }

    public bool IsPointerOnAllowedArea()
    {
        return _isPointerOnAllowedArea;
    }
}
