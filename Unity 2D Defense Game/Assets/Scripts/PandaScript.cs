using UnityEngine;
using System.Collections;

public class PandaScript : MonoBehaviour {

    private Rigidbody2D rb2d;
    private SpriteRenderer spr;

    private Waypoint currentWaypoint = null;   // 현재 판다가 향하고 있는 웨이포인트
    private static GameManagerScript gameManager = null;   // private static GameManagerScript. 모든 판다 클래스의 인스턴스들간에 공유한다
    private const float changeDist = 0.1f;    // 이 거리 이하면 다음 웨이포인트로 넘어감

    public bool moveable;   // 판다가 이동 가능한가? (맞고있는 동안에는 false가 될 것)
    public float noStunTime = 1f; // 판다 스턴 무적타임 (연속 스턴 방지용)
    public float curTime = 1f;

    //Private variable to store the animator for handling animations
    private Animator animator;

    //Public variables that express the characteristic of the Panda
    public float speed;     //The movement speed
    public float health;    //The amount of health

    //Hash representations of the Triggers of the Animator controller of the Panda
    private int AnimDieTriggerHash = Animator.StringToHash("DieTrigger");
    private int AnimHitTriggerHash = Animator.StringToHash("HitTrigger");
    private int AnimEatTriggerHash = Animator.StringToHash("EatTrigger");

    private float myAttackProbability;    // 판다가 컵케이크타워를 공격할 확률
    public float attackProbability = 0.1f;   // 이 확률로 처음 보는 컵케이크 타워를 공격할 것
    private bool isAttackingTower;  // 이 판다가 컵케이크타워를 공격중인가?
    private GameObject targetTower; // 공격 대상 타워

    Vector3 lastPos;

    // Use this for initialization
    void Start () {
        spr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        moveable = true;
        //Get the reference to the Animator
        animator = GetComponent<Animator>();
        myAttackProbability = -1;

        // set static GamaManagerScript variable gameManager
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManagerScript>();
        }
        currentWaypoint = gameManager.firstWaypoint;  // 첫 번째 웨이포인트 지정

        lastPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        curTime += Time.deltaTime;
    }

    // MoveTowards() 함수는 여기서 실행되어야 함
    void FixedUpdate()
    {
        // 컵케이크 타워를 향해 공격
        if (isAttackingTower)
        {
            // 컵케이크에 충분히 가까이 다가갔으므로 자폭공격
            if(Vector2.Distance(transform.position, targetTower.transform.position) < changeDist)
            {
                Eat();  // 판다가 케이크를 먹고 펑 터지는 애니메이션 trigger
                targetTower.GetComponent<CupcakeTowerScript>().Hit(); // 컵케이크 타워에 패널티를 줌
                isAttackingTower = false;   // 공격을 완료했으므로 isAttackingTower는 false가 됨
            }
            else
            { 
                MoveTowards(targetTower.transform.position); // 공격 대상 컵케이크 방향으로 이동
            }
            return;
        }

        // 맨 마지막 웨이포인트에 도착했다. 즉, 판다가 케이크에 도달했다
        if(currentWaypoint == null)
        {
            // 케이크 먹는 애니메이션 시작. StateBehaviour script에서 Object를 Destory하므로 여기서 Destory() 부를 필요 없음
            animator.SetTrigger(AnimEatTriggerHash);
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

        // 판다의 움직이는 방향을 face 하기
        var velocity = transform.position - lastPos;
        FaceMovingDirection(velocity);
        lastPos = transform.position;
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
    public void Hit(float damage) {
        //Subtract the damage to the health of the Panda
        health -= damage;
        //Then it triggers the Die or the Hit animations based if the Panda is still alive
        if(health <= 0) {
            animator.SetTrigger(AnimDieTriggerHash);
        }
        else {
            if (curTime >= noStunTime)
            {
                // 스턴 무적타임이 지났을때만 스턴상태로 들어감
                animator.SetTrigger(AnimHitTriggerHash);
                curTime = 0;
            }
        }
    }

    private void Eat()
    {
        speed = 0;
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
        else if (other.tag == "CupcakeTower")
        {
            // 랜덤한 확률로 컵케이크 타워를 공격
            myAttackProbability = Random.Range(0f, 1f);
            if(myAttackProbability <= attackProbability)
            {
                targetTower = other.gameObject; // 공격 대상 설정
                isAttackingTower = true;
            }
        }
    }

    // 판다가 움직이는 방향을 기준으로 오브젝트의 로컬스케일을 x축 반전시킨다
    private void FaceMovingDirection(Vector3 movingVector)
    {
        if (movingVector.x > 0.05)  // 오른쪽으로 가는 중
        {
            spr.flipX = false;
        }
        if (movingVector.x < -0.05) // 왼쪽으로 가는 중
        {
            spr.flipX = true;
        }
    }
}
