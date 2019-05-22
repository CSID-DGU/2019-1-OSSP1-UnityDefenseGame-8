using UnityEngine;
using System.Collections;

//SCRIPT VERSION - END OF CHAPTER 2
public class GoodPandaTowerScript : MonoBehaviour
{

    private int upgradeLevel;        //Level of the Cupcake Tower
    public Sprite[] upgradeSprites; //Different sprites for the different levels of the Cupcake Tower
    private SpriteRenderer currentSpriteRenderer; // 현재 컵케이크 타워의 spriteRenderer 참조변수

    // 타워 stat 정보
    public float spawnRadius;           // 아군판다가 spawn될 수 있는 radius
    public float reloadTime;            // 죽은 아군판다가 re-spawn될 수 있는 time
    public GameObject GoodPandaPrefab; // 아군판다 기지에서 생성할 아군판다(GoodPanda) Prefab
    public int maxGoodPanda = 3;    // 최대 spawn가능한 아군판다 수
    private int curGoodPanda = 0;   // 현재 spawn되어있는 아군판다 수
    private float elapsedTime;          //Time elapsed from the last time the Cupcake Tower has produced its last GoodPanda
    
    // 아군판다 유닛 stat 정보


    //Boolean to check if the tower is upgradable
    public bool IsUpgradable
    {
        get
        {
            return (upgradeLevel + 1 < upgradeSprites.Length);
        }
    }

    // 아군판다들을 radius 범위 내에서 랜덤한 위치에 spawn
    void Start()
    {
        currentSpriteRenderer = GetComponent<SpriteRenderer>();
        SpawnGoodPanda();
    }

    // 현재 아군판다 수가 max 아군판다 수와 동일해 질 때 까지 기지 주변의 원을 두르면서 아군판다 생성
    public void SpawnGoodPanda() {
        for(; curGoodPanda < maxGoodPanda; ++curGoodPanda) {
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

    public void Upgrade()
    {
        //Check if the tower is upgradable
        if (!IsUpgradable)
        {
            return;
        }

        //Increase the level of the tower
        upgradeLevel++;

        //Increase the stats of the tower
        reloadTime -= 0.5f;
        maxGoodPanda += 2;

        //Change graphics of the tower
        currentSpriteRenderer.sprite = upgradeSprites[upgradeLevel];
    }

    //Implements the shooting logic
    void Update()
    {
       
    }
}