using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 플레이어의 클릭을 감지
public abstract class TradeCupcakeTower : MonoBehaviour, IPointerClickHandler // 핸들러
{

    // 슈거미터를 저장하는 변수
    protected static SugarMeterScript sugarMeter;

    // 현재 활성화된 컵케이크 타워를 저장하는 변수
    protected static CupcakeTowerScript currentActiveTower;

    // Use this for initialization
    void Start()
    {
        //Debug.Log("first sugar:" + sugarMeter);
        // 슈거미터에 대한 참조가 없을 경우 직접 SugarMeterScript에서 참조
        if (sugarMeter == null)
        {
            //Debug.Log("sugar");
            sugarMeter = FindObjectOfType<SugarMeterScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 다른 스크립트가 선택된 타워를 할당받을 수 있도록 하는 함수
    // 여기서 함수를 생성하여 CupcakeTowerScript에서 사용하도록 한다.
    // 이거 먼저 생성됨

    // 5/19 수정사항 김민선
    public static void setActiveTower(CupcakeTowerScript cupcakeTower) // 해당 컵케이크 타워
    { 
        // Debug.Log("Cupcake 타워 선택");
        currentActiveTower = cupcakeTower;
        Debug.Log("Cupcake 타워 선택" + currentActiveTower);
    }
  
    // 거래 버튼을 누르면 활성화된다
    // 구매
    // 업그레이드
    // 판매
    public abstract void OnPointerClick(PointerEventData eventData);
}
