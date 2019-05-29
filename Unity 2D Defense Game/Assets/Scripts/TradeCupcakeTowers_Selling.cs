using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowers_Selling : TradeCupcakeTower

{

    public 
    /*
    // Use this for initialization
    void Start()
    {

    }
    */

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("selling에서 현재 선택된 타워는" + currentActiveTower);
        /*
        if (currentActiveTower == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        */
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        //GetComponents<BoxCollider2D>().enabled = false;
        //Debug.Log("왜?");
        //Debug.Log("현재 선택된 컵케이크 타워 냥냥: " + currentActiveTower);
        // 선택된 타워가 없을 경우
        if (currentActiveTower == null)
            return;

        // 컵케이크 타워를 판매하였으므로 슈거의 판매값을 더한다
        sugarMeter.ChangeSugar(currentActiveTower.sellingValue);
        // 게임에서 컵케이크 타워 GameObject 삭제

        // 5/23 수정
        // 여기서 오류가 났었다!
        // Destroy(currentActiveTower)은 그저 currentActiveTower의 CupcakeTowerScript만 삭제 해주는것
        // 우리가 필요한건 gameObject 삭제임!
        Destroy(currentActiveTower.gameObject); // 수정수정 5/23
        //GetComponents<BoxCollider2D>().enabled = true;
    }
    }
