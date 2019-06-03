using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public Waypoint firstWaypoint;  // 판다가 따라갈 경로의 첫 번째 웨이포인트

    // 플레이어의 현재 체력(헬스)
    private HealthBarScript playerHealth;

    public SugarMeterScript sugarmeter; // 6/3 추가

    // Start is called before the first frame update
    void Start()
    {
        // 게임 딱 처음에 플레이어의 현재 체력을 가져온다
        playerHealth = FindObjectOfType<HealthBarScript>();

        spawner = GameObject.Find("Spawning Spot").transform; // 여기서 spawner을 2차로 사용하는 것
        //Debug.Log(spawner.position);

        StartCoroutine(WavesSpawner()); // 추가
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

  

    //*******************************게임 오버 조건***********************

    // 플레이어가 게임에서 패배했을 때의 Sprite
    public GameObject losingScreen;

    // 플레이어가 게임에서 승리했을 때의 Sprite
    public GameObject winningScreen;

    private void GameOver(bool playerHasWon)
    {
        // 플레이어 승리시
        if (playerHasWon) // true
        {
            // 설정했던 승리 Sprite 디스플레이
            winningScreen.SetActive(true);
        }
        // 플레이어 패배시
        else // false
        {
            //Debug.Log("패배");
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
        // 6/3 판다가 죽을때마다 슈거미터 500씩 증가!
        sugarmeter.sugar += 500; // 6/3 추가
        sugarmeter.updateSugarMeter();
        numberOfPandasToDefeat--;
    }

    // 판다가 플레이어의 케이크에 도착했을 때 플레이어에게 데미지를 줌
    // 게임 오버 조건에 만족하면 게임 오버 시킴
    public void BiteTheCake(int damage)
    {
        // 판다가 플레이어의 케이크를 전부 먹었는지 확인하는 함수
        // 전부 먹었으면 게임 오버 조건에 만족된다
        bool IsCakeAllEaten = playerHealth.ApplyDamage(damage);
        // 판다가 플레이어의 케이크를 전부 먹었다면 게임은 종료된다(패배 조건)
        //Debug.Log("확인되는지 확인");
        if (IsCakeAllEaten)
        {
            //Debug.Log("졌어요");
            GameOver(false);
        }
        // (그렇지 않을 경우) 케이크를 먹은 판다는 폭발해 버리므로 판다 수 1 감소
        OneMorePandaInHeaven();

        //Instantiate(pandaPrefab, spawner.position, Quaternion.identity); // 마지막에 한번 실행된다.

        //Debug.Log("실행됩니다.");
        // OK
    }
    
    //*************코루틴 코드********************

    // 코루틴을 따라갈 판다프리팹 게임오브젝트
     public GameObject pandaPrefab;

    // 코루틴을 시작할 곳
    private Transform spawner; // 여기서 1차로 정의해서 // spawner의 위치 // 즉, Spawning Spot의 위치

    // 코루틴의 수 = 판다 수
    public int numberOfWaves;
    
    // 각 웨이브마다의 판다 수
    public int numberOfPandasPerWave;

    // 판다마다 서로 다른 코루틴
    private IEnumerator WavesSpawner()
    {
        //Debug.Log("실행됩니다3");
        // 각 웨이브
        for (int i = 0; i < numberOfWaves; i++)
        {
            // 판다가 하나의 웨이브 코루틴
            // 코루틴 완료시 웨이브도 종료해 코루틴 지속
            Debug.Log("numberOfWave 작동" + i);
            yield return PandaSpawner();
            // 각 웨이브마다 생성 판다수 증가
            numberOfPandasPerWave += 3;
        }

        // 플레이어가 모든 웨이브 이겨내면 승리조건으로 게임오버된다.
        GameOver(true);
    }
    
    private IEnumerator PandaSpawner()
    {
        //Debug.Log("실행됩니다2");
        numberOfPandasToDefeat = numberOfPandasPerWave;

        //Progressively spawn pandas
        for (int i = 0; i < numberOfPandasPerWave; i++)
        {
            Debug.Log("numberOfPandasPerWave 작동" + i);
            //Debug.Log("실행됩니다1");
            // 위치에 판다를 둔다
            Instantiate(pandaPrefab, spawner.position, Quaternion.identity); // 회전 없음

            float ratio = (i * 1f) / (numberOfPandasPerWave - 1);
            float timeToWait = Mathf.Lerp(3f, 5f, ratio) + Random.Range(0f, 2f);
            yield return new WaitForSeconds(timeToWait);
        }

        // 게임 종료될때까지 대기
        yield return new WaitUntil(() => numberOfPandasToDefeat <= 0);
    }

}
