using UnityEngine;
using System.Collections;

//SCRIPT VERSION - END OF CHAPTER 2
public class CupcakeTowerScript : MonoBehaviour
{

    protected int upgradeLevel;        //Level of the Cupcake Tower
    public Sprite[] upgradeSprites; //Different sprites for the different levels of the Cupcake Tower
    protected SpriteRenderer currentSpriteRenderer; // 현재 컵케이크 타워의 spriteRenderer 참조변수

    public int initialCost; // 초기 타워 가격
    public int upgradingCost;   // 타워 업그레이드 비용
    public int sellingValue;    // 타워 판매시 플레이어가 얻는 비용

    public float rangeRadius;           //Maximum distance that the Cupcake Tower can shoot
    public float reloadTime;            //Time before the Cupcake Tower is able to shoot again
    public GameObject projectilePrefab; //Projectile type that is fired from the Cupcake Tower

    private float elapsedTime;          //Time elapsed from the last time the Cupcake Tower has shot

    //Boolean to check if the tower is upgradable
    public bool IsUpgradable
    {
        get
        {
            return (upgradeLevel + 1 < upgradeSprites.Length);
        }
    }

    public void Start()
    {
        currentSpriteRenderer = GetComponent<SpriteRenderer>();
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
        rangeRadius += 5f;
        reloadTime -= 0.1f;

        //Change graphics of the tower
        currentSpriteRenderer.sprite = upgradeSprites[upgradeLevel];

        // 타워 업그레이드 비용과 판매시 얻는 가격이 늘어남
        sellingValue += 5;
        upgradingCost += 10;
    }

    //Implements the shooting logic
    public void Update()
    {
        if (elapsedTime >= reloadTime)
        {
            //Reset elapsed Time
            elapsedTime = 0;

            //Find all the gameObjects with a collider within the range of the Cupcake Tower
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, rangeRadius);

            //Check if there is at least one gameObject found
            if (hitColliders.Length != 0)
            {
                //Loop over all the gameObjects to identify the closest to the Cupcake Tower
                float min = int.MaxValue;
                int index = -1;

                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].tag == "Enemy")
                    {
                        float distance = Vector2.Distance(hitColliders[i].transform.position, transform.position);
                        if (distance < min)
                        {
                            index = i;
                            min = distance;
                        }
                    }
                }
                if (index == -1)
                    return;

                //Get the direction of the target
                Transform target = hitColliders[index].transform;
                Vector2 direction = (target.position - transform.position).normalized;

                //Create the Projectile
                GameObject projectile = GameObject.Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
                // 발사체의 곡선 발사를 위해 target을 발사체에 전달해준다. 
                projectile.GetComponent<ProjectileScript>().target = target;
                // 발사체를 발사한 컵케이크 타워의 위치를 발사체에 전달해준다.
                projectile.GetComponent<ProjectileScript>().start = transform.position;
            }
        }
        elapsedTime += Time.deltaTime;
    }

    // 적 판다에게 타워가 공격당해 일정시간동안 fire 불가능
    public void Hit()
    {
        elapsedTime = -2f;
    }

    // 컵케이크 타워의 업그레이드, 판매 등을 위해서 이 타워가 선택(마우스 클릭)되었을 때 호출되는 함수
    private void OnMouseDown()
    {
        // TradeCupCakeTowers 클래스의 static 함수인 SetActiveTower 호출을 통하여, 역시 static 변수인 현재 선택된 타워를 설정하게 된다
        TradeCupcakeTowers.SetActiveTower(this);
    }
}