using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowers_Upgrading : TradeCupcakeTower
{
    


    /*
    // Use this for initialization
    void Start()
    {
        if (sugarMeter == null)
        {
            Debug.Log("sugar");
            sugarMeter = FindObjectOfType<SugarMeterScript>();
        }

        //gameObject.SetActive(true);
    }
    */

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("upgrading에서 현재 선택된 타워는" + currentActiveTower);

        /*
        if (currentActiveTower == null)
        {
            // 5/19 수정사항 김민선
            Debug.Log("현재 선택된 타워 없음"); // 여기서 오류 발생!!
            gameObject.SetActive(false);
            
}

        else // (currentActiveTower != null) // 5/23 추가
        {
            // 5/19 수정사항 김민선
            Debug.Log("현재 선택된 타워가 있음");
            gameObject.SetActive(true);
            
        }
    */
    }
    
    // 컵케이크 타워를 클릭하는게 아니고, 업그레이드 아이콘을 클릭하는 것
    public override void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("클릭");

        // 현재 타워가 업그레이드 가능한가? IsUpgradable로 수정
        if (currentActiveTower.IsUpgradable && currentActiveTower.upgradingCost <= sugarMeter.getSugarAmount())
            {
                // 결제 진행 - 플레이어의 슈거에서 금액 차감
                sugarMeter.ChangeSugar(-currentActiveTower.upgradingCost);
                // 업그레이드 된 컵케이크 타워 Prefab으로 변경
                currentActiveTower.Upgrade();
            }
        
    }
}
