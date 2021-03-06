﻿using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    
    public float damage;                //How much damage will the enemy receive
    public float lifeDuration = 10f;    //How long the projectile lives before self-destructing

    public AnimationCurve curve;    // 발사체 곡선의 높이를 결정하기 위한 animation curve
    public Transform target;    // 발사체가 맞출 목표물의 위치정보
    public Vector2 start;  // 발사체의 시작지점(이 발사체를 발사한 컵케이크 타워의 위치정보)
    public Vector2 end;
    public float duration = 1f; // 발사 시작-끝 점까지의 시간
    private float time = 0f;    // 발사 시작부터 지난 시간
    public float projectile_max_height = 3f;    // 발사체의 최대 높이

    // Initialize the direction, set the right rotation and the timer for self-destruction
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //Set the timer for self-destruction
        end = target.position;
        Destroy(gameObject, lifeDuration);
    }

    // Update the position of the projectile according to time and speed
    void FixedUpdate()
    {
        if (time < duration)
        {
            time += Time.fixedDeltaTime;

            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, projectile_max_height, heightT); // change 3 to however tall you want the arc to be
            rb2d.MovePosition(Vector2.Lerp(start, end, linearT) + new Vector2(0f, height));
        }
    }

}