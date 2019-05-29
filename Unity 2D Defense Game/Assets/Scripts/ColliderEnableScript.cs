using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnableScript : MonoBehaviour
{
    public static ColliderEnableScript ColliderControl = null;
    GameObject thisGameObject;
    private bool _isPointerOnAllowedArea;

    // Start is called before the first frame update
    void Start()
    {
        thisGameObject = this.gameObject;
        if(ColliderControl == null)
            ColliderControl = this;
    }

    public void EnableColliders()
    {
        thisGameObject.SetActive(true);
    }

    public void DisableColliders()
    {
        thisGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool isPointerOnAllowedArea()
    {
        return _isPointerOnAllowedArea;
    }

    // 마우스가 콜라이더 내 존재
    void OnMouseEnter()
    {
        // 컵케이크 타워 배치 가능
        _isPointerOnAllowedArea = true;
        //GetComponents<Collider2>().enabled = false;
    }

    // 마우스가 콜라이더 밖 존재
    void OnMouseExit()
    {
        // 컵케이크 타워 배치 불가능
        _isPointerOnAllowedArea = false;
    }
}
