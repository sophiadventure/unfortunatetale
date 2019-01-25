using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string playerName;
    public float moveSpeed;
    public Animator animator;
    public Rigidbody2D rigidBody;
    public string areaTransitionName; // Exit we JUST used

    public static PlayerController INSTANCE; // this doesn't show up in the UI because it is static

    private void Start()
    {
        // Only instantiate when the game starts running
        if(INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        float moveThreshold = 0.5f;
        bool isRight = horizontalMove > moveThreshold;
        bool isLeft = horizontalMove < -moveThreshold;
        bool isUp = verticalMove > moveThreshold;
        bool isDown = verticalMove < -moveThreshold;
        bool isHorizontalMove = isRight || isLeft;
        bool isVerticalMove = isUp || isDown;
        bool isMove = isHorizontalMove || isVerticalMove;

        rigidBody.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);

        if (isMove)
        {
            animator.SetFloat("LastMoveX", horizontalMove);
            animator.SetFloat("LastMoveY", verticalMove);
        }

        animator.SetFloat("MoveX", horizontalMove);
        animator.SetFloat("MoveY", verticalMove);
        animator.SetBool("IsPlayerMoving", isMove);

    }

    void FixedUpdate()
    {

    }
}
