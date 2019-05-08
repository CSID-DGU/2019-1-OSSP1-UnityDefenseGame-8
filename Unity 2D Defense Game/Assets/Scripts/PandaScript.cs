using UnityEngine;
using System.Collections;

public class PandaScript : MonoBehaviour {

    private Rigidbody2D rb2d;

    // Game Manager에 대한 참조를 저장하기 위한 static 변수 추가
    private static GameManagerScript gameManager;


    // 현재 웨이포인트가 몇개인지 저장하는 변수
    //private int currentWaypointNumber;
    // 현재 웨이포인트 저장
    private Waypoint currentWaypoint;

    // 임시로 웨이포인트에 도달했다고 간주하기 위한 상수
    private const float changeDist = 0.001f;

    public bool moveable;   // 판다가 이동 가능한가? (맞고있는 동안에는 false가 될 것)

    //Private variable to store the animator for handling animations
    private Animator animator;

    //Public variables that express the characteristic of the Panda
    public float speed;     //The movement speed
    public float health;    //The amount of health

    //Hash representations of the Triggers of the Animator controller of the Panda
    private int AnimDieTriggerHash = Animator.StringToHash("DieTrigger");
    private int AnimHitTriggerHash = Animator.StringToHash("HitTrigger");
    private int AnimEatTriggerHash = Animator.StringToHash("EatTrigger");

    // Use this for initialization
    void Start () {

        // Game Manager에 대한 참조가 없을 시, 스크립트는 이 코드를 통해 얻게 됨
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManagerScript>();
        }

        rb2d = GetComponent<Rigidbody2D>();
        moveable = true;
        //Get the reference to the Animator
        animator = GetComponent<Animator>();

        // 첫번째 웨이포인트를 가져온다.
        currentWaypoint = gameManager.firstWaypoint;

        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        // MoveTowards() 함수는 여기서 실행되어야 함

        if(currentWaypoint == null)
        {
            animator.SetTrigger(AnimEatTriggerHash);
            Destroy(this);
            return;
        }


        // 판다가 마지막 웨이포인트, 즉 케이크에 도달
        //if (currentWaypointNumber == gameManager.waypoints.Length)
        //{
        //    // Eat Animation Trigger
        //    animator.SetTrigger(AnimEatTriggerHash);
        //    // 판다 제거는 스테이트 머신 비헤이버가 처리
        //    // 이 스크립트만 제거
        //    Destroy(this);
        //    return;
        // }

        // 현재 판다의 위치와 판다가 향하고 있는 웨이포인트 간 거리 계산
        float dist = Vector2.Distance(transform.position, currentWaypoint.GetPosition());
        
        // 위에서 제시한 임시 웨이포인트 도달함수(changeDist)와 비교
        // changeDist 이하로 dist가 좁혀진다 -> 즉, 가까워진다
        // 웨이포인트에 도달한것으로 간주하여 웨이포인트 카운터 증가 + 1
        if (dist <= changeDist)
        {
            currentWaypoint = currentWaypoint.GetNextWaypoint();
        }

        // 그렇지 않을 경우 판다는 계속해서 웨이포인트를 향해 이동
        else
        {
            MoveTowards(currentWaypoint.GetPosition());
        }

    }

    //Function that based on the speed of the Panda makes it moving towards the destination point, specified as Vector3
    private void MoveTowards(Vector3 destination) {
        if (moveable)
        {
            //Create a step and then move in towards destination of one step
            float step = speed * Time.fixedDeltaTime;
            rb2d.MovePosition(Vector3.MoveTowards(transform.position, destination, step));
        }
    }

    /* Function that takes as input the damage that Panda received when hit by a sprinkle.
    *  After have detracted the damage to the amount of health of the Panda checks if the Panda
    *  is still alive, and so play the Hit animation, or if the health goes below zero the Die animation
    */
    private void Hit(float damage) {
        //Subtract the damage to the health of the Panda
        health -= damage;
        //Then it triggers the Die or the Hit animations based if the Panda is still alive
        if(health <= 0) {
            animator.SetTrigger(AnimDieTriggerHash);
        }
        else {
            animator.SetTrigger(AnimHitTriggerHash);
        }
        Debug.Log("Panda Health:" + health);
    }

    private void Eat()
    {
        animator.SetTrigger(AnimEatTriggerHash);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 tag가 발사체인지 확인
        if (other.tag == "Projectile")
        {
            // 판다에게 발사체에 설정된 만큼의 데미지를 입힘
            Hit(other.GetComponent<ProjectileScript>().damage);
            // 발사체는 파괴됨
            Destroy(other.gameObject);
        }
    }
}
