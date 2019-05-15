using UnityEngine;
using System.Collections;

public class PandaScript : MonoBehaviour {

    public int cakeEatenPerBite;

    private Rigidbody2D rb2d;

    private Waypoint currentWaypoint = null;   // 현재 판다가 향하고 있는 웨이포인트
    private static GameManagerScript gameManager = null;   // private static GameManagerScript. 모든 판다 클래스의 인스턴스들간에 공유한다
    private const float changeDist = 0.1f;    // 이 거리 이하면 다음 웨이포인트로 넘어감

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
        rb2d = GetComponent<Rigidbody2D>();
        moveable = true;
        //Get the reference to the Animator
        animator = GetComponent<Animator>();

        // set static GamaManagerScript variable gameManager
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManagerScript>();
        }
        currentWaypoint = gameManager.firstWaypoint;  // 첫 번째 웨이포인트 지정
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    // MoveTowards() 함수는 여기서 실행되어야 함
    void FixedUpdate()
    {
        // 맨 마지막 웨이포인트에 도착했다. 즉, 판다가 케이크에 도달했다
        if(currentWaypoint == null)
        {
            // 케이크 먹는 애니메이션 시작. StateBehaviour script에서 Object를 Destory하므로 여기서 Destory() 부를 필요 없음
            animator.SetTrigger(AnimEatTriggerHash);
            gameManager.BiteTheCake(cakeEatenPerBite);
            Destroy(this);
            return;
        }

        // 현재와 다음 waypoint사이 거리 계산
        float dist = Vector2.Distance(transform.position, currentWaypoint.GetPosition());
        if(dist <= changeDist)  // 기준거리 이하
        {
            currentWaypoint = currentWaypoint.GetNextWaypoint();    // 다음 waypoint로 현재 waypoint를 변경
        }
        else
        {
            MoveTowards(currentWaypoint.GetPosition()); // 현재 waypoint로 이동
        }
    }

    //Function that based on the speed of the Panda makes it moving towards the destination point, specified as Vector3
    private void MoveTowards(Vector3 destination) {
        if (moveable)
        {
            Vector3 move_vector;    // 이동 벡터

            // 이동 벡터 계산
            float step = speed * Time.fixedDeltaTime;   // 한 번의 call에서 최대 이동 가능한 step. 이동벡터의 magnitude가 될 것.
            Vector3 diff = destination - transform.position;    // 이동벡터의 x,y값 계산
            diff.z = 0; // normalize 이전에 z값을 0으로 하여 x,y값만을 고려하게 한다.
            diff.Normalize();
            move_vector= transform.position + (step * diff);    // 현재 위치 + (x,y값 저장한 벡터)*magnitude

            rb2d.MovePosition(move_vector); // 이동

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
            gameManager.OneMorePandaInHeaven();
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
