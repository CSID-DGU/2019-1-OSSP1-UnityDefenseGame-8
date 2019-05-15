using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowers_Selling : TradeCupcakeTower
{

    // Use this for initialization
    void Start()
    {

    }
    /*
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
    */
    public override void OnPointerClick(PointerEventData eventData)
    {
        // 선택된 타워가 없을 경우
        if (currentActiveTower == null)
            return;

        // 컵케이크 타워를 판매하였으므로 슈거의 판매값을 더한다
        sugarMeter.ChangeSugar(currentActiveTower.sellingValue);
        // 게임에서 컵케이크 타워 GameObject 삭제
        Destroy(currentActiveTower);
    }
}
