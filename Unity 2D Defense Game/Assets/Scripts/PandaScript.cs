using UnityEngine;
using System.Collections;

public class PandaScript : MonoBehaviour {

    private Rigidbody2D rb2d;

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
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        // MoveTowards() 함수는 여기서 실행되어야 함
    }

    //Function that based on the speed of the Panda makes it moving towards the destination point, specified as Vector3
    private void MoveTowards(Vector3 destination) {
        if (moveable)
        {
            //Create a step and then move in towards destination of one step
            float step = speed * Time.fixedDeltaTime;
            rb2d.MovePosition(Vector3.MoveTowards(transform.position, destination, step));
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
        }
        else {
            animator.SetTrigger(AnimHitTriggerHash);
        }
    }

    private void Eat()
    {
        animator.SetTrigger(AnimEatTriggerHash);
    }
}
