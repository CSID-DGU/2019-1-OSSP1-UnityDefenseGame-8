using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointChainScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitWaypointChain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 웨이포인트 체인을 생성
    private void InitWaypointChain()
    {
        int children_num = transform.childCount;    // Child object (waypoint)들의 개수

        if (children_num <= 0) return;

        // waypoint 끼리의 연결관계 생성
        Waypoint cur_wp = transform.GetChild(0).gameObject.GetComponent<Waypoint>();
        for(int i=1; i< children_num; ++i)
        {
            // 현재 waypoint와 다음 것을 연결
            Waypoint next_wp = transform.GetChild(i).gameObject.GetComponent<Waypoint>();   // next waypoint 설정
            cur_wp.SetNextWaypoint(next_wp);
            cur_wp = next_wp;
        }
    }

    
}
