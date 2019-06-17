using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    private static GameManagerScript _instance;    // 싱글톤 인스턴스
    public static GameManagerScript Instance
    {
        get { return _instance; }
    }

    public Waypoint firstWaypoint;  // 판다가 따라갈 경로의 첫 번째 웨이포인트

    public GameObject winningScreen;
    public GameObject losingScreen;

    public SugarMeterScript sugarmeter;
    private HealthBarScript playerHealth;
    private int numberOfPandasToDefeat;

    // Enemy 웨이브 관리 변수들
    private Transform spawner;// 판다 스포닝 포인트
    public GameObject pandaPrefab;
    public int numberOfWaves;
    public int numberOfPandasPerWave;

    // 적의 등장을 알리는 Text UI
    public Text SpawnText;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }

        playerHealth = FindObjectOfType<HealthBarScript>();
        sugarmeter = FindObjectOfType<SugarMeterScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 판다 스포닝 포인트의 transform을 가져옴
        spawner = GameObject.Find("SpawningPoint").transform;

        // 판다 스포닝 시작
        StartCoroutine("WavesSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameOver(bool playerHasWon)
    {
        // 플레이어의 승패에 따라 승리 또는 패배 화면을 Active 시킨다.
        if (playerHasWon)
        {
            winningScreen.SetActive(true);
        }
        else
        {
            losingScreen.SetActive(true);
        }

        // 게임의 시간을 멈춤으로써 일시정지 한다
        Time.timeScale = 0;
    }

    // 판다 한 마리가 죽었을 경우
    public void OneMorePandaInHeaven()
    {
        numberOfPandasToDefeat--;

        // 슈거 올라감
        sugarmeter.ChangeSugar(10);
    }

    public void BiteTheCake(int damage)
    {
        bool IsCakeAllEaten = playerHealth.ApplyDamage(damage);
        if (IsCakeAllEaten)
        {
            GameOver(false);
        }

        OneMorePandaInHeaven();
    }

    // 전체 웨이브를 관리하는 코루틴
    // 정해진 웨이브 수 만큼 판다들을 스폰한다. 모든 웨이브를 플레이어가 견뎌냈다면 승리이므로 GameOver(true)를 호출하여 승리화면을 출력한다.
    private IEnumerator WavesSpawner()
    {
        for(int i=0; i<numberOfWaves; ++i)
        {
            // 웨이브 정보를 Text에 업데이트하고 출력
            SpawnText.text =  "Wave " + (i + 1) + ":\n" + (numberOfPandasPerWave) + " pandas";
            SpawnText.GetComponent<SpawnTextScript>().DisplayText();

            // 현재 웨이브에 속하는 판다들을 스폰
            yield return PandaSpawner();

            // 다음 웨이브에서 스폰되는 판다들의 숫자를 증가
            numberOfPandasPerWave += 3;
        }

        // 플레이어가 모든 웨이브들을 견뎌냈으므로 승리
        GameOver(true);
    }

    // 각 웨이브에서 실제로 판다들을 스폰하는 코루틴
    private IEnumerator PandaSpawner()
    {
        numberOfPandasToDefeat = numberOfPandasPerWave;
        for(int i=0; i<numberOfPandasPerWave; ++i)
        {
            // 스폰 포인트에 판다 스폰함
            Instantiate(pandaPrefab, spawner.position, Quaternion.identity);

            // 임의 시간동안 기다린 후 다음 판다를 스폰
            float timeToWait = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(timeToWait);
        }

        
        // 스폰된 판다들이 모두 죽으면 현재 웨이브 종료됨
        yield return new WaitUntil(() => numberOfPandasToDefeat <= 0);  // 람다 식 사용
    }
}
