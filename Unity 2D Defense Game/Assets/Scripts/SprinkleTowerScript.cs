using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinkleTowerScript : CupcakeTowerScript
{
    override public void Upgrade()
    {
        //Check if the tower is upgradable
        if (!IsUpgradable)
        {
            return;
        }

        //Increase the level of the tower
        upgradeLevel++;

        //Increase the stats of the tower
        rangeRadius += 10f;
        reloadTime -= 0.1f;

        //Change graphics of the tower
        currentSpriteRenderer.sprite = upgradeSprites[upgradeLevel];

        // 타워 업그레이드 비용과 판매시 얻는 가격이 늘어남
        sellingValue += 5;
        upgradingCost += 10;
    }
}
