using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowers_Buying : TradeCupcakeTowers
{
    public GameObject cupcakeTowerPrefab;

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Buy BUtton clicked");
        int price = cupcakeTowerPrefab.GetComponent<CupcakeTowerScript>().initialCost;
        if(price <= sugarMeter.getSugarAmount())
        {
            sugarMeter.ChangeSugar(-price);
            GameObject newTower = Instantiate(cupcakeTowerPrefab);
            currentActiveTower = newTower.GetComponent<CupcakeTowerScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
