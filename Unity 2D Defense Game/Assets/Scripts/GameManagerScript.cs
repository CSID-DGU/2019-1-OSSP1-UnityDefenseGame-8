using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Waypoint firstWaypoint;  // 판다가 따라갈 경로의 첫 번째 웨이포인트

    // 플레이어의 현재 체력(헬스)
    private HealthBarScript playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        // 게임 딱 처음에 플레이어의 현재 체력을 가져온다
        playerHealth = FindObjectOfType<HealthBarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //**************************컵케이크 타워 배치***********************

    // 컵케이크 타워를 배치할 수 있는 영역에 마우스가 있는지 확인
    private bool _isPointerOnAllowedArea = true;

    // 가능한 영역에 존재
    public bool isPointerOnAllowedArea()
    {
        return _isPointerOnAllowedArea;
    }

    // 마우스가 콜라이더 내 존재
    void OnMouseEnter()
    {
        // 컵케이크 타워 배치 가능
        _isPointerOnAllowedArea = true;
    }

    // 마우스가 콜라이더 밖 존재
    void OnMouseExit()
    {
        // 컵케이크 타워 배치 불가능
        _isPointerOnAllowedArea = false;
    }

    //*******************************게임 오버 조건***********************

    // 플레이어가 게임에서 패배했을 때의 Sprite
    public GameObject losingScreen;

    // 플레이어가 게임에서 승리했을 때의 Sprite
    public GameObject winningScreen;

    private void GameOver(bool playerHasWon)
    {
        // 플레이어 승리시
        if (playerHasWon)
        {
            // 설정했던 승리 Sprite 디스플레이
            winningScreen.SetActive(true);
        }
        // 플레이어 패배시
        else
        {
            // 설정했던 패배 Sprite 디스플레이
            losingScreen.SetActive(true);
        }

        // 게임이 종료되었으므로 time = 0
        Time.timeScale = 0;
    }

    //***********게임 진행 상황 추적***************************

    // 현재 있는 판다수(물리쳐야 하는 것)
    private int numberOfPandasToDefeat;

    // 판다가 죽을 때마다 1 감소
    public void OneMorePandaInHeaven()
    {
        numberOfPandasToDefeat--;
    }

    // 판다가 플레이어의 케이크에 도착했을 떄 플레이어에게 데미지를 줌
    // 게임 오버 조건에 만족하면 게임 오버 시킴
    public void BiteTheCake(int damage)
    {
        // 판다가 플레이어의 케이크를 전부 먹었는지 확인하는 함수
        // 전부 먹었으면 게임 오버 조건에 만족된다
        bool IsCakeAllEaten = playerHealth.ApplyDamage(damage);
        // 판다가 플레이어의 케이크를 전부 먹었다면 게임은 종료된다(패배 조건)
        if (IsCakeAllEaten)
        {
            GameOver(false);
        }
        // (그렇지 않을 경우) 케이크를 먹은 판다는 폭발해 버리므로 판다 수 1 감소
        OneMorePandaInHeaven();
    }

}
