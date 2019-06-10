using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowers_Selling : TradeCupcakeTowers
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (currentActiveTower == null)
            return;
        sugarMeter.ChangeSugar(currentActiveTower.GetComponent<CupcakeTowerScript>().sellingValue);
        Destroy(currentActiveTower.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
