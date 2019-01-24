using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string playerName;
    public float moveSpeed;
    public Animator animator;
    public Rigidbody2D rigidBody;

    private void Start()
    {
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
