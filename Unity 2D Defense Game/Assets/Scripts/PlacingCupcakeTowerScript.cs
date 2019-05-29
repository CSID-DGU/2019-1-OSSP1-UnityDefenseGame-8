using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//둘때만 enable하고 나머지는 disable로 둔다.

public class PlacingCupcakeTowerScript : MonoBehaviour
{
    private GameManagerScript gameManager;
    
    // Use this for initialization
    void Start()
    {
        ColliderEnableScript.ColliderControl.EnableColliders();
        gameManager = FindObjectOfType<GameManagerScript>();
        // 추가추가
        //GetComponents<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스의 위치 가져옴
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        // 마우스의 위치를 게임 내 위치로 변경
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 7));

        void OnCollisionEnter2D(Collision2D collision)
        {
            GetComponent<Collider>().enabled = false;
        }


        // 마우스가 컵케이크 타워를 배치할 수 있는 곳에 존재하는지 확인
        if (Input.GetMouseButtonDown(0) && ColliderEnableScript.ColliderControl.isPointerOnAllowedArea())
        {
            //GetComponents<Collider>().enabled = true;
            // CupcakeTowerScript 지금 False 해뒀는데,
            // True로 변경
            GetComponent<CupcakeTowerScript>().enabled = true;
            // 컵케이크 타워에 Collider 배치
            //gameObject.AddComponent<BoxCollider2D>();
            // 컵케이크 타워가 더 이상 마우스를 따라다니지 않도록

            //mapCollider.GetComponent<Collider2D>().enabled = false;

            //placing cupcake tower에 map collider을 주자. 그래서 그렇게하장
            ColliderEnableScript.ColliderControl.DisableColliders();
            Destroy(this);
            // enable 수정 필요 5/23
           //GetComponents<Collider>().enabled = false;
        }

    }
}
