using UnityEngine;
using System.Collections;

public class PandaScript : MonoBehaviour
{

    // Animation을 핸들링하기 위한 Animator을 저장할 변수
    private Animator animator;

    // 판다의 수명과 속도를 결정한다.
    public float speed;     // 판다의 속도
    public float health;    // 판다의 수명

    // 판다 Animator Contoller의 TriggerHash
    // 우리는 현재 DieTrigger, HitTrigger, EatTrigger 존재
    // DieTrigger : 판다가 발사체 에 맞아 죽을 때
    // HitTrigger : 판다가 발사체에 공격 당했을 때
    // EatTrigger : 판다가 플레이어 쪽 케이크를 먹을 때
    private int AnimDieTriggerHash = Animator.StringToHash("DieTrigger");
    private int AnimHitTriggerHash = Animator.StringToHash("HitTrigger");
    private int AnimEatTriggerHash = Animator.StringToHash("EatTrigger");

    // Use this for initialization
    void Start()
    {
        // Animator에 대한 참조 얻기
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 판다가 한 지점을 향해 가게 하는 함수
    // 이 때, 판다가 가고자 하는 목표 지점을 나타내는 변수는 Vector3 사용
    private void MoveTowards(Vector3 destination)
    {
        //속도 변수를 기반으로 판다가 이동하는 거리를 나타낸 변수 step
        float step = speed * Time.deltaTime;
        //MoveTowards() 함수 사용해 목표지점으로 step 만큼 판다 이동
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
    }

    // 판다가 컵케이크의 발사체에 맞으면 위 함수가 아닌 Hit 함수 호출
    // 판다가 받은 데미지양(변수) damage를 변수로 받음
    private void Hit(float damage)
    {
        // 판다의 현재 수명에서 damage를 뺀다
        // 발사체로 부터 공격을 받고난 후의 수명을 계산하기 위함
        health -= damage;

        // 빼고난 수명이 0보다 작으면 Die Animation 재생
        if (health <= 0)
        {
            animator.SetTrigger(AnimDieTriggerHash);
        }

        // 빼고난 수명이 0보다 작지 않을 경우 Hit Animation 재생
        else
        {
            animator.SetTrigger(AnimHitTriggerHash);
        }
    }

    // 판다가 경로 끝에 도착했을 때 플레이어의 케이크를 먹는 Eat Animation 호출
    private void Eat()
    {
        animator.SetTrigger(AnimEatTriggerHash);

    }

}
