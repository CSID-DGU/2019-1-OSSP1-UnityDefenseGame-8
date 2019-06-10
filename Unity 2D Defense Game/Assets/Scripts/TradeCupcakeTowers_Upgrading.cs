using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowers_Upgrading : TradeCupcakeTowers
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (currentActiveTower == null)
            return;
        if (currentActiveTower.IsUpgradable && currentActiveTower.upgradingCost <= sugarMeter.getSugarAmount())
        {
            sugarMeter.ChangeSugar(-currentActiveTower.upgradingCost);
            currentActiveTower.Upgrade();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
