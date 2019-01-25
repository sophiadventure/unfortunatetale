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
    public Vector2 startingPosition;

    public static PlayerController INSTANCE; // this doesn't show up in the UI because it is static

    private void Start()
    {
        // Only instantiate when the game starts running
        if(INSTANCE == null)
        {
            INSTANCE = this;
            if(startingPosition != null)
            {
                this.transform.position = startingPosition;
            }
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
        bool isDance = Input.GetButton("Fire1");

        // Move the player
        rigidBody.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);

        // Set animator variables below

        if (isMove)
        {
            // The 2 variables below are used to determine the orientation of the player
            // Only set these two when the player is actually moving (not to 0, so we can remember orientation)
            animator.SetFloat("LastMoveX", horizontalMove);
            animator.SetFloat("LastMoveY", verticalMove);
        }

        animator.SetFloat("MoveX", horizontalMove);
        animator.SetFloat("MoveY", verticalMove);
        animator.SetBool("IsPlayerMoving", isMove);
        animator.SetBool("IsPlayerDancing", isDance);

    }

    void FixedUpdate()
    {

    }
}
