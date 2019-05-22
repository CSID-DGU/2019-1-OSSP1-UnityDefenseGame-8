using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodPandaScript : MonoBehaviour
{
    CircleCollider2D attackRange;   // 아군 판다의 적군 탐지 범위
    // Start is called before the first frame update
    void Start()
    {
        attackRange = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
