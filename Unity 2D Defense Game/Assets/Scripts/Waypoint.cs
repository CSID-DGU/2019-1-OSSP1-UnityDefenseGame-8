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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
