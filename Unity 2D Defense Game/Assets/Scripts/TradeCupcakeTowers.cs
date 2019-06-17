using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class TradeCupcakeTowers : MonoBehaviour, IPointerClickHandler
{
    protected static SugarMeterScript sugarMeter;
    protected static CupcakeTowerScript currentActiveTower;

    // Start is called before the first frame update
    void Start()
    {
        if(sugarMeter == null)
        {
            sugarMeter = FindObjectOfType<SugarMeterScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetActiveTower(CupcakeTowerScript cupcakeTower)
    {
        currentActiveTower = cupcakeTower;

        // 현재 선택한 타워의 업그레이드 비용 표시
        if(cupcakeTower.IsUpgradable)
            GameManagerScript.Instance.upgradeCostText.text = cupcakeTower.upgradingCost.ToString();
        else
            GameManagerScript.Instance.upgradeCostText.text = "MAX";    // 이미 풀 업그레이드 된 상태


        // 판매가격 표시
        GameManagerScript.Instance.sellingValueText.text = cupcakeTower.sellingValue.ToString();
    }

    public abstract void OnPointerClick(PointerEventData eventData);
}
