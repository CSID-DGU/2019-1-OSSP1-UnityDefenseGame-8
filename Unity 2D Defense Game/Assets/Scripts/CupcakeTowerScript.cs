using UnityEngine;
using System.Collections;

//SCRIPT VERSION - END OF CHAPTER 2
public class CupcakeTowerScript : MonoBehaviour
{

    private int upgradeLevel;        //Level of the Cupcake Tower
    public Sprite[] upgradeSprites; //Different sprites for the different levels of the Cupcake Tower
    private SpriteRenderer currentSpriteRenderer; // 현재 컵케이크 타워의 spriteRenderer 참조변수

    //Boolean to check if the tower is upgradable
    public bool IsUpgradable
    {
        get
        {
            return (upgradeLevel + 1 < upgradeSprites.Length);
        }
    }

    void Start()
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
        rangeRadius += 1f;
        reloadTime -= 0.5f;

        //Change graphics of the tower
        currentSpriteRenderer.sprite = upgradeSprites[upgradeLevel];
    }

    public float rangeRadius;           //Maximum distance that the Cupcake Tower can shoot
    public float reloadTime;            //Time before the Cupcake Tower is able to shoot again
    public GameObject projectilePrefab; //Projectile type that is fired from the Cupcake Tower

    private float elapsedTime;          //Time elapsed from the last time the Cupcake Tower has shot

    //Implements the shooting logic
    void Update()
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
}