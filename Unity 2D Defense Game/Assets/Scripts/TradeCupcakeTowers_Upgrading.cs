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

    }
    */
    
    // Update is called once per frame
    void Update()
    {
        if (currentActiveTower == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        
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
