using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GoodPandaScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private CircleCollider2D detectCollider;   // 적군 탐지용 collider
    private BoxCollider2D attackCollider;   // 공격용 collider
    private Vector3 spawnPoint; // 아군판다의 spawnPoint 및 복귀지점
    public AIState currState;  // 아군판다의 현재 state
    public enum AIState { Wait=1, Attack, GoBack}  //아군판다의 state를 나타내는 enum (대기-Wait, 공격-Attack, 복귀-GoBack)
    private GameObject targetEnemy; // 공격 대상 적 판다
    private const float closeDist = 0.1f;   // 거리 판별용 상수

    public int damage = 10; // 아군 판다의 공격능력
    public float speed = 10f;     // 이동속도
    public float attackRange = 10f; // 기지로부터 최대 얼마나 멀리 판다를 쫓아갈것인가

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        detectCollider = GetComponent<CircleCollider2D>();
        attackCollider = GetComponent<BoxCollider2D>();
        spawnPoint = transform.position;
        StateWait();    // 대기상태로 시작
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currState)
        {
            case AIState.Attack:    // 공격 상태. 공격 대상에게 접근
                if (!IsTooFarFromHome() && targetEnemy != null)
                    MoveTowards(targetEnemy.transform.position);
                else
                    StateGoBack();  // 적 판다가 죽었거나 기지에서 너무 멀리 떨어졌으므로 복귀상태로 바뀜
                break;
            case AIState.GoBack:    // 복귀 상태. spawnPoint로 복귀
                if (Vector2.Distance((Vector2)transform.position, spawnPoint) > closeDist)
                    MoveTowards(spawnPoint);
                else
                    StateWait();    // 충분히 가까우므로 대기 상태로 바뀜
                break;
            default:
                break;
        }
    }

    private void MoveTowards(Vector3 destination)
    {
        Vector3 move_vector;    // 이동 벡터
        // 이동 벡터 계산
        float step = speed * Time.fixedDeltaTime;   // 한 번의 call에서 최대 이동 가능한 step. 이동벡터의 magnitude가 될 것.
        Vector3 diff = destination - transform.position;    // 이동벡터의 x,y값 계산
        diff.z = 0; // normalize 이전에 z값을 0으로 하여 x,y값만을 고려하게 한다.
        diff.Normalize();
        move_vector = transform.position + (step * diff);    // 현재 위치 + (x,y값 저장한 벡터)*magnitude

        rb2d.MovePosition(move_vector); // 이동
    }

    // 현재 쿠키가 기지에서 너무 먼가? (attackRange 변수를 기준으로)
    bool IsTooFarFromHome()
    {
        return (attackRange < Vector3.Distance(transform.parent.position, transform.position));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == null)
            return;
        // 적 팬더 감지
        if(!IsTooFarFromHome() && other.tag == "Enemy")
        {
            switch (currState)
            {
                case AIState.Wait:
                    StateAttack(other.gameObject);
                    break;
                case AIState.Attack:
                    if(targetEnemy != null && other.gameObject == targetEnemy)
                    {
                        var panda = targetEnemy.GetComponent<PandaScript>();
                        if (panda != null)
                            panda.Hit(damage);
                    }
                    StateGoBack();  // 연속 공격을 위해 되돌아가기
                    break;
                case AIState.GoBack:
                    StateAttack(other.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    // 대기
    private void StateWait()
    {
        currState = AIState.Wait;
        detectCollider.enabled = true;
        attackCollider.enabled = false;
    }

    // 공격
    private void StateAttack(GameObject target)
    {
        targetEnemy = target;
        currState = AIState.Attack;
        detectCollider.enabled = false;
        attackCollider.enabled = true;

    }

    // 복귀
    private void StateGoBack()
    {
        currState = AIState.GoBack;
        detectCollider.enabled = true;
        attackCollider.enabled = false;
    }
}
