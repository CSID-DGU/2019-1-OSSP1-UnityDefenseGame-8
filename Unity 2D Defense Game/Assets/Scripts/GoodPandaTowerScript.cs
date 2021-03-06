﻿using UnityEngine;
using System.Collections;

//SCRIPT VERSION - END OF CHAPTER 2
public class GoodPandaTowerScript : CupcakeTowerScript
{
    // 타워 stat 정보
    public float spawnRadius;           // 아군판다가 spawn될 수 있는 radius
    public GameObject GoodPandaPrefab; // 아군판다 기지에서 생성할 아군판다(GoodPanda) Prefab
    public int maxGoodPanda = 3;    // 최대 spawn가능한 아군판다 수
    public int curGoodPanda = 0;   // 현재 spawn되어있는 아군판다 수
    
    // 아군판다 유닛 stat 정보


    // 아군판다들을 radius 범위 내에서 랜덤한 위치에 spawn
    new void Start()
    {
        base.Start();

        // Max수만큼의 아군유닛을 처음 생성
        SpawnGoodPanda(maxGoodPanda);
        curGoodPanda = maxGoodPanda;
    }

    // 현재 아군판다 수가 num 수와 동일해 질 때 까지 기지 주변의 원을 두르면서 아군판다 생성
    public void SpawnGoodPanda(int num) {
        for(int i = 0; i < num; ++i) {
            Vector3 addv = new Vector3();
            float rad = Random.Range(0, Mathf.PI * 2);  // random radian angle

            // radius로 정의되는 원의 끝부분에서 랜덤한 위치 설정
            addv.x = Mathf.Cos(rad);
            addv.y = Mathf.Sin(rad);
            addv.Normalize();
            addv *= spawnRadius;
            addv.z = -1;    // 판다 유닛들은 -1의 z값을 가짐

            // 아군판다 생성
            GameObject GoodPandaUnit = GameObject.Instantiate(GoodPandaPrefab, transform.position + addv, Quaternion.identity) as GameObject;
            GoodPandaUnit.transform.SetParent(this.transform);
            }
    }

    override public void Upgrade()
    {
        //Check if the tower is upgradable
        if (!IsUpgradable)
        {
            return;
        }

        //Increase the level of the tower
        upgradeLevel++;

        //Change graphics of the tower
        currentSpriteRenderer.sprite = upgradeSprites[upgradeLevel];

        // 타워 업그레이드 비용과 판매시 얻는 가격이 늘어남
        sellingValue += 5;
        upgradingCost += 10;

        // 업그레이드 시 쿠키 유닛 수를 증가
        maxGoodPanda += 2;

        // 유닛 수를 증가시킨 만큼 스폰함
        SpawnGoodPanda(2);
        curGoodPanda = maxGoodPanda;
    }

    new void Hit()
    {

    }
}