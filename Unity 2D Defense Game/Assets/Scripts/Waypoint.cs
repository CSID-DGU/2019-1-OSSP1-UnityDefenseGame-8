using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    // 웨이포인트를 마치 체인처럼 연결한다. 
    [SerializeField]
    // 체인에서 다음 웨이포인트를 저장할 변수
    private Waypoint nextWaypoint;

    // 웨이포인트의 위치를 검색
    // 현재 무슨 웨이포인트에 존재하는가?
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // 체인에서 다음 웨이포인트를 검색
    public Waypoint GetNextWaypoint()
    {
        return nextWaypoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
