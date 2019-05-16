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
        Debug.Log("first sugar:" + sugarMeter);
        // 슈거미터에 대한 참조가 없을 경우 직접 SugarMeterScript에서 참조
        if (sugarMeter == null)
        {
            Debug.Log("sugar");
            sugarMeter = FindObjectOfType<SugarMeterScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 다른 스크립트가 선택된 타워를 할당받을 수 있도록 하는 함수
    public static void setActiveTower(CupcakeTowerScript cupcakeTower)
    {
        Debug.Log("할당");
        currentActiveTower = cupcakeTower;
    }

    // 거래 버튼을 누르면 활성화된다
    // 구매
    // 업그레이드
    // 판매
    public abstract void OnPointerClick(PointerEventData eventData);
}
