using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Waypoint nextWaypoint;  // 다음 웨이포인트. Inspector에서 세팅하기 위해 serializeField

    // Returns the position of this waypoint
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // Returns the next waypoint in chain
    public Waypoint GetNextWaypoint()
    {
        return nextWaypoint;
    }

    public void SetNextWaypoint(Waypoint wp)
    {
        nextWaypoint = wp;
    }

    // 웨이포인트 체인 그리기
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;   // Color : Red

        // 현재 웨이포인트에서 다음 웨이포인트에 선 그리기
        if (nextWaypoint != null)
        {
            Gizmos.DrawLine(transform.position, nextWaypoint.transform.position);
        }
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
